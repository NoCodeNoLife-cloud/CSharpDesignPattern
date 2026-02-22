using FactoryMethod.Products;

namespace FactoryMethod.Factories
{
    /// <summary>
    /// Creator abstract class
    /// Defines the factory method interface
    /// </summary>
    public abstract class ProductFactory
    {
        /// <summary>
        /// Factory method - creates product instances
        /// </summary>
        /// <returns>IProduct instance</returns>
        public abstract IProduct CreateProduct();

        /// <summary>
        /// Gets factory name
        /// </summary>
        public abstract string FactoryName { get; }

        /// <summary>
        /// Creates product and displays information
        /// </summary>
        public void CreateAndDisplayProduct()
        {
            var product = CreateProduct();
            Console.WriteLine($"\n--- Created by {FactoryName} ---");
            product.DisplayInfo();
        }
    }
}