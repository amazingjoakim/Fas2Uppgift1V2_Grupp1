﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fas2Uppgift1V2_Grupp1
{
    class Program
    {
        class TodoTask
        {
            /* 
               CLASS: TodoTask
               PURPOSE: a todo entry in todo list
            */

            public string date, state, description;

            public TodoTask(string date, string state, string description)
            {
                this.date = date;
                this.state = state;
                this.description = description;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the todo list!\n" +
                "Type 'quit' to quit, type 'help' for help!");

            //Variable Declarations
            string command = "";
            string[] commandWords;
            List<TodoTask> todoList = new List<TodoTask>();
            string file = "";

            //Command Prompt
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                commandWords = command.Split(' ');

                if (command == "quit")
                {
                    Console.WriteLine("Bye");
                }
                else if (command == "help")
                {
                    PrintHelp();
                }
                else if (command == "save")
                {
                    SaveListToFile(todoList, file);
                }
                else if (command == "show")
                {
                    ShowList(todoList);
                }
                else if (commandWords[0] == "load")
                {
                    //TBD try/catch
                    file = commandWords[1];
                    todoList.AddRange(LoadFile(file));
                    ShowList(todoList);
                }
                else if (commandWords[0] == "add")
                {
                    todoList.Add(AddTodoTask(commandWords));
                }
                else if (commandWords[0] == "delete")
                {
                    DeleteTodoTask(commandWords, todoList);
                }
                else if (commandWords[0] == "set")
                {
                    SetState(commandWords, todoList);
                }
                else if (commandWords[0] == "move")
                {
                    
                    MoveTodoTask(commandWords, todoList);
                }
                else if (commandWords[0] == "save")
                {
                    file = commandWords[1];
                    SaveListToFile(todoList, file);
                }
                else
                {
                    Console.WriteLine("Unknown command");
                }

            } while (command.ToLower() != "quit");

        }

        #region Methods
        private static void DeleteTodoTask(string[] commandWords, List<TodoTask> todoList)
        {
            /* 
               METHOD: DeleteTodoTask (static)
               PURPOSE: Remove todo entry from todo list
               PARAMETERS: string[commandWords] - get todo entry position. List<TodoTask> todo list- the todo list to remove todo entry from
               RETURN VALUE: none
            */

            //TBD try/catch if number is not in list
            for (int i = 0; i < todoList.Count(); i++)
            {
                int listPos = i + 1;
                if (listPos == int.Parse(commandWords[1]))
                {
                    todoList.RemoveAt(i);
                }
            }
        }

        private static void ShowList(List<TodoTask> todoList)
        {
            //TBD - inform user if list is empty
            int listPos = 1;
            foreach (TodoTask todo in todoList)
            {
                Console.WriteLine($"{listPos}. {todo.date} - {todo.state} - {todo.description}");
                listPos++;
            }
        }

        private static void SetState(string[] commandWords, List<TodoTask> todoList)
        {
            //TBD try/catch index error
            if (commandWords[2] == "avklarad")
            {
                todoList[int.Parse(commandWords[1]) - 1].state = "*";
            }
            else if (commandWords[2] == "pågående")
            {
                todoList[int.Parse(commandWords[1]) - 1].state = "p";
            }
        }

        private static void SaveListToFile(List<TodoTask> todoList, string file)
        {
            //TBD try/catch to avoid file error
            using (StreamWriter sw = new StreamWriter(file)) // opens a stream to file and creates a writer
            {
                foreach (TodoTask task in todoList)
                {
                    sw.WriteLine($"{task.date}#{task.state}#{task.description}");
                }
            }
        }

        private static void MoveTodoTask(string[] commandWords, List<TodoTask> todoList)
        {
            //TBD try/catch
            int index = int.Parse(commandWords[1]) - 1;

            if (commandWords[2] == "down")
            {
                todoList.Insert(index + 2, todoList[index]);
                todoList.RemoveAt(index);
            }
            else if (commandWords[2] == "up" && index != 0)
            {
                todoList.Insert(index - 1, todoList[index]);
                todoList.RemoveAt(index + 1);
            }
            else
            {
                Console.WriteLine("Fel kommando");
            }
        }

        private static TodoTask AddTodoTask(string[] commandWords)
        {
            for (int i = 3; i < commandWords.Count(); i++)
            {
                commandWords[2] += $" {commandWords[i]}";
            }

            return new TodoTask(commandWords[1], "v", commandWords[2]);
        }

        static List<TodoTask> LoadFile(string filePath) //C:\Users\Admin\grupp1.lis
        {
            List<TodoTask> todoList = new List<TodoTask>();
            string[] fileLine;

            //TBD try/catch, if input is incorrect
            using (StreamReader sr = new StreamReader(@filePath))
            {
                while (sr.Peek() >= 0)
                {
                    fileLine = sr.ReadLine().Split('#');
                    TodoTask newTask = new TodoTask(fileLine[0], fileLine[1], fileLine[2]);
                    todoList.Add(newTask);
                }
            }

            return todoList;
        }

        static void PrintHelp()
        {
            Console.WriteLine("Help commands: \n" +
                "'load' <filepath> -- loads todo file \n" +
                "'show' -- shows todo list\n" +
                "'add <todoTask>' -- add new todo task\n" +
                "'delete' -- deletes todo from list\n" +
                "'save' -- saves list to current file\n" +
                "'save <filepath>' -- saves list to specific file path\n" +
                "'move <number> <up/down>' -- moves todo task position in list\n" +
                "'set <taskNo> <state>' -- set state for todo task\n"
                );
        }
        #endregion

    }
}
