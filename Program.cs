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
            public string date, state, description;

            public TodoTask()
            {

            }
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

            string command = "";
            string[] commandWords;            
            List<TodoTask> todoList = new List<TodoTask>();

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
                else if (commandWords[0] == "load")
                {
                    string file = commandWords[1];
                    todoList.AddRange(LoadFile(file));
                }
                else if (command == "show")
                {
                    int listPos = 1;
                    foreach (TodoTask todo in todoList)
                    {
                        Console.WriteLine($"{listPos}. {todo.date} - {todo.state} - {todo.description}");
                        listPos++;
                    }
                }
                else if (commandWords[0] == "add")
                {
                    todoList.Add(AddTodoTask(commandWords));
                }
                else if (commandWords[0] == "delete")
                {
                    for (int i = 0; i < todoList.Count(); i++)
                    {
                        int listPos = i + 1;
                        if (listPos == int.Parse(commandWords[1]))
                        {
                            todoList.RemoveAt(i);
                        }
                    }
                }
                else if (commandWords[0] == "set")
                {
                    if (commandWords[2] == "avklarad")
                    {
                        todoList[int.Parse(commandWords[1]) - 1].state = "*";
                    }
                    else if (commandWords[2] == "pågående")
                    {
                        todoList[int.Parse(commandWords[1]) - 1].state = "p";
                    }
                }
                else if (commandWords[0] == "move")
                {                   
                    if (commandWords[2] == "up")
                    {
                        int oldIndex = int.Parse(commandWords[1]) -1;
                        int newIndex = oldIndex - 1;

                        TodoTask tempTodoTask = todoList[newIndex];
                        todoList[newIndex] = todoList[oldIndex];
                        todoList[oldIndex] = tempTodoTask;
                    }
                    else if (commandWords[2] == "down")
                    {
                        int oldIndex = int.Parse(commandWords[1]) - 1;
                        int newIndex = oldIndex + 1;

                        TodoTask tempTodoTask = todoList[newIndex];
                        todoList[newIndex] = todoList[oldIndex];
                        todoList[oldIndex] = tempTodoTask;
                    }
                }
                else if (command == "save")
                {

                }

            } while (command.ToLower() != "quit");

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

            using(StreamReader sr = new StreamReader(@filePath))
            {
                while (sr.Peek() >= 0)
                {
                    fileLine = sr.ReadLine().Split('#');
                    TodoTask newTask = new TodoTask(fileLine[0], fileLine[1], fileLine[2]);
                    todoList.Add(newTask);
                    Console.WriteLine($"{newTask.date} - {newTask.state} - {newTask.description}");
                }
            }

            return todoList;
        }

        static void PrintHelp()
        {
            Console.WriteLine("Help commands: \n" +
                "<load> <filepath> -- loads the todofile \n" +
                "<add> <todoTask> -- add new todo task\n" +
                "<delete> -- deletes todo from list\n" +
                "<save> -- saves list to current file\n" +
                "<save> <filepath> -- saves list to specific file path\n" +
                "<move> <number> <up/down> -- moves todo task position in list\n" +
                "<set> <taskNo> <state> -- set state for todo task\n" +
                "<show> -- shows todo list"
                );
        }


    }
}
