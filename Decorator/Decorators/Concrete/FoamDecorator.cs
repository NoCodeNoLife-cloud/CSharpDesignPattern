using Decorator.Components;
using Decorator.Decorators;

namespace Decorator.Decorators.Concrete
{
    /// <summary>
    /// Foam decorator
    /// Adds milk foam to coffee
    /// </summary>
    public class FoamDecorator : CoffeeDecorator
    {
        private readonly FoamType _foamType;

        public FoamDecorator(ICoffee coffee, FoamType foamType = FoamType.Regular) : base(coffee)
        {
            _foamType = foamType;
        }

        public override string Description => $"{base.Description} with {_foamType} foam";
        
        public override decimal Cost => base.Cost + GetFoamCost();

        public override void Prepare()
        {
            base.Prepare();
            Console.WriteLine($"Adding {_foamType} foam to coffee");
        }

        private decimal GetFoamCost()
        {
            return _foamType switch
            {
                FoamType.Regular => 0.40m * GetSizeMultiplier(),
                FoamType.Velvet => 0.50m * GetSizeMultiplier(),
                FoamType.Artistic => 0.75m * GetSizeMultiplier(),
                _ => 0.40m * GetSizeMultiplier()
            };
        }
    }

    /// <summary>
    /// Foam type enumeration
    /// </summary>
    public enum FoamType
    {
        Regular,
        Velvet,
        Artistic
    }
}