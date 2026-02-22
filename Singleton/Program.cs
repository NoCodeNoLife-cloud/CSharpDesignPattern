using Singleton.Application;

namespace Singleton
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Singleton Pattern Implementation Examples ===\n");
            
            try
            {
                var demoManager = new SingletonDemoManager();
                demoManager.RunAllDemos();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\n=== Demo Completed Successfully ===");
            Console.ReadKey();
        }
    }
}