using ClassesLibrary;
//C:\Users\murd3rRRR\source\repos\KDZ1_Isaev_Kirill\Smth\bin\Debug\net6.0\data_8V.JSON
namespace Smth
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                do
                {
                    
                    Menu.StartComputer();
                    Console.WriteLine();
                    Console.Clear();
                    HelpingMethods.PrintWithColor("Do you want to start app again?[y,n]", ConsoleColor.Yellow);
                } while (Console.ReadKey().Key != ConsoleKey.N);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                HelpingMethods.PrintWithColor(ex.Message, ConsoleColor.Red);
            }
            catch (NullReferenceException ex)
            {
                HelpingMethods.PrintWithColor(ex.Message, ConsoleColor.Red);
            }
            catch (ArgumentNullException ex)
            {
                HelpingMethods.PrintWithColor(ex.Message, ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                HelpingMethods.PrintWithColor(ex.Message, ConsoleColor.Red);
            }
        }
    }
}