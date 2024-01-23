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

        public static List<AppUser> ReadJson(string path)
        {
            throw new NotImplementedException();
        }

        public static void WriteJson()
        {
            throw new NotImplementedException();
        }

        public static void GetFilePath()
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

        public static bool CheckJsonCorrectness()
        {
            if (UserDataCorrectness() == false)
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

        private static bool UserDataCorrectness()
        {
            StreamReader jsonFile = new StreamReader(file_path);
            string currentLine;
            while ((currentLine = jsonFile.ReadLine()) != null)
            {
                if (currentLine.Trim() == "{")
                {
                    for (int i = 0; i < 7; i++)
                    {
                        currentLine = jsonFile.ReadLine().Trim(' ', ',').Replace("\"", "");
                        if (currentLine == null)
                        {
                            return false;
                        }
                        string[] currentItem = currentLine.Split(':');
                        if (currentItem.Length == 0) return false;
                        switch(currentItem[0])
                        {
                            case "customer_id":
                                if (!int.TryParse(currentItem[1].Trim(), out int value1))
                                {
                                    Console.WriteLine("Incorrect id");
                                    return false;
                                }
                                break;
                            case "name":
                                if (!Regex.IsMatch(currentItem[1].Trim(), namePattern))
                                {
                                    Console.WriteLine("Incorrect name");
                                    return false;
                                }
                                break;
                            case "email":
                                if (!Regex.IsMatch(currentItem[1].Trim(), emailPattern))
                                {
                                    Console.WriteLine("Incorrect email");
                                    return false;
                                }
                                break;
                            case "age":
                                if (!int.TryParse(currentItem[1].Trim(), out int value2))
                                {
                                    Console.WriteLine("Incorrect age");
                                    return false;
                                }
                                break;
                            case "city":
                                if (!Regex.IsMatch(currentItem[1].Trim(), cityPattern))
                                {
                                    Console.WriteLine("Incorrect city");
                                    return false;
                                }
                                break;
                            case "is_premium":
                                if (!bool.TryParse(currentItem[1].Trim(), out bool value3))
                                {
                                    Console.WriteLine("Incorrect premium status");
                                    return false;
                                }
                                break;
                            case "orders":
                                if (currentItem[1].Trim() != "[")
                                {
                                    Console.WriteLine("No offers in order list");
                                    return false;
                                }
                                else
                                {
                                    while ((currentLine = jsonFile.ReadLine().Trim()) != "]")
                                    {
                                        if (!double.TryParse(currentLine.TrimEnd(',').Trim().Replace('.',','), out double value4))
                                        {
                                            Console.WriteLine("Incorrcet price");
                                            return false;
                                        }
                                    }
                                }
                                break;

                            default: return false;
                        } 
                    }
                }
            }
            return true;
        }
    }
}
//C:\Users\murd3rRRR\source\repos\KDZ1_Isaev_Kirill\Smth\bin\Debug\net6.0\data_8V.JSON
