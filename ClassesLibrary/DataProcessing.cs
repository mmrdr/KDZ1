
namespace ClassesLibrary
{
    public class DataProcessing
    {
        internal static List<AppUser> SelectedAppUsers;

        public static List<AppUser> Selecting()
        {
            if (HelpingMethods.currentAppUsers == null)
            {
                Console.WriteLine("No data error, try again");
                return new List<AppUser>(0);
            }
            HelpingMethods.Welcoming();
            SelectedAppUsers = new List<AppUser>();
            switch (HelpingMethods.Item())
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    Console.Write("Input value for selecting: ");
                    string n1 = Console.ReadLine();                  
                    while (n1 == null || String.IsNullOrEmpty(n1) || !int.TryParse(n1, out int x1))
                    {
                        Console.WriteLine("Incorrect value, try again");
                        Console.Write("Input value for selecting: ");
                        n1 = Console.ReadLine();
                    }
                    foreach (AppUser user in HelpingMethods.currentAppUsers)
                    {
                        if (int.Parse(n1) == user.CustomerId)
                        {
                            SelectedAppUsers.Add(user);
                        }
                    }
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    Console.Write("Input value for selecting: ");
                    string n2 = Console.ReadLine();                  
                    while (n2 == null || String.IsNullOrEmpty(n2))
                    {
                        Console.WriteLine("Incorrect value, try again");
                        Console.Write("Input value for selecting: ");
                        n2 = Console.ReadLine();
                    }
                    foreach (AppUser user in HelpingMethods.currentAppUsers)
                    {
                        if (n2 == user.Name)
                        {
                            SelectedAppUsers.Add(user);
                        }
                    }
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    Console.Write("Input value for selecting: ");
                    string n3 = Console.ReadLine();
                    
                    while (n3 == null || String.IsNullOrEmpty(n3))
                    {
                        Console.WriteLine("Incorrect value, try again");
                        Console.Write("Input value for selecting: ");
                        n3 = Console.ReadLine();
                    }
                    foreach (AppUser user in HelpingMethods.currentAppUsers)
                    {
                        if (n3 == user.Email)
                        {
                            SelectedAppUsers.Add(user);
                        }
                    }
                    break;
                case ConsoleKey.D4:
                    Console.Clear();
                    Console.Write("Input value for selecting: ");
                    string n4 = Console.ReadLine();
                    
                    while (n4 == null || String.IsNullOrEmpty(n4) || !int.TryParse(n4, out int y))
                    {
                        Console.WriteLine("Incorrect value, try again");
                        Console.Write("Input value for selecting: ");
                        n4 = Console.ReadLine();
                    }
                    foreach (AppUser user in HelpingMethods.currentAppUsers)
                    {
                        if (int.Parse(n4) == user.Age)
                        {
                            SelectedAppUsers.Add(user);
                        }
                    }
                    break;
                case ConsoleKey.D5:
                    Console.Clear();
                    Console.Write("Input value for selecting: ");
                    string n5 = Console.ReadLine();
                    
                    while (n5 == null || String.IsNullOrEmpty(n5))
                    {
                        Console.WriteLine("Incorrect value, try again");
                        Console.Write("Input value for selecting: ");
                        n5 = Console.ReadLine();
                    }
                    foreach (AppUser user in HelpingMethods.currentAppUsers)
                    {
                        if (n5 == user.City)
                        {
                            SelectedAppUsers.Add(user);
                        }
                    }
                    break;
                case ConsoleKey.D6:
                    Console.Clear();
                    Console.Write("Input value for selecting: ");
                    string n6 = Console.ReadLine();
                    
                    while (n6 == null || String.IsNullOrEmpty(n6) || !bool.TryParse(n6, out bool z))
                    {
                        Console.WriteLine("Incorrect value, try again");
                        Console.Write("Input value for selecting: ");
                        n6 = Console.ReadLine();
                    }
                    foreach (AppUser user in HelpingMethods.currentAppUsers)
                    {
                        if (bool.Parse(n6) == user.IsPremium)
                        {
                            SelectedAppUsers.Add(user);
                        }
                    }
                    break;
                case ConsoleKey.D7:
                    Console.Clear();
                    Console.Write("Input value for selecting: ");
                    string n7 = Console.ReadLine();
                    
                    while (n7 == null || String.IsNullOrEmpty(n7) || !double.TryParse(n7, out double k))
                    {
                        Console.WriteLine("Incorrect value, try again");
                        Console.Write("Input value for selecting: ");
                        n7 = Console.ReadLine();
                    }
                    foreach (AppUser user in HelpingMethods.currentAppUsers)
                    {
                        foreach(double order in user.Orders)
                        {
                            if (double.Parse(n7) == order)
                            {
                                SelectedAppUsers.Add(user);
                            }
                        }
                    }
                    break;
            }
            if (SelectedAppUsers.Count == 0)
            {
                Console.WriteLine("Nothing was found");
                return new List<AppUser>(0);
            }
            HelpingMethods.currentAppUsers = SelectedAppUsers;
            return SelectedAppUsers;
        }

        public static List<AppUser> Sorting()
        {           
            if (HelpingMethods.currentAppUsers == null)
            {
                Console.WriteLine("No data error, try again");
                return new List<AppUser>(0);
            }
            HelpingMethods.Welcoming();
            var tempSelectedUsers = new AppUser[HelpingMethods.currentAppUsers.Count()];
            switch (HelpingMethods.Item())
            {
                case ConsoleKey.D1:
                    HelpingMethods.currentAppUsers.CopyTo(tempSelectedUsers, 0);
                    tempSelectedUsers = tempSelectedUsers.OrderBy(x => x.CustomerId).ToArray();
                    break;
                case ConsoleKey.D2:
                    HelpingMethods.currentAppUsers.CopyTo(tempSelectedUsers, 0);
                    tempSelectedUsers = tempSelectedUsers.OrderBy(x => x.Name).ToArray();
                    break;
                case ConsoleKey.D3:
                    HelpingMethods.currentAppUsers.CopyTo(tempSelectedUsers, 0);
                    tempSelectedUsers = tempSelectedUsers.OrderBy(x => x.Email).ToArray();
                    break;
                case ConsoleKey.D4:
                    HelpingMethods.currentAppUsers.CopyTo(tempSelectedUsers, 0);
                    tempSelectedUsers = tempSelectedUsers.OrderBy(x => x.Age).ToArray();
                    break;
                case ConsoleKey.D5:
                    HelpingMethods.currentAppUsers.CopyTo(tempSelectedUsers, 0);
                    tempSelectedUsers = tempSelectedUsers.OrderBy(x => x.City).ToArray();
                    break;
                case ConsoleKey.D6:
                    HelpingMethods.currentAppUsers.CopyTo(tempSelectedUsers, 0);
                    tempSelectedUsers = tempSelectedUsers.OrderBy(x => x.IsPremium).ToArray();
                    break;
                case ConsoleKey.D7:
                    HelpingMethods.currentAppUsers.CopyTo(tempSelectedUsers, 0);
                    tempSelectedUsers = tempSelectedUsers.OrderBy(x => x.Orders.Sum()).ToArray();
                    break;
            }
            SelectedAppUsers = new List<AppUser>(tempSelectedUsers.Length);
            foreach (var user in tempSelectedUsers)
            {
                SelectedAppUsers.Add(user);
            }
            HelpingMethods.currentAppUsers = SelectedAppUsers;
            return SelectedAppUsers;
        }
    }
}
