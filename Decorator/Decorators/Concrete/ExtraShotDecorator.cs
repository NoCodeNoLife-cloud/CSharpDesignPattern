using Decorator.Components;
using Decorator.Decorators;

namespace Decorator.Decorators.Concrete
{
    /// <summary>
    /// Extra shot decorator
    /// Adds extra espresso shots to coffee
    /// </summary>
    public class ExtraShotDecorator : CoffeeDecorator
    {
        private readonly int _extraShots;

        public ExtraShotDecorator(ICoffee coffee, int extraShots = 1) : base(coffee)
        {
            _extraShots = extraShots > 0 ? extraShots : 1;
        }

        public override string Description => $"{base.Description} + {_extraShots} extra shot{(_extraShots > 1 ? "s" : "")}";
        
        public override decimal Cost => base.Cost + (_extraShots * 1.00m * GetSizeMultiplier());

        public override void Prepare()
        {
            base.Prepare();
            Console.WriteLine($"Adding {_extraShots} extra espresso shot{(_extraShots > 1 ? "s" : "")} to coffee");
        }
    }
}