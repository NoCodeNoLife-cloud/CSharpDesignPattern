namespace Decorator.Components
{
    /// <summary>
    /// Coffee component interface
    /// Defines the base functionality for coffee beverages
    /// </summary>
    public interface ICoffee
    {
        /// <summary>
        /// Gets the description of the coffee
        /// </summary>
        string Description { get; }
        
        /// <summary>
        /// Gets the cost of the coffee
        /// </summary>
        decimal Cost { get; }
        
        /// <summary>
        /// Gets the size of the coffee
        /// </summary>
        CoffeeSize Size { get; }
        
        /// <summary>
        /// Prepares the coffee
        /// </summary>
        void Prepare();
    }

    /// <summary>
    /// Coffee size enumeration
    /// </summary>
    public enum CoffeeSize
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }
}