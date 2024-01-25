using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassesLibrary
{
    public partial class HelpingMethods
    {
        internal static void GetFilePath()
        {
            Console.WriteLine("Input full path to file");
            string path = Console.ReadLine();
            while (!File.Exists(path) || String.IsNullOrEmpty(path) || path.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                Console.WriteLine("Wrong file path, try again");
                path = Console.ReadLine();
            }
            file_path = path;
        }

        internal static bool CheckJsonCorrectness()
        {
            if (!CheckUserDataCorrectness()) return false;
            StreamReader jsonFile = new StreamReader(file_path);
            string currentLine = jsonFile.ReadLine();
            int valueForJsonCheck = 0;
            if (currentLine != null || currentLine == "[")
            {
                while ((currentLine = jsonFile.ReadLine()) != null)
                {
                    if (currentLine == "]")
                    {
                        break;
                    }
                    if (currentLine.Trim() == "{")
                    {
                        valueForJsonCheck++;
                    }
                    if (currentLine.Trim() == "},")
                    {
                        valueForJsonCheck--;
                    }
                }
            }
            if (valueForJsonCheck != 1)
            {
                Console.WriteLine("Incorrect structure of brackets");
                return false;
            }
            return true;
        }

        internal static bool CheckUserDataCorrectness()
        {
            StreamReader jsonFile = new StreamReader(file_path);
            int key = 0;
            int value = 1;
            string currentLine;
            while ((currentLine = jsonFile.ReadLine()) != null)
            {
                if (currentLine.Trim() == "{")
                {
                    for (int i = 0; i < 7; i++)
                    {
                        try
                        {
                            currentLine = jsonFile.ReadLine().Trim(' ', ',').Replace("\"", "");
                            if (currentLine == null)
                            {
                                return false;
                            }
                            string[] currentItem = currentLine.Split(':');
                            if (currentItem.Length == 0) return false;
                            switch (currentItem[key])
                            {
                                case "customer_id":
                                    if (!int.TryParse(currentItem[value].Trim(), out int value1))
                                    {
                                        Console.WriteLine("Incorrect id");
                                        return false;
                                    }
                                    break;
                                case "name":
                                    if (!Regex.IsMatch(currentItem[value].Trim(), namePattern))
                                    {
                                        Console.WriteLine("Incorrect name");
                                        return false;
                                    }
                                    break;
                                case "email":
                                    if (!Regex.IsMatch(currentItem[value].Trim(), emailPattern))
                                    {
                                        Console.WriteLine("Incorrect email");
                                        return false;
                                    }
                                    break;
                                case "age":
                                    if (!int.TryParse(currentItem[value].Trim(), out int value2))
                                    {
                                        Console.WriteLine("Incorrect age");
                                        return false;
                                    }
                                    break;
                                case "city":
                                    if (!Regex.IsMatch(currentItem[value].Trim(), cityPattern))
                                    {
                                        Console.WriteLine("Incorrect city");
                                        return false;
                                    }
                                    break;
                                case "is_premium":
                                    if (!bool.TryParse(currentItem[value].Trim(), out bool value3))
                                    {
                                        Console.WriteLine("Incorrect premium status");
                                        return false;
                                    }
                                    break;
                                case "orders":
                                    if (currentItem[value].Trim() != "[")
                                    {
                                        Console.WriteLine("No offers in order list");
                                        return false;
                                    }
                                    else
                                    {
                                        while ((currentLine = jsonFile.ReadLine().Trim()) != "]")
                                        {
                                            if (!double.TryParse(currentLine.TrimEnd(',').Trim().Replace('.', ','), out double value4))
                                            {
                                                Console.WriteLine("Incorrect price");
                                                return false;
                                            }
                                        }
                                    }
                                    break;

                                default: return false;
                            }
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine(ex.Message + " " + "in CheckUserDataCorrectness");
                        }
                    }
                }
            }
            return true;
        }

        internal static void FillAppUsersFields(List<AppUser> appUsers)
        {
            Console.WriteLine("Do you want to input data for user?[y/n]");
            var answer = Console.ReadKey().Key;
            while (answer != ConsoleKey.Y && answer != ConsoleKey.N)
            {
                Console.WriteLine("Incorrect answer, try again[y,n]");
                answer = Console.ReadKey().Key;
            }
            if (answer == ConsoleKey.Y)
            {
                Console.Clear();
                appUsers.Add(new AppUser());
                Console.WriteLine($"Now you are filling the {appUsers[appUsers.Count - 1]}");
                while (appUsers[appUsers.Count - 1].CustomerId == 0 || appUsers[appUsers.Count - 1].Name == null || appUsers[appUsers.Count - 1].Email == null
                    || appUsers[appUsers.Count - 1].Age == 0 || appUsers[appUsers.Count - 1].City == null || appUsers[appUsers.Count - 1].Orders.Count == 0)
                {
                    Console.Clear();
                    FillDataFromConsoleMenu();
                    var key = Console.ReadKey().Key;
                    switch (key)
                    {
                        case ConsoleKey.D1:
                            Console.Clear();
                            Console.Write("Input id: ");
                            string n1 = Console.ReadLine();
                            while (!int.TryParse(n1, out int x) || n1 == null || String.IsNullOrEmpty(n1) || int.Parse(n1) < 0 || int.Parse(n1) > int.MaxValue)
                            {
                                Console.WriteLine("Incorrect id, try again");
                                Console.Write("Input id: ");
                                n1 = Console.ReadLine();
                            }
                            appUsers[appUsers.Count - 1].CustomerId = int.Parse(n1);
                            break;
                        case ConsoleKey.D2:
                            Console.Clear();
                            Console.WriteLine("Input name: ");
                            string n2 = Console.ReadLine();
                            while (!Regex.IsMatch(n2, namePattern) || n2 == null || String.IsNullOrEmpty(n2))
                            {
                                Console.WriteLine("Incorrect name, try again");
                                Console.Write("Input name: ");
                                n2 = Console.ReadLine();
                            }
                            appUsers[appUsers.Count - 1].Name = n2;
                            break;
                        case ConsoleKey.D3:
                            Console.Clear();
                            Console.WriteLine("Input email: ");
                            string n3 = Console.ReadLine();
                            while (!Regex.IsMatch(n3, emailPattern) || n3 == null || String.IsNullOrEmpty(n3))
                            {
                                Console.WriteLine("Incorrect email, try again");
                                Console.Write("Input email: ");
                                n3 = Console.ReadLine();
                            }
                            appUsers[appUsers.Count - 1].Email = n3;
                            break;
                        case ConsoleKey.D4:
                            Console.Clear();
                            Console.WriteLine("Input age: ");
                            string n4 = Console.ReadLine();
                            while (!int.TryParse(n4, out int y) || n4 == null || String.IsNullOrEmpty(n4) || int.Parse(n4) < 0 || int.Parse(n4) > int.MaxValue)
                            {
                                Console.WriteLine("Incorrect age, try again");
                                Console.Write("Input age: ");
                                n4 = Console.ReadLine();
                            }
                            appUsers[appUsers.Count - 1].Age = int.Parse(n4);
                            break;
                        case ConsoleKey.D5:
                            Console.Clear();
                            Console.WriteLine("Input city: ");
                            string n5 = Console.ReadLine();
                            while (!Regex.IsMatch(n5, cityPattern) || n5 == null || String.IsNullOrEmpty(n5))
                            {
                                Console.WriteLine("Incorrect city, try again");
                                Console.Write("Input city: ");
                                n5 = Console.ReadLine();
                            }
                            appUsers[appUsers.Count - 1].City = n5;
                            break;
                        case ConsoleKey.D6:
                            Console.Clear();
                            Console.WriteLine("Input premium status[true/false]: ");
                            string n6 = Console.ReadLine();
                            while (!bool.TryParse(n6, out bool z) || n6 == null || String.IsNullOrEmpty(n6))
                            {
                                Console.WriteLine("Incorrect premium status, try again");
                                Console.Write("Input premium status: ");
                                n6 = Console.ReadLine();
                            }
                            appUsers[appUsers.Count - 1].IsPremium = bool.Parse(n6);
                            break;
                        case ConsoleKey.D7:
                            Console.Clear();
                            Console.WriteLine("Input all orders, input \":\" between them(dou,ble1:dou,ble2:dou,ble3 .. :dou,bleN)");
                            string doubles = Console.ReadLine().Replace('.', ',');
                            while (!Regex.IsMatch(doubles, doublesPattern))
                            {
                                Console.WriteLine("Incorrect doubles, try again");
                                Console.WriteLine("Input all orders, input \":\" between them(dou,ble1:dou,ble2:dou,ble3 .. :dou,bleN)");
                                doubles = Console.ReadLine().Replace('.', ',');
                            }
                            string[] doublesArr = doubles.Split(':');
                            for (int i = 0; i < doublesArr.Length; i++)
                            {
                                appUsers[appUsers.Count - 1].Orders.Add(double.Parse(doublesArr[i]));
                            }
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Incorrect input");
                            Thread.Sleep(1000);
                            break;
                    }
                }
            }
            if (answer == ConsoleKey.N)
            {
                Console.Clear();
                Console.WriteLine("Filling data is over");
                Thread.Sleep(1000);
            }
        }

        internal static void FillDataFromConsoleMenu()
        {
            Console.WriteLine("You are filling data now ");
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

        internal static ConsoleKey Item()
        {
            var key = Console.ReadKey().Key;
            while (key != ConsoleKey.D1 && key != ConsoleKey.D2 && key != ConsoleKey.D3 && key != ConsoleKey.D4
                && key != ConsoleKey.D5 && key != ConsoleKey.D6 && key != ConsoleKey.D7)
            {
                Console.WriteLine("Incorrect input, try again");
                Console.Write("Which field will be selectered: ");
                key = Console.ReadKey().Key;
            }
            return key;
        }

        internal static void Welcoming()
        {
            Console.WriteLine("Which field will be selectered: ");
            HelpingMethods.SelectionSortingMenu();
            Console.CursorVisible = false;
        }
    }
}
