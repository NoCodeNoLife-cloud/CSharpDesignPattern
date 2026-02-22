using Decorator.Components;

namespace Decorator.Components.Concrete
{
    /// <summary>
    /// Americano concrete component
    /// Diluted espresso with hot water
    /// </summary>
    public class Americano : ICoffee
    {
        public string Description => "Americano";
        public decimal Cost => 3.00m;
        public CoffeeSize Size { get; set; } = CoffeeSize.Medium;

        public Americano()
        {
        }

        public Americano(CoffeeSize size)
        {
            Size = size;
        }

        public void Prepare()
        {
            Console.WriteLine($"Preparing {Description} ({Size}) - Adding hot water to espresso");
        }
    }
}