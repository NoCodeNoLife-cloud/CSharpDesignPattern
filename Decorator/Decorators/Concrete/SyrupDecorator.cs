using Decorator.Components;
using Decorator.Decorators;

namespace Decorator.Decorators.Concrete
{
    /// <summary>
    /// Syrup decorator
    /// Adds flavored syrup to coffee
    /// </summary>
    public class SyrupDecorator : CoffeeDecorator
    {
        private readonly SyrupFlavor _flavor;

        public SyrupDecorator(ICoffee coffee, SyrupFlavor flavor) : base(coffee)
        {
            _flavor = flavor;
        }

        public override string Description => $"{base.Description} with {_flavor} syrup";
        
        public override decimal Cost => base.Cost + GetSyrupCost();

        public override void Prepare()
        {
            base.Prepare();
            Console.WriteLine($"Adding {_flavor} syrup to coffee");
        }

        private decimal GetSyrupCost()
        {
            return _flavor switch
            {
                SyrupFlavor.Vanilla => 0.75m * GetSizeMultiplier(),
                SyrupFlavor.Caramel => 0.75m * GetSizeMultiplier(),
                SyrupFlavor.Hazelnut => 0.80m * GetSizeMultiplier(),
                SyrupFlavor.Chocolate => 0.80m * GetSizeMultiplier(),
                SyrupFlavor.Mocha => 0.85m * GetSizeMultiplier(),
                _ => 0.75m * GetSizeMultiplier()
            };
        }
    }

    /// <summary>
    /// Syrup flavor enumeration
    /// </summary>
    public enum SyrupFlavor
    {
        Vanilla,
        Caramel,
        Hazelnut,
        Chocolate,
        Mocha
    }
}