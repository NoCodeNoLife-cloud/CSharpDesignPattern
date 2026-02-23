namespace TemplateMethod.Processors
{
    /// <summary>
    /// JSON data processor implementation
    /// Processes JSON data using template method pattern
    /// </summary>
    public class JsonDataProcessor : Framework.DataProcessor
    {
        private readonly string _apiEndpoint;
        private readonly List<JsonProduct> _products = new List<JsonProduct>();

        public JsonDataProcessor(string apiEndpoint) : base("JSON Processor")
        {
            _apiEndpoint = apiEndpoint;
        }

        protected override object LoadData()
        {
            Console.WriteLine($"[JSON Processor] Fetching data from API: {_apiEndpoint}");
            
            // Simulate JSON API response
            var jsonData = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    ["id"] = 1,
                    ["name"] = "Laptop",
                    ["category"] = "Electronics",
                    ["price"] = 999.99,
                    ["stock"] = 50
                },
                new Dictionary<string, object>
                {
                    ["id"] = 2,
                    ["name"] = "Mouse",
                    ["category"] = "Electronics",
                    ["price"] = 29.99,
                    ["stock"] = 200
                },
                new Dictionary<string, object>
                {
                    ["id"] = 3,
                    ["name"] = "Keyboard",
                    ["category"] = "Electronics",
                    ["price"] = 79.99,
                    ["stock"] = 100
                },
                new Dictionary<string, object>
                {
                    ["id"] = 4,
                    ["name"] = "Monitor",
                    ["category"] = "Electronics",
                    ["price"] = 299.99,
                    ["stock"] = 25
                }
            };
            
            Console.WriteLine($"[JSON Processor] Retrieved {jsonData.Count} products from API");
            return jsonData;
        }

        protected override bool ValidateData(object data)
        {
            Console.WriteLine("[JSON Processor] Validating JSON data structure");
            
            if (data is not List<Dictionary<string, object>> jsonData)
                return false;
            
            // Validate required fields exist
            var requiredFields = new[] { "id", "name", "category", "price", "stock" };
            
            foreach (var item in jsonData)
            {
                foreach (var field in requiredFields)
                {
                    if (!item.ContainsKey(field))
                    {
                        Console.WriteLine($"[JSON Processor] Missing required field: {field}");
                        return false;
                    }
                }
                
                // Validate data types
                if (item["price"] is not double && item["price"] is not int)
                {
                    Console.WriteLine($"[JSON Processor] Invalid price data type");
                    return false;
                }
                
                if (item["stock"] is not int)
                {
                    Console.WriteLine($"[JSON Processor] Invalid stock data type");
                    return false;
                }
            }
            
            Console.WriteLine($"[JSON Processor] JSON data validation passed ({jsonData.Count} items)");
            return true;
        }

        protected override object TransformData(object rawData)
        {
            Console.WriteLine("[JSON Processor] Transforming JSON data to product objects");
            
            var jsonData = (List<Dictionary<string, object>>)rawData;
            _products.Clear();
            
            foreach (var item in jsonData)
            {
                var product = new JsonProduct
                {
                    Id = Convert.ToInt32(item["id"]),
                    Name = item["name"].ToString() ?? string.Empty,
                    Category = item["category"].ToString() ?? string.Empty,
                    Price = Convert.ToDecimal(item["price"]),
                    Stock = Convert.ToInt32(item["stock"])
                };
                _products.Add(product);
            }
            
            Console.WriteLine($"[JSON Processor] Transformed {_products.Count} products");
            return _products;
        }

        protected override object ProcessTransformedData(object transformedData)
        {
            Console.WriteLine("[JSON Processor] Processing product inventory analysis");
            
            var products = (List<JsonProduct>)transformedData;
            
            // Calculate inventory value
            foreach (var product in products)
            {
                product.InventoryValue = product.Price * product.Stock;
                product.DiscountedPrice = product.Price * 0.9m; // 10% discount
            }
            
            // Calculate category statistics
            var categoryStats = products.GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    ProductCount = g.Count(),
                    TotalValue = g.Sum(p => p.InventoryValue),
                    AveragePrice = g.Average(p => p.Price)
                })
                .ToList();
            
            Console.WriteLine("[JSON Processor] Inventory Analysis:");
            foreach (var stat in categoryStats)
            {
                Console.WriteLine($"  {stat.Category}: {stat.ProductCount} products, Value: ${stat.TotalValue:F2}, Avg Price: ${stat.AveragePrice:F2}");
            }
            
            return products;
        }

        protected override void SaveResults(object processedData)
        {
            Console.WriteLine("[JSON Processor] Saving processed inventory results");
            
            var products = (List<JsonProduct>)processedData;
            
            // Simulate saving to database
            var totalInventoryValue = products.Sum(p => p.InventoryValue);
            var lowStockProducts = products.Where(p => p.Stock < 30).ToList();
            
            Console.WriteLine($"[JSON Processor] Inventory Summary:");
            Console.WriteLine($"  Total Products: {products.Count}");
            Console.WriteLine($"  Total Inventory Value: ${totalInventoryValue:F2}");
            Console.WriteLine($"  Low Stock Items: {lowStockProducts.Count}");
            
            if (lowStockProducts.Any())
            {
                Console.WriteLine("  Low Stock Products:");
                foreach (var product in lowStockProducts)
                {
                    Console.WriteLine($"    {product.Name}: {product.Stock} units remaining");
                }
            }
        }

        protected override void GenerateReport()
        {
            Console.WriteLine("[JSON Processor] Generating inventory management report");
            
            var totalProducts = _products.Count;
            var totalValue = _products.Sum(p => p.InventoryValue);
            var avgPrice = _products.Average(p => p.Price);
            var categories = _products.Select(p => p.Category).Distinct().ToList();
            
            Console.WriteLine($"[JSON Processor] Report Summary:");
            Console.WriteLine($"  Products Analyzed: {totalProducts}");
            Console.WriteLine($"  Total Inventory Value: ${totalValue:F2}");
            Console.WriteLine($"  Average Product Price: ${avgPrice:F2}");
            Console.WriteLine($"  Categories: {string.Join(", ", categories)}");
            
            // Price range analysis
            var expensiveProducts = _products.Where(p => p.Price > 100).Count();
            var affordableProducts = _products.Where(p => p.Price <= 100).Count();
            
            Console.WriteLine($"  Price Analysis:");
            Console.WriteLine($"    Expensive Items (> $100): {expensiveProducts}");
            Console.WriteLine($"    Affordable Items (≤ $100): {affordableProducts}");
        }

        protected override void HandleError(Exception ex)
        {
            Console.WriteLine($"[JSON Processor] JSON processing error: {ex.Message}");
            // Implement retry logic or alert system
        }

        protected override void HandleValidationError()
        {
            Console.WriteLine("[JSON Processor] JSON data validation failed - check API response format");
        }

        // JSON Product data structure
        private class JsonProduct
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Category { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public decimal InventoryValue { get; set; }
            public decimal DiscountedPrice { get; set; }
        }
    }
}