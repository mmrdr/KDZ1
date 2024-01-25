using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassesLibrary
{
    /// <summary>
    /// Important auxiliary methods that verify the correctness of data, etc.
    /// </summary>
    public partial class HelpingMethods
    {
        /// <summary>
        /// Path to file.
        /// </summary>
        internal static void GetFilePath()
        {
            Console.WriteLine("Input full path to file");
            string path = Console.ReadLine();
            while (!File.Exists(path) || String.IsNullOrEmpty(path) || path.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                PrintWithColor("Wrong file path, try again", ConsoleColor.Red);
                path = Console.ReadLine();
            }
            file_path = path;
        }

        /// <summary>
        /// This method json checks file's structure for correctness.
        /// </summary>
        /// <returns></returns>
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
                PrintWithColor("Incorrect structure of brackets", ConsoleColor.Red);
                return false;
            }
            return true;
        }

        /// <summary>
        /// This method checks file's data for correctness.
        /// </summary>
        /// <returns></returns>
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
                                        PrintWithColor("Incorrect id", ConsoleColor.Red);
                                        return false;
                                    }
                                    break;
                                case "name":
                                    if (!Regex.IsMatch(currentItem[value].Trim(), namePattern))
                                    {
                                        PrintWithColor("Incorrect name", ConsoleColor.Red);
                                        return false;
                                    }
                                    break;
                                case "email":
                                    if (!Regex.IsMatch(currentItem[value].Trim(), emailPattern))
                                    {
                                        PrintWithColor("Incorrect email", ConsoleColor.Red);
                                        return false;
                                    }
                                    break;
                                case "age":
                                    if (!int.TryParse(currentItem[value].Trim(), out int value2))
                                    {
                                        PrintWithColor("Incorrect age", ConsoleColor.Red);
                                        return false;
                                    }
                                    break;
                                case "city":
                                    if (!Regex.IsMatch(currentItem[value].Trim(), cityPattern))
                                    {
                                        PrintWithColor("Incorrect city", ConsoleColor.Red);
                                        return false;
                                    }
                                    break;
                                case "is_premium":
                                    if (!bool.TryParse(currentItem[value].Trim().Replace('T','t').Replace('F','f'), out bool value3))
                                    {
                                        PrintWithColor("Incorrect premium status", ConsoleColor.Red);
                                        return false;
                                    }
                                    break;
                                case "orders":
                                    if (currentItem[value].Trim() != "[")
                                    {
                                        PrintWithColor("No offers in order list", ConsoleColor.Red);
                                        return false;
                                    }
                                    else
                                    {
                                        while ((currentLine = jsonFile.ReadLine().Trim()) != "]")
                                        {
                                            if (!double.TryParse(currentLine.TrimEnd(',').Trim().Replace('.', ','), out double value4))
                                            {
                                                PrintWithColor("Incorrect price", ConsoleColor.Red);
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
                            PrintWithColor(ex.Message + " " + "in CheckUserDataCorrectness", ConsoleColor.Red);
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// This method allows user to input data for future json file by using only console(and keyboard).
        /// </summary>
        /// <param name="appUsers"></param>
        internal static void FillAppUsersFields(List<AppUser> appUsers)
        {
            Console.WriteLine("Do you want to input data for user?[y/n]");
            var answer = Console.ReadKey().Key;
            while (answer != ConsoleKey.Y && answer != ConsoleKey.N)
            {
                PrintWithColor("Incorrect answer, try again[y,n]", ConsoleColor.Red);
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
                                PrintWithColor("Incorrect id, try again", ConsoleColor.Red);
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
                                PrintWithColor("Incorrect name, try again", ConsoleColor.Red);
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
                                PrintWithColor("Incorrect email, try again", ConsoleColor.Red);
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
                                PrintWithColor("Incorrect age, try again", ConsoleColor.Red);
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
                                PrintWithColor("Incorrect city, try again", ConsoleColor.Red);
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
                                PrintWithColor("Incorrect premium status, try again", ConsoleColor.Red);
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
                                PrintWithColor("Incorrect doubles, try again", ConsoleColor.Red);
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
                            PrintWithColor("Incorrect input", ConsoleColor.Red);
                            Thread.Sleep(1000);
                            break;
                    }
                }
            }
            if (answer == ConsoleKey.N)
            {
                Console.Clear();
                PrintWithColor("Filling data is over", ConsoleColor.Yellow);
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// This method checks user's input for correctness.  
        /// </summary>
        /// <returns></returns>
        internal static ConsoleKey Item()
        {
            var key = Console.ReadKey().Key;
            while (key != ConsoleKey.D1 && key != ConsoleKey.D2 && key != ConsoleKey.D3 && key != ConsoleKey.D4
                && key != ConsoleKey.D5 && key != ConsoleKey.D6 && key != ConsoleKey.D7)
            {
                Console.WriteLine();
                PrintWithColor("Incorrect input, try again: ", ConsoleColor.Red);
                key = Console.ReadKey().Key;
            }
            return key;
        }

        /// <summary>
        /// This method allows user to choose between two styles of writing json file.
        /// </summary>
        internal static void WriteChoose()
        {
            Console.WriteLine("You want create new file or overwrite current?");
            Console.WriteLine("1. Create new\n" +
                "2. Overwrite current\n");
            var key = Console.ReadKey().Key;
            while (key != ConsoleKey.D1 && key != ConsoleKey.D2)
            {
                PrintWithColor("Incorrect input, try again: ", ConsoleColor.Red);
                key = Console.ReadKey().Key;
            }
            switch (key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    JsonParser.CreateNewJsonFile();
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    JsonParser.WriteJsonToFile();
                    break;
            }
        }

        /// <summary>
        /// This method allows user to choose between two styles of sorting json file.
        /// </summary>
        internal static void SortChoose()
        {
            Console.WriteLine("You want usual sorting or reverse sorting?");
            Console.WriteLine("1. Usual sorting\n" +
                "2. Reverse sorting\n");
            var key = Console.ReadKey().Key;
            while (key != ConsoleKey.D1 && key != ConsoleKey.D2)
            {
                PrintWithColor("Incorrect input, try again: ", ConsoleColor.Red);
                key = Console.ReadKey().Key;
            }
            switch (key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    DataProcessing.Sorting();
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    DataProcessing.Sorting();
                    DataProcessing.SelectedAppUsers.Reverse();
                    break;
            }
        }
    }
}
