// See https://aka.ms/new-console-template for more information
using Decorator.Components;
using Decorator.Components.Concrete;
using Decorator.Decorators.Concrete;
using Decorator.Services;

namespace Decorator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Decorator Pattern Implementation Examples ===\n");

            try
            {
                var coffeeShop = new CoffeeShopService();
                
                // Display menu
                coffeeShop.DisplayMenu();
                
                // Demonstrate basic decorator usage
                DemonstrateBasicDecorators();
                
                Console.WriteLine(new string('=', 60));
                
                // Demonstrate coffee shop service
                DemonstrateCoffeeShopService(coffeeShop);
                
                Console.WriteLine(new string('=', 60));
                
                // Demonstrate complex combinations
                DemonstrateComplexCombinations();
                
                Console.WriteLine(new string('=', 60));
                
                // Demonstrate decorator benefits
                DemonstrateDecoratorBenefits();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\n=== Decorator Pattern Demo Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates basic decorator pattern usage
        /// </summary>
        private static void DemonstrateBasicDecorators()
        {
            Console.WriteLine("1. Basic Decorator Pattern Demo");
            Console.WriteLine("================================");
            
            // Basic espresso
            Console.WriteLine("\n--- Basic Espresso ---");
            ICoffee basicEspresso = new Espresso(CoffeeSize.Medium);
            Console.WriteLine($"Description: {basicEspresso.Description}");
            Console.WriteLine($"Cost: ${basicEspresso.Cost:F2}");
            basicEspresso.Prepare();
            
            // Espresso with milk
            Console.WriteLine("\n--- Espresso with Milk ---");
            ICoffee milkEspresso = new MilkDecorator(basicEspresso, MilkType.Oat);
            Console.WriteLine($"Description: {milkEspresso.Description}");
            Console.WriteLine($"Cost: ${milkEspresso.Cost:F2}");
            milkEspresso.Prepare();
            
            // Espresso with milk and syrup
            Console.WriteLine("\n--- Espresso with Milk and Caramel Syrup ---");
            ICoffee fancyEspresso = new SyrupDecorator(milkEspresso, SyrupFlavor.Caramel);
            Console.WriteLine($"Description: {fancyEspresso.Description}");
            Console.WriteLine($"Cost: ${fancyEspresso.Cost:F2}");
            fancyEspresso.Prepare();
        }

        /// <summary>
        /// Demonstrates coffee shop service usage
        /// </summary>
        private static void DemonstrateCoffeeShopService(CoffeeShopService coffeeShop)
        {
            Console.WriteLine("\n2. Coffee Shop Service Demo");
            Console.WriteLine("============================");
            
            // Create various coffee orders
            var orders = new[]
            {
                coffeeShop.CreateCustomCoffee(CoffeeType.Espresso, CoffeeSize.Small),
                coffeeShop.CreateCustomCoffee(
                    CoffeeType.Americano, 
                    CoffeeSize.Large, 
                    MilkType.Almond, 
                    SyrupFlavor.Vanilla),
                coffeeShop.CreateCustomCoffee(
                    CoffeeType.Espresso, 
                    CoffeeSize.Medium, 
                    MilkType.Regular, 
                    SyrupFlavor.Mocha, 
                    FoamType.Artistic, 
                    2)
            };
            
            // Display and process each order
            foreach (var order in orders)
            {
                coffeeShop.DisplayOrder(order);
                coffeeShop.ProcessPayment(order, order.Cost + 1.00m); // Pay with extra dollar
            }
        }

        /// <summary>
        /// Demonstrates complex decorator combinations
        /// </summary>
        private static void DemonstrateComplexCombinations()
        {
            Console.WriteLine("\n3. Complex Decorator Combinations Demo");
            Console.WriteLine("=====================================");
            
            // Build a complex coffee step by step
            Console.WriteLine("\n--- Building Complex Coffee Step by Step ---");
            
            // Start with base coffee
            ICoffee coffee = new Americano(CoffeeSize.Large);
            Console.WriteLine($"Base: {coffee.Description} - ${coffee.Cost:F2}");
            
            // Add decorators one by one
            coffee = new MilkDecorator(coffee, MilkType.Oat);
            Console.WriteLine($"With milk: {coffee.Description} - ${coffee.Cost:F2}");
            
            coffee = new SyrupDecorator(coffee, SyrupFlavor.Hazelnut);
            Console.WriteLine($"With syrup: {coffee.Description} - ${coffee.Cost:F2}");
            
            coffee = new FoamDecorator(coffee, FoamType.Velvet);
            Console.WriteLine($"With foam: {coffee.Description} - ${coffee.Cost:F2}");
            
            coffee = new ExtraShotDecorator(coffee, 1);
            Console.WriteLine($"With extra shot: {coffee.Description} - ${coffee.Cost:F2}");
            
            Console.WriteLine("\nFinal preparation:");
            coffee.Prepare();
        }

        /// <summary>
        /// Demonstrates decorator pattern benefits
        /// </summary>
        private static void DemonstrateDecoratorBenefits()
        {
            Console.WriteLine("\n4. Decorator Pattern Benefits Demo");
            Console.WriteLine("==================================");
            
            Console.WriteLine("Decorator Pattern Advantages:");
            Console.WriteLine("• Extends functionality without modifying existing code");
            Console.WriteLine("• Provides flexible alternative to subclassing");
            Console.WriteLine("• Allows combination of behaviors dynamically");
            Console.WriteLine("• Follows Open/Closed Principle");
            Console.WriteLine("• Enables runtime composition of objects");
            
            Console.WriteLine("\nComparison with inheritance:");
            Console.WriteLine("Inheritance: Creates rigid class hierarchies");
            Console.WriteLine("Decorator:   Allows dynamic behavior composition");
            
            Console.WriteLine("\nReal-world applications:");
            Console.WriteLine("• GUI components with borders, scrollbars, etc.");
            Console.WriteLine("• Stream processing with compression, encryption");
            Console.WriteLine("• Coffee shops, car customization, pizza toppings");
            Console.WriteLine("• Logging, caching, authentication wrappers");
        }
    }
}