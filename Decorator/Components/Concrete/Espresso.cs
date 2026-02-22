using Decorator.Components;

namespace Decorator.Components.Concrete
{
    /// <summary>
    /// Espresso concrete component
    /// Basic coffee implementation
    /// </summary>
    public class Espresso : ICoffee
    {
        public string Description => "Espresso";
        public decimal Cost => 2.50m;
        public CoffeeSize Size { get; set; } = CoffeeSize.Medium;

        public Espresso()
        {
        }

        public Espresso(CoffeeSize size)
        {
            Size = size;
        }

        public void Prepare()
        {
            Console.WriteLine($"Preparing {Description} ({Size}) - Extracting concentrated coffee shot");
        }
    }
}