using FactoryMethod.Products;

namespace FactoryMethod.Factories
{
    /// <summary>
    /// Simple factory - Static factory method implementation
    /// Provides centralized product creation
    /// </summary>
    public static class SimpleProductFactory
    {
        /// <summary>
        /// Creates product based on product type
        /// </summary>
        /// <param name="productType">Type of product to create</param>
        /// <returns>IProduct instance</returns>
        public static IProduct CreateProduct(string productType)
        {
            return productType.ToLower() switch
            {
                "electronic" => new ElectronicProduct(
                    name: "Smartphone",
                    price: 699.99m,
                    brand: "MobileTech",
                    model: "MT-X1",
                    isWireless: true
                ),
                "clothing" => new ClothingProduct(
                    name: "Designer Jeans",
                    price: 89.99m,
                    size: "32x32",
                    color: "Dark Wash",
                    material: "Denim Cotton Blend"
                ),
                "food" => new FoodProduct(
                    name: "Whole Grain Bread",
                    price: 2.49m,
                    expiryDate: DateTime.Now.AddDays(7),
                    weight: 500,
                    isOrganic: false
                ),
                _ => throw new ArgumentException($"Unknown product type: {productType}")
            };
        }

        /// <summary>
        /// Creates product with custom parameters
        /// </summary>
        public static IProduct CreateCustomElectronic(string name, decimal price, string brand, string model)
        {
            return new ElectronicProduct(name, price, brand, model);
        }

        public static IProduct CreateCustomClothing(string name, decimal price, string size, string color, string material)
        {
            return new ClothingProduct(name, price, size, color, material);
        }

        public static IProduct CreateCustomFood(string name, decimal price, DateTime expiryDate, double weight)
        {
            return new FoodProduct(name, price, expiryDate, weight);
        }
    }
}