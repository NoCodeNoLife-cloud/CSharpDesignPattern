using Decorator.Components;
using Decorator.Decorators;

namespace Decorator.Decorators.Concrete
{
    /// <summary>
    /// Milk decorator
    /// Adds milk to coffee with additional cost
    /// </summary>
    public class MilkDecorator : CoffeeDecorator
    {
        private readonly MilkType _milkType;

        public MilkDecorator(ICoffee coffee, MilkType milkType = MilkType.Regular) : base(coffee)
        {
            _milkType = milkType;
        }

        public override string Description => $"{base.Description} with {_milkType} milk";
        
        public override decimal Cost => base.Cost + GetMilkCost();

        public override void Prepare()
        {
            base.Prepare();
            Console.WriteLine($"Adding {_milkType} milk to coffee");
        }

        private decimal GetMilkCost()
        {
            return _milkType switch
            {
                MilkType.Regular => 0.50m * GetSizeMultiplier(),
                MilkType.Oat => 0.75m * GetSizeMultiplier(),
                MilkType.Almond => 0.75m * GetSizeMultiplier(),
                MilkType.Soy => 0.60m * GetSizeMultiplier(),
                _ => 0.50m * GetSizeMultiplier()
            };
        }
    }

    /// <summary>
    /// Milk type enumeration
    /// </summary>
    public enum MilkType
    {
        Regular,
        Oat,
        Almond,
        Soy
    }
}