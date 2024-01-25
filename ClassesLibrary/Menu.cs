using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesLibrary
{
    public class Menu
    {
        public static void StartComputer()
        {
            Console.WriteLine();
            Console.WriteLine("Press SPACEBAR to start app");
            var vkl = Console.ReadKey().Key;
            if (vkl == ConsoleKey.Spacebar)
            {
                Console.WriteLine("I can: ");
                BeginAction();
            }
            else
            {
                Console.WriteLine("Bye...bye");
                Console.Clear();
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("1. Take data from file(you need to input path)\n" +
                "2. Enter data manually\n" +
                "3. Perform selection\n" +
                "4. Perform sorting\n" +
                "5. Output current Json file\n" +
                "6. Write data to the file(you need to input path)\n" +
                "7. Exit");
        }

        private static void BeginAction()
        {
            do
            {
                ShowMenu();
                Console.CursorVisible = false;
                switch (HelpingMethods.Item())
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        JsonParser.ReadJson();
                        Console.WriteLine("Data loaded");
                        Thread.Sleep(1000);
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        JsonParser.ReadJsonFromConsole();
                        Console.WriteLine("Data loaded");
                        Thread.Sleep(1000);
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        DataProcessing.Selecting();
                        Console.WriteLine("Complete!");
                        Thread.Sleep(1000);
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        HelpingMethods.SortChoose();
                        Console.WriteLine("Complete!");
                        Thread.Sleep(1000);
                        break;
                    case ConsoleKey.D5:
                        Console.Clear();
                        JsonParser.WriteJsonToConsole(HelpingMethods.currentAppUsers);
                        Thread.Sleep(1000);
                        break;
                    case ConsoleKey.D6:
                        Console.Clear();
                        HelpingMethods.WriteChoose();
                        break;
                    case ConsoleKey.D7:
                        Console.Clear();
                        Console.WriteLine("Do you really want it? :(");
                        break;
                }
                Console.WriteLine("To EXIT press 7\n" +
                    "If you want to turn to menu, any other input");
            } while (Console.ReadKey().Key != ConsoleKey.D7);
        }
    }
}
