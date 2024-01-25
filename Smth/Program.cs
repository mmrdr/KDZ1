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
                    Console.WriteLine("Do you want to start app again?[y,n]");
                } while (Console.ReadKey().Key != ConsoleKey.N);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}