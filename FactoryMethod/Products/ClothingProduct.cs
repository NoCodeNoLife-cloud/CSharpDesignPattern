namespace FactoryMethod.Products
{
    /// <summary>
    /// Clothing product implementation
    /// Concrete product for clothing items
    /// </summary>
    public class ClothingProduct : IProduct
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category => "Clothing";
        public string Size { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;

        public ClothingProduct()
        {
        }

        public ClothingProduct(string name, decimal price, string size, string color, string material)
        {
            Name = name;
            Price = price;
            Size = size;
            Color = color;
            Material = material;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"=== Clothing Product Information ===");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Size: {Size}");
            Console.WriteLine($"Color: {Color}");
            Console.WriteLine($"Material: {Material}");
            Console.WriteLine($"Price: ${Price:F2}");
            Console.WriteLine($"Category: {Category}");
            Console.WriteLine($"==================================");
        }
    }
}