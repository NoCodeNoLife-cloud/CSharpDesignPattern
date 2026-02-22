// See https://aka.ms/new-console-template for more information
using Builder.Builders;
using Builder.Directors;
using Builder.Products;

namespace Builder
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Builder Pattern Implementation Examples ===\n");

            try
            {
                // Demonstrate traditional builder pattern
                DemonstrateTraditionalBuilder();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate fluent builder pattern
                DemonstrateFluentBuilder();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate director usage
                DemonstrateDirector();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate builder comparison
                DemonstrateBuilderComparison();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\n=== Builder Pattern Demo Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates traditional builder pattern with different builders
        /// </summary>
        private static void DemonstrateTraditionalBuilder()
        {
            Console.WriteLine("1. Traditional Builder Pattern Demo");
            Console.WriteLine("==================================");
            
            // Gaming computer construction
            Console.WriteLine("\n--- Building Gaming Computer ---");
            var gamingBuilder = new GamingComputerBuilder();
            var gamingComputer = gamingBuilder
                .SetCPU("AMD Ryzen 7 5800X")
                .SetRAM("32GB DDR4 RGB")
                .SetStorage("1TB NVMe SSD")
                .SetGraphicsCard("NVIDIA RTX 3080")
                .SetMotherboard("X570 ATX Gaming")
                .SetPowerSupply("850W Modular")
                .SetCase("Full Tower Tempered Glass")
                .AddAccessory("RGB Fans")
                .AddAccessory("Gaming Keyboard")
                .GetComputer();
            
            gamingComputer.DisplayConfiguration();
            Console.WriteLine($"Estimated Price: ${gamingComputer.CalculateEstimatedPrice():F2}");
        }

        /// <summary>
        /// Demonstrates fluent builder pattern
        /// </summary>
        private static void DemonstrateFluentBuilder()
        {
            Console.WriteLine("\n2. Fluent Builder Pattern Demo");
            Console.WriteLine("===============================");
            
            // Fluent interface construction
            Console.WriteLine("\n--- Building Developer Workstation (Fluent) ---");
            var developerPC = FluentComputerBuilder.Create()
                .WithCPU("Intel Core i9-12900K")
                .WithRAM("64GB DDR5")
                .WithStorage("2TB NVMe SSD")
                .WithGraphicsCard("RTX 4090")
                .Build();
            
            developerPC.DisplayConfiguration();
            Console.WriteLine($"Estimated Price: ${developerPC.CalculateEstimatedPrice():F2}");
        }

        /// <summary>
        /// Demonstrates director pattern usage
        /// </summary>
        private static void DemonstrateDirector()
        {
            Console.WriteLine("\n3. Director Pattern Demo");
            Console.WriteLine("========================");
            
            // Using director with gaming builder
            Console.WriteLine("\n--- Director with Gaming Builder ---");
            var gamingDirector = new ComputerDirector(new GamingComputerBuilder());
            
            var basicGamingPC = gamingDirector.BuildBasicComputer();
            basicGamingPC.DisplayConfiguration();
            Console.WriteLine($"Price: ${basicGamingPC.CalculateEstimatedPrice():F2}");
        }

        /// <summary>
        /// Demonstrates comparison between different builder approaches
        /// </summary>
        private static void DemonstrateBuilderComparison()
        {
            Console.WriteLine("\n4. Builder Pattern Comparison");
            Console.WriteLine("==============================");
            
            Console.WriteLine("Builder Pattern Benefits:");
            Console.WriteLine("• Separates construction from representation");
            Console.WriteLine("• Allows different representations using same construction process");
            Console.WriteLine("• Provides flexible object creation");
        }
    }
}