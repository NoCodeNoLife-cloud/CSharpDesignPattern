namespace Facade.Subsystems
{
    /// <summary>
    /// Inventory management subsystem
    /// Handles product inventory, stock levels, and warehouse operations
    /// </summary>
    public class InventorySubsystem
    {
        private readonly Dictionary<string, Product> _inventory = new Dictionary<string, Product>();

        public class Product
        {
            public string ProductId { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public int StockQuantity { get; set; }
            public decimal UnitPrice { get; set; }
            public string Category { get; set; } = string.Empty;
            public DateTime LastUpdated { get; set; }
        }

        public class WarehouseLocation
        {
            public string LocationId { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public int Capacity { get; set; }
            public int CurrentUsage { get; set; }
        }

        public InventorySubsystem()
        {
            // Initialize with sample products
            InitializeInventory();
        }

        private void InitializeInventory()
        {
            AddProduct("P001", "Laptop", 50, 999.99m, "Electronics");
            AddProduct("P002", "Mouse", 200, 29.99m, "Electronics");
            AddProduct("P003", "Keyboard", 150, 79.99m, "Electronics");
            AddProduct("P004", "Monitor", 75, 299.99m, "Electronics");
            AddProduct("P005", "Headphones", 100, 149.99m, "Electronics");
        }

        /// <summary>
        /// Adds a new product to inventory
        /// </summary>
        public void AddProduct(string productId, string name, int quantity, decimal price, string category)
        {
            var product = new Product
            {
                ProductId = productId,
                Name = name,
                StockQuantity = quantity,
                UnitPrice = price,
                Category = category,
                LastUpdated = DateTime.Now
            };

            _inventory[productId] = product;
            Console.WriteLine($"[Inventory] Added {name} (#{productId}) - Qty: {quantity}, Price: ${price:F2}");
        }

        /// <summary>
        /// Checks product availability
        /// </summary>
        public bool CheckAvailability(string productId, int quantity)
        {
            if (_inventory.TryGetValue(productId, out var product))
            {
                bool available = product.StockQuantity >= quantity;
                Console.WriteLine($"[Inventory] {product.Name} availability: {product.StockQuantity} in stock, Requested: {quantity}, Available: {available}");
                return available;
            }
            
            Console.WriteLine($"[Inventory] Product #{productId} not found");
            return false;
        }

        /// <summary>
        /// Reserves stock for an order
        /// </summary>
        public bool ReserveStock(string productId, int quantity)
        {
            if (!_inventory.TryGetValue(productId, out var product)) return false;
            
            if (product.StockQuantity >= quantity)
            {
                product.StockQuantity -= quantity;
                product.LastUpdated = DateTime.Now;
                Console.WriteLine($"[Inventory] Reserved {quantity} units of {product.Name}");
                return true;
            }
            
            Console.WriteLine($"[Inventory] Insufficient stock for {product.Name}. Available: {product.StockQuantity}");
            return false;
        }

        /// <summary>
        /// Releases reserved stock
        /// </summary>
        public void ReleaseStock(string productId, int quantity)
        {
            if (_inventory.TryGetValue(productId, out var product))
            {
                product.StockQuantity += quantity;
                product.LastUpdated = DateTime.Now;
                Console.WriteLine($"[Inventory] Released {quantity} units of {product.Name}");
            }
        }

        /// <summary>
        /// Updates stock quantity
        /// </summary>
        public void UpdateStock(string productId, int newQuantity)
        {
            if (_inventory.TryGetValue(productId, out var product))
            {
                product.StockQuantity = newQuantity;
                product.LastUpdated = DateTime.Now;
                Console.WriteLine($"[Inventory] Updated {product.Name} stock to {newQuantity}");
            }
        }

        /// <summary>
        /// Gets product information
        /// </summary>
        public Product? GetProductInfo(string productId)
        {
            return _inventory.TryGetValue(productId, out var product) ? product : null;
        }

        /// <summary>
        /// Gets low stock products
        /// </summary>
        public List<Product> GetLowStockProducts(int threshold = 10)
        {
            return _inventory.Values
                .Where(p => p.StockQuantity <= threshold)
                .ToList();
        }

        /// <summary>
        /// Gets inventory report
        /// </summary>
        public Dictionary<string, int> GetInventoryReport()
        {
            return _inventory.ToDictionary(
                kvp => kvp.Value.Name,
                kvp => kvp.Value.StockQuantity
            );
        }

        /// <summary>
        /// Restocks a product
        /// </summary>
        public void RestockProduct(string productId, int quantity)
        {
            if (_inventory.TryGetValue(productId, out var product))
            {
                product.StockQuantity += quantity;
                product.LastUpdated = DateTime.Now;
                Console.WriteLine($"[Inventory] Restocked {product.Name} with {quantity} units. New total: {product.StockQuantity}");
            }
        }
    }
}