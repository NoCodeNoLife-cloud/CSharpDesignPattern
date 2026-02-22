using FactoryMethod.Factories;

namespace FactoryMethod
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Factory Method Pattern Example ===\n");

            // Demonstrate Factory Method Pattern
            Console.WriteLine("1. Factory Method Pattern Demo:");
            DemonstrateFactoryMethod();

            Console.WriteLine("\n" + new string('=', 50) + "\n");

            // Demonstrate Simple Factory Pattern
            Console.WriteLine("2. Simple Factory Pattern Demo:");
            DemonstrateSimpleFactory();

            Console.WriteLine("\n" + new string('=', 50) + "\n");

            // Demonstrate Custom Product Creation
            Console.WriteLine("3. Custom Product Creation Demo:");
            DemonstrateCustomCreation();

            Console.WriteLine("\n=== Example Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates traditional Factory Method pattern
        /// </summary>
        private static void DemonstrateFactoryMethod()
        {
            // Create different factory instances
            ProductFactory electronicFactory = new ElectronicFactory();
            ProductFactory clothingFactory = new ClothingFactory();
            ProductFactory foodFactory = new FoodFactory();

            // Use factories to create products
            electronicFactory.CreateAndDisplayProduct();
            clothingFactory.CreateAndDisplayProduct();
            foodFactory.CreateAndDisplayProduct();
        }

        /// <summary>
        /// Demonstrates Simple Factory pattern
        /// </summary>
        private static void DemonstrateSimpleFactory()
        {
            try
            {
                // Create products using simple factory
                var electronicProduct = SimpleProductFactory.CreateProduct("electronic");
                var clothingProduct = SimpleProductFactory.CreateProduct("clothing");
                var foodProduct = SimpleProductFactory.CreateProduct("food");

                Console.WriteLine("\n--- Products created by Simple Factory ---");
                electronicProduct.DisplayInfo();
                clothingProduct.DisplayInfo();
                foodProduct.DisplayInfo();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Demonstrates custom product creation with parameters
        /// </summary>
        private static void DemonstrateCustomCreation()
        {
            // Create custom products with specific parameters
            var customLaptop = SimpleProductFactory.CreateCustomElectronic(
                name: "Gaming Laptop Pro",
                price: 1299.99m,
                brand: "GameMaster",
                model: "GM-GTX2024"
            );

            var customShirt = SimpleProductFactory.CreateCustomClothing(
                name: "Premium Dress Shirt",
                price: 49.99m,
                size: "M",
                color: "White",
                material: "Egyptian Cotton"
            );

            var customChocolate = SimpleProductFactory.CreateCustomFood(
                name: "Dark Chocolate Bar",
                price: 3.99m,
                expiryDate: DateTime.Now.AddDays(180),
                weight: 100
            );

            Console.WriteLine("\n--- Custom Products ---");
            customLaptop.DisplayInfo();
            customShirt.DisplayInfo();
            customChocolate.DisplayInfo();
        }
    }
}