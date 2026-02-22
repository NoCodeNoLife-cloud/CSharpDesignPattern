namespace FactoryMethod.Products
{
    /// <summary>
    /// Food product implementation
    /// Concrete product for food items
    /// </summary>
    public class FoodProduct : IProduct
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category => "Food";
        public DateTime ExpiryDate { get; set; }
        public double Weight { get; set; }
        public bool IsOrganic { get; set; }

        public FoodProduct()
        {
        }

        public FoodProduct(string name, decimal price, DateTime expiryDate, double weight, bool isOrganic = false)
        {
            Name = name;
            Price = price;
            ExpiryDate = expiryDate;
            Weight = weight;
            IsOrganic = isOrganic;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"=== Food Product Information ===");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Weight: {Weight}g");
            Console.WriteLine($"Price: ${Price:F2}");
            Console.WriteLine($"Expiry Date: {ExpiryDate:yyyy-MM-dd}");
            Console.WriteLine($"Category: {Category}");
            Console.WriteLine($"Organic: {(IsOrganic ? "Yes" : "No")}");
            Console.WriteLine($"===============================");
        }
    }
}