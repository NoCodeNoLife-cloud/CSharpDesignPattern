using Decorator.Components;
using Decorator.Components.Concrete;
using Decorator.Decorators.Concrete;

namespace Decorator.Services
{
    /// <summary>
    /// Coffee shop service
    /// Demonstrates decorator pattern usage in a real scenario
    /// </summary>
    public class CoffeeShopService
    {
        /// <summary>
        /// Creates a customized coffee order
        /// </summary>
        public ICoffee CreateCustomCoffee(
            CoffeeType coffeeType,
            CoffeeSize size = CoffeeSize.Medium,
            MilkType? milk = null,
            SyrupFlavor? syrup = null,
            FoamType? foam = null,
            int extraShots = 0)
        {
            // Start with base coffee
            ICoffee coffee = coffeeType switch
            {
                CoffeeType.Espresso => new Espresso(size),
                CoffeeType.Americano => new Americano(size),
                _ => new Espresso(size)
            };

            // Apply decorators in order
            if (milk.HasValue)
            {
                coffee = new MilkDecorator(coffee, milk.Value);
            }

            if (syrup.HasValue)
            {
                coffee = new SyrupDecorator(coffee, syrup.Value);
            }

            if (foam.HasValue)
            {
                coffee = new FoamDecorator(coffee, foam.Value);
            }

            if (extraShots > 0)
            {
                coffee = new ExtraShotDecorator(coffee, extraShots);
            }

            return coffee;
        }

        /// <summary>
        /// Displays coffee order details
        /// </summary>
        public void DisplayOrder(ICoffee coffee)
        {
            Console.WriteLine("\n=== Coffee Order Details ===");
            Console.WriteLine($"Description: {coffee.Description}");
            Console.WriteLine($"Size: {coffee.Size}");
            Console.WriteLine($"Total Cost: ${coffee.Cost:F2}");
            Console.WriteLine("Preparation Steps:");
            coffee.Prepare();
            Console.WriteLine("===========================\n");
        }

        /// <summary>
        /// Processes payment for coffee order
        /// </summary>
        public bool ProcessPayment(ICoffee coffee, decimal amountPaid)
        {
            var totalCost = coffee.Cost;
            if (amountPaid >= totalCost)
            {
                var change = amountPaid - totalCost;
                Console.WriteLine($"Payment successful! Total: ${totalCost:F2}, Paid: ${amountPaid:F2}, Change: ${change:F2}");
                return true;
            }
            else
            {
                Console.WriteLine($"Insufficient payment. Total: ${totalCost:F2}, Paid: ${amountPaid:F2}");
                return false;
            }
        }

        /// <summary>
        /// Gets menu items with prices
        /// </summary>
        public void DisplayMenu()
        {
            Console.WriteLine("\n=== Coffee Shop Menu ===");
            Console.WriteLine("Base Coffees:");
            Console.WriteLine($"  Espresso: $2.50");
            Console.WriteLine($"  Americano: $3.00");
            Console.WriteLine("\nAdd-ons:");
            Console.WriteLine($"  Milk (Regular/Oat/Almond/Soy): $0.50-$0.75");
            Console.WriteLine($"  Syrup (Vanilla/Caramel/Hazelnut/Chocolate/Mocha): $0.75-$0.85");
            Console.WriteLine($"  Foam (Regular/Velvet/Artistic): $0.40-$0.75");
            Console.WriteLine($"  Extra Shot: $1.00 each");
            Console.WriteLine($"  Size Multiplier: Small (0.8x), Medium (1.0x), Large (1.3x)");
            Console.WriteLine("========================\n");
        }
    }

    /// <summary>
    /// Coffee type enumeration
    /// </summary>
    public enum CoffeeType
    {
        Espresso,
        Americano
    }
}