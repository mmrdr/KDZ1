using System.Text.RegularExpressions;

namespace ClassesLibrary
{
    public static class JsonParser
    {
        private static string file_path;
        private static string namePattern = @"^[\p{L}\s.'-]+$";
        private static string emailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
        private static string cityPattern = @"\b\p{Lu}\p{L}*\b";

        public static List<AppUser> ReadJson()
        {
            GetFilePath();
            int key = 0;
            int value = 1;
            while (CheckJsonCorrectness() != true)
            {
                Console.WriteLine("Incorrect file, try another path");
                file_path = Console.ReadLine();
            }
            StreamReader jsonFile = new StreamReader(file_path);
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
            return appUsers;
        }

        public static void WriteJson()
        {
            throw new NotImplementedException();
        }

        private static void GetFilePath()
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

        private static bool CheckJsonCorrectness()
        {
            if (CheckUserDataCorrectness() == false)
            {
                return false;
            }
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

        private static bool CheckUserDataCorrectness()
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
                            switch(currentItem[key])
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
    }
}
//C:\Users\murd3rRRR\source\repos\KDZ1_Isaev_Kirill\Smth\bin\Debug\net6.0\data_8V.JSON
