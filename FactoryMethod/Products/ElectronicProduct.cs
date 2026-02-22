namespace FactoryMethod.Products
{
    /// <summary>
    /// Electronic product implementation
    /// Concrete product for electronic devices
    /// </summary>
    public class ElectronicProduct : IProduct
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category => "Electronics";
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public bool IsWireless { get; set; }

        public ElectronicProduct()
        {
        }

        public ElectronicProduct(string name, decimal price, string brand, string model, bool isWireless = false)
        {
            Name = name;
            Price = price;
            Brand = brand;
            Model = model;
            IsWireless = isWireless;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"=== Electronic Product Information ===");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Brand: {Brand}");
            Console.WriteLine($"Model: {Model}");
            Console.WriteLine($"Price: ${Price:F2}");
            Console.WriteLine($"Category: {Category}");
            Console.WriteLine($"Wireless: {(IsWireless ? "Yes" : "No")}");
            Console.WriteLine($"=====================================");
        }
    }
}