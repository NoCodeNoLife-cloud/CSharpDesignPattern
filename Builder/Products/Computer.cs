namespace Builder.Products
{
    /// <summary>
    /// Computer product class
    /// Represents the complex object to be built
    /// </summary>
    public class Computer
    {
        // Required components
        public string CPU { get; set; } = string.Empty;
        public string RAM { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;

        // Optional components
        public string GraphicsCard { get; set; } = "Integrated";
        public string Motherboard { get; set; } = "Standard";
        public string PowerSupply { get; set; } = "500W";
        public string Case { get; set; } = "Mid Tower";
        public List<string> Accessories { get; set; } = new List<string>();

        /// <summary>
        /// Gets computer specification summary
        /// </summary>
        public string Specification => $"{CPU} + {RAM} + {Storage}";

        /// <summary>
        /// Displays complete computer configuration
        /// </summary>
        public void DisplayConfiguration()
        {
            Console.WriteLine("\n=== Computer Configuration ===");
            Console.WriteLine($"CPU: {CPU}");
            Console.WriteLine($"RAM: {RAM}");
            Console.WriteLine($"Storage: {Storage}");
            Console.WriteLine($"Graphics Card: {GraphicsCard}");
            Console.WriteLine($"Motherboard: {Motherboard}");
            Console.WriteLine($"Power Supply: {PowerSupply}");
            Console.WriteLine($"Case: {Case}");
            
            if (Accessories.Any())
            {
                Console.WriteLine("Accessories:");
                foreach (var accessory in Accessories)
                {
                    Console.WriteLine($"  - {accessory}");
                }
            }
            
            Console.WriteLine($"Specification: {Specification}");
            Console.WriteLine("=============================\n");
        }

        /// <summary>
        /// Calculates estimated price
        /// </summary>
        public decimal CalculateEstimatedPrice()
        {
            var basePrice = GetComponentPrice(CPU) + GetComponentPrice(RAM) + GetComponentPrice(Storage);
            var optionalPrice = GetComponentPrice(GraphicsCard) + GetComponentPrice(Motherboard) + 
                              GetComponentPrice(PowerSupply) + GetComponentPrice(Case);
            
            var accessoriesPrice = Accessories.Sum(GetComponentPrice);
            
            return basePrice + optionalPrice + accessoriesPrice;
        }

        /// <summary>
        /// Gets component price (simplified pricing logic)
        /// </summary>
        private static decimal GetComponentPrice(string component)
        {
            return component.ToLower() switch
            {
                var c when c.Contains("intel") || c.Contains("amd") => 300m,
                var c when c.Contains("16gb") => 100m,
                var c when c.Contains("32gb") => 200m,
                var c when c.Contains("ssd") => 80m,
                var c when c.Contains("hdd") => 50m,
                var c when c.Contains("rtx") => 500m,
                var c when c.Contains("atx") => 150m,
                var c when c.Contains("750w") => 120m,
                var c when c.Contains("full tower") => 80m,
                _ => 0m
            };
        }
    }
}