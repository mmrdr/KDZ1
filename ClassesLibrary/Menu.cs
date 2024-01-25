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
            HelpingMethods.PrintWithColor("Press SPACEBAR to start app", ConsoleColor.Yellow);
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

        private static void BeginAction()
        {
            do
            {
                HelpingMethods.ShowMenu();
                Console.CursorVisible = false;
                switch (HelpingMethods.Item())
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        JsonParser.ReadJson();
                        HelpingMethods.PrintWithColor("Data loaded",ConsoleColor.Green);
                        Thread.Sleep(1000);
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        JsonParser.ReadJsonFromConsole();
                        HelpingMethods.PrintWithColor("Data loaded", ConsoleColor.Green);
                        Thread.Sleep(1000);
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        DataProcessing.Selecting();
                        HelpingMethods.PrintWithColor("Complete!", ConsoleColor.Green);
                        Thread.Sleep(1000);
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        HelpingMethods.SortChoose();
                        HelpingMethods.PrintWithColor("Complete!", ConsoleColor.Green);
                        Thread.Sleep(1000);
                        break;
                    case ConsoleKey.D5:
                        Console.Clear();
                        JsonParser.WriteJsonToConsole(HelpingMethods.currentAppUsers);
                        Thread.Sleep(2000);
                        HelpingMethods.PrintWithColor("Stop looking at file :(", ConsoleColor.DarkRed);
                        Thread.Sleep(1000);
                        break;
                    case ConsoleKey.D6:
                        Console.Clear();
                        HelpingMethods.WriteChoose();
                        break;
                    case ConsoleKey.D7:
                        Console.Clear();
                        HelpingMethods.PrintWithColor("DO YOU REALLY WANT IT? :(", ConsoleColor.DarkRed);
                        Thread.Sleep(1000);
                        break;
                }
                Console.Clear();
                HelpingMethods.PrintWithColor("To EXIT press 7\n" +
                    "If you want to turn to menu, any other input", ConsoleColor.Blue);
            } while (Console.ReadKey().Key != ConsoleKey.D7);
        }
    }
}
