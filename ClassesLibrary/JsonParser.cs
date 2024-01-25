﻿using System.Text.RegularExpressions;

namespace ClassesLibrary
{
    public static class JsonParser
    {
        public static List<AppUser> ReadJson()
        {
            HelpingMethods.GetFilePath();
            int key = 0;
            int value = 1;
            while (!HelpingMethods.CheckJsonCorrectness())
            {
                Console.WriteLine("Incorrect file, try another path");
                HelpingMethods.file_path = Console.ReadLine();
            }
            StreamReader jsonFile = new StreamReader(HelpingMethods.file_path);
            var appUsers = new List<AppUser>();
            string currentLine;
            int currentUserIndex = -1;
            while((currentLine = jsonFile.ReadLine()) != null)
            {
                currentUserIndex++;
                appUsers.Add(new AppUser());
                while (currentLine.Trim(' ',',') != "}")
                {                   
                    try
                    {
                        currentLine = jsonFile.ReadLine();
                        if (currentLine == null)
                        {
                            appUsers.RemoveAt(appUsers.Count-1);
                            break;
                        }

                        string[] currentItem = currentLine.Trim(' ', ',').Replace("\"", "").Split(':');
                        switch (currentItem[key])
                        {
                            case "customer_id":
                                appUsers[currentUserIndex].CustomerId = int.Parse(currentItem[value].Trim(' ', ','));
                                break;
                            case "name":
                                appUsers[currentUserIndex].Name = currentItem[value].Trim(' ');
                                break;
                            case "email":
                                appUsers[currentUserIndex].Email = currentItem[value].Trim(' ');
                                break;
                            case "age":
                                appUsers[currentUserIndex].Age = int.Parse(currentItem[value].Trim(' ', ','));
                                break;
                            case "city":
                                appUsers[currentUserIndex].City = currentItem[value].Trim(' ');
                                break;
                            case "is_premium":
                                appUsers[currentUserIndex].IsPremium = bool.Parse(currentItem[value].Trim(' ', ','));
                                break;
                            case "orders":
                                while ((currentLine = jsonFile.ReadLine().Trim().TrimEnd(',')) != "]")
                                {
                                    string currentOrder = currentLine.Trim(' ').TrimEnd(',').Replace('.',',');
                                    appUsers[currentUserIndex].Orders.Add(double.Parse(currentOrder));
                                }
                                break;
                        }                               
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine(currentUserIndex + " " + ex.Message + " " + "in ReadJson");
                    }
                    catch(ArgumentOutOfRangeException ex)
                    {
                       Console.WriteLine(currentUserIndex + " " + ex.Message + " " + "in ReadJson");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(currentUserIndex + " " + ex.Message + " " + "in ReadJson");
                    }
                }
            }
            HelpingMethods.currentAppUsers = appUsers;
            return appUsers;
        }

        public static List<AppUser> ReadJsonFromConsole()
        {
            var appUsers = new List<AppUser>();
            do
            {
                Console.Clear();
                Console.WriteLine("Starting system...");
                HelpingMethods.FillAppUsersFields(appUsers);
                Console.WriteLine("Data uploaded successfully");
                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine("If you want to fill another user press ENTER\n" +
                    "Else any another input");

            } while (Console.ReadKey().Key == ConsoleKey.Enter);
            HelpingMethods.currentAppUsers = appUsers;
            return appUsers;           
        }

        public static void WriteJson(List<AppUser> appUsers)
        {
            string tab = HelpingMethods.TAB;
            string JsonFileView = String.Empty;
            JsonFileView += "[\n";
            for (int i = 0; i < appUsers.Count; i++)
            {
                AppUser appUser = appUsers[i];
                JsonFileView += $"{tab}{{\n";
                tab += HelpingMethods.TAB;
                JsonFileView += $"{tab}\"customer_id\": {appUser.CustomerId}, \n";
                JsonFileView += $"{tab}\"name\": {appUser.Name}, \n";
                JsonFileView += $"{tab}\"email\": {appUser.Email}, \n";
                JsonFileView += $"{tab}\"age\": {appUser.Age}, \n";
                JsonFileView += $"{tab}\"city\": {appUser.City}, \n";
                JsonFileView += $"{tab}\"is_premuim\": {appUser.IsPremium}, \n";
                JsonFileView += $"{tab}\"orders\": [\n";
                tab += HelpingMethods.TAB;
                for (int j = 0; j < appUser.Orders.Count(); j++)
                {
                    JsonFileView += $"{tab}{appUser.Orders[j]}, \n";
                    if (j ==  appUser.Orders.Count - 1)
                    {
                        JsonFileView += $"{tab}{appUser.Orders[j]} \n";
                    }
                }
                tab = tab.Remove(tab.Length - HelpingMethods.TAB.Length, HelpingMethods.TAB.Length);
                JsonFileView += $"{tab}]\n";
                tab = tab.Remove(tab.Length - HelpingMethods.TAB.Length, HelpingMethods.TAB.Length);
                if (i != appUsers.Count - 1)
                {
                    JsonFileView += $"{tab}}},\n";
                }
                else
                {
                    JsonFileView += $"{tab}}}\n";
                }
            }
            JsonFileView += "]";
            Console.WriteLine(JsonFileView);          
        }
    }
}
//C:\Users\murd3rRRR\source\repos\KDZ1_Isaev_Kirill\Smth\bin\Debug\net6.0\data_8V.JSON
