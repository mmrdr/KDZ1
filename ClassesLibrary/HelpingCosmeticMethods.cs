using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesLibrary
{
    public partial class HelpingMethods
    {
        public static void PrintWithColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static void SelectionSortingMenu()
        {
            Console.WriteLine("1. Customer id\n" +
                "2. Name\n" +
                "3. Email\n" +
                "4. Age\n" +
                "5. City\n" +
                "6. Premium status\n" +
                "7. Orders");
        }

        internal static void Welcoming()
        {
            PrintWithColor("Which field will be selectered: ", ConsoleColor.Yellow);
            SelectionSortingMenu();
            Console.CursorVisible = false;
        }

        internal static void FillDataFromConsoleMenu()
        {
            PrintWithColor("You are filling data now ", ConsoleColor.Yellow);
            Console.WriteLine("You can input data about:\n" +
                "But you should input ALL data\n" +
                "1. \"customer_id\"\n" +
                "2. \"name\"\n" +
                "3. \"email\"\n" +
                "4. \"age\"\n" +
                "5. \"city\"\n" +
                "6. \"is_premium\" status\n" +
                "7. \"orders\"");
            Console.Write("What you want to fill: ");
        }

        internal static void ShowMenu()
        {
            Console.WriteLine("1. Take data from file(you need to input path)\n" +
                "2. Enter data manually\n" +
                "3. Perform selection\n" +
                "4. Perform sorting\n" +
                "5. Output current Json file\n" +
                "6. Write data to the file(you need to input path)\n" +
                "7. Exit");
        }
    }
}
