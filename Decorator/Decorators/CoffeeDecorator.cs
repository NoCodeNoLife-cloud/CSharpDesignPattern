using Decorator.Components;

namespace Decorator.Decorators
{
    /// <summary>
    /// Coffee decorator base class
    /// Implements decorator pattern for coffee components
    /// </summary>
    public abstract class CoffeeDecorator : ICoffee
    {
        protected readonly ICoffee _coffee;

        protected CoffeeDecorator(ICoffee coffee)
        {
            _coffee = coffee ?? throw new ArgumentNullException(nameof(coffee));
        }

        public virtual string Description => _coffee.Description;
        public virtual decimal Cost => _coffee.Cost;
        public virtual CoffeeSize Size => _coffee.Size;

        public virtual void Prepare()
        {
            _coffee.Prepare();
        }

        /// <summary>
        /// Calculates size-based price multiplier
        /// </summary>
        protected decimal GetSizeMultiplier()
        {
            return Size switch
            {
                CoffeeSize.Small => 0.8m,
                CoffeeSize.Medium => 1.0m,
                CoffeeSize.Large => 1.3m,
                _ => 1.0m
            };
        }
    }
}