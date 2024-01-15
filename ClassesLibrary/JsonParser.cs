namespace ClassesLibrary
{
    public static class JsonParser
    {
        private static string file_path;

        public static AppUser[] ReadJson(string path)
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
    }
}
