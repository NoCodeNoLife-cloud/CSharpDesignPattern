namespace FactoryMethod.Products
{
    /// <summary>
    /// Product interface
    /// Defines the common interface for all products
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// Gets product name
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets product price
        /// </summary>
        decimal Price { get; }
        
        /// <summary>
        /// Gets product category
        /// </summary>
        string Category { get; }
        
        /// <summary>
        /// Displays product information
        /// </summary>
        void DisplayInfo();
    }
}