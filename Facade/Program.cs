// See https://aka.ms/new-console-template for more information
using Facade.Facades;
using Facade.Subsystems;

namespace Facade
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Facade Pattern Implementation Examples ===\n");

            try
            {
                // Demonstrate direct subsystem usage
                DemonstrateDirectSubsystemUsage();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate facade pattern
                DemonstrateFacadePattern();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate complex operations
                DemonstrateComplexOperations();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate facade benefits
                DemonstrateFacadeBenefits();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\n=== Facade Pattern Demo Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates direct usage of subsystems (complex and error-prone)
        /// </summary>
        private static void DemonstrateDirectSubsystemUsage()
        {
            Console.WriteLine("1. Direct Subsystem Usage (Without Facade)");
            Console.WriteLine("===========================================");
            
            // Initialize subsystems
            var orderSystem = new OrderProcessingSubsystem();
            var inventorySystem = new InventorySubsystem();
            var paymentSystem = new PaymentSubsystem();
            var shippingSystem = new ShippingSubsystem();
            
            Console.WriteLine("\n--- Complex Manual Order Process ---");
            
            // Manual order placement (tedious and error-prone)
            var items = new List<OrderProcessingSubsystem.OrderItem>
            {
                new OrderProcessingSubsystem.OrderItem { ProductId = "P001", ProductName = "Laptop", Quantity = 1, Price = 999.99m },
                new OrderProcessingSubsystem.OrderItem { ProductId = "P002", ProductName = "Mouse", Quantity = 2, Price = 29.99m }
            };
            
            // Step 1: Create order
            var orderId = orderSystem.CreateOrder("John Doe", items);
            
            // Step 2: Check inventory manually
            bool allAvailable = true;
            foreach (var item in items)
            {
                if (!inventorySystem.CheckAvailability(item.ProductId, item.Quantity))
                {
                    allAvailable = false;
                    break;
                }
            }
            
            if (!allAvailable)
            {
                Console.WriteLine("Order failed: Insufficient inventory");
                return;
            }
            
            // Step 3: Reserve inventory
            foreach (var item in items)
            {
                inventorySystem.ReserveStock(item.ProductId, item.Quantity);
            }
            
            // Step 4: Validate order
            if (!orderSystem.ValidateOrder(orderId))
            {
                Console.WriteLine("Order validation failed");
                return;
            }
            
            // Step 5: Process payment
            var totalAmount = items.Sum(i => i.Price * i.Quantity);
            var transactionId = paymentSystem.ProcessPayment("PM001", orderId, totalAmount);
            
            if (transactionId <= 0)
            {
                Console.WriteLine("Payment failed");
                // Need to rollback inventory reservation
                foreach (var item in items)
                {
                    inventorySystem.ReleaseStock(item.ProductId, item.Quantity);
                }
                return;
            }
            
            // Step 6: Confirm payment in order system
            if (!orderSystem.ProcessPayment(orderId, totalAmount))
            {
                Console.WriteLine("Payment confirmation failed");
                return;
            }
            
            // Step 7: Create shipment
            var weight = items.Sum(i => i.Quantity * 0.5m);
            var shipmentId = shippingSystem.CreateShipment(orderId, "123 Main St, New York", weight, ShippingSubsystem.ShippingMethod.Standard);
            
            Console.WriteLine($"Manual process completed - Order: #{orderId}, Transaction: #{transactionId}, Shipment: #{shipmentId}");
        }

        /// <summary>
        /// Demonstrates facade pattern usage (simple and clean)
        /// </summary>
        private static void DemonstrateFacadePattern()
        {
            Console.WriteLine("\n2. Facade Pattern Usage (Simplified Interface)");
            Console.WriteLine("=============================================");
            
            var ecommerce = new ECommerceFacade();
            
            // Simple order placement
            var items = new List<OrderProcessingSubsystem.OrderItem>
            {
                new OrderProcessingSubsystem.OrderItem { ProductId = "P001", ProductName = "Laptop", Quantity = 1, Price = 999.99m },
                new OrderProcessingSubsystem.OrderItem { ProductId = "P003", ProductName = "Keyboard", Quantity = 1, Price = 79.99m }
            };
            
            Console.WriteLine("\n--- Simple Order Placement via Facade ---");
            var result = ecommerce.PlaceOrder(
                customerName: "Jane Smith",
                destinationAddress: "456 Oak Ave, Los Angeles",
                items: items,
                paymentMethodId: "PM002"
            );
            
            if (result.Success)
            {
                Console.WriteLine($"Order placed successfully!");
                Console.WriteLine($"Order ID: #{result.OrderId}");
                Console.WriteLine($"Transaction ID: #{result.TransactionId}");
                Console.WriteLine($"Total Amount: ${result.TotalAmount:F2}");
                
                // Track order
                ecommerce.TrackOrder(result.OrderId);
            }
            else
            {
                Console.WriteLine($"Order failed: {result.Message}");
            }
        }

        /// <summary>
        /// Demonstrates complex operations through facade
        /// </summary>
        private static void DemonstrateComplexOperations()
        {
            Console.WriteLine("\n3. Complex Operations Demo");
            Console.WriteLine("==========================");
            
            var ecommerce = new ECommerceFacade();
            
            // View product catalog
            Console.WriteLine("\n--- Product Catalog ---");
            var catalog = ecommerce.GetProductCatalog();
            foreach (var product in catalog)
            {
                var status = product.IsAvailable ? "In Stock" : "Out of Stock";
                Console.WriteLine($"{product.Name} - ${product.Price:F2} - {product.StockQuantity} units - {status}");
            }
            
            // Place multiple orders
            Console.WriteLine("\n--- Multiple Order Processing ---");
            var orderResults = new List<OrderResult>();
            
            // Order 1
            var items1 = new List<OrderProcessingSubsystem.OrderItem>
            {
                new OrderProcessingSubsystem.OrderItem { ProductId = "P004", ProductName = "Monitor", Quantity = 2, Price = 299.99m }
            };
            orderResults.Add(ecommerce.PlaceOrder("Alice Johnson", "789 Pine St, Chicago", items1, "PM003"));
            
            // Order 2
            var items2 = new List<OrderProcessingSubsystem.OrderItem>
            {
                new OrderProcessingSubsystem.OrderItem { ProductId = "P005", ProductName = "Headphones", Quantity = 1, Price = 149.99m }
            };
            orderResults.Add(ecommerce.PlaceOrder("Bob Wilson", "321 Elm St, Miami", items2, "PM001"));
            
            // Track all orders
            Console.WriteLine("\n--- Order Tracking ---");
            foreach (var result in orderResults.Where(r => r.Success))
            {
                ecommerce.TrackOrder(result.OrderId);
            }
            
            // Check system health
            Console.WriteLine("\n--- System Health Check ---");
            var health = ecommerce.GetSystemHealth();
            Console.WriteLine($"Order System: {health.OrderSystemStatus}");
            Console.WriteLine($"Inventory System: {health.InventorySystemStatus}");
            Console.WriteLine($"Payment System: {health.PaymentSystemStatus}");
            Console.WriteLine($"Shipping System: {health.ShippingSystemStatus}");
        }

        /// <summary>
        /// Demonstrates facade pattern benefits
        /// </summary>
        private static void DemonstrateFacadeBenefits()
        {
            Console.WriteLine("\n4. Facade Pattern Benefits Demo");
            Console.WriteLine("================================");
            
            Console.WriteLine("Facade Pattern Key Benefits:");
            Console.WriteLine("• Provides simplified interface to complex subsystems");
            Console.WriteLine("• Decouples clients from subsystem implementation details");
            Console.WriteLine("• Reduces complexity for clients");
            Console.WriteLine("• Improves maintainability and flexibility");
            Console.WriteLine("• Enables subsystem independence");
            
            Console.WriteLine("\nComparison:");
            Console.WriteLine("Without Facade: Clients must understand and coordinate multiple subsystems");
            Console.WriteLine("With Facade:    Clients interact with single, simplified interface");
            
            Console.WriteLine("\nReal-world Applications:");
            Console.WriteLine("• E-commerce platforms");
            Console.WriteLine("• Banking systems");
            Console.WriteLine("• API gateways");
            Console.WriteLine("• Enterprise application integration");
            Console.WriteLine("• Library/framework interfaces");
            
            Console.WriteLine("\nWhen to Use Facade:");
            Console.WriteLine("• Complex subsystems need simplified access");
            Console.WriteLine("• Multiple subsystems must be coordinated");
            Console.WriteLine("• Client code complexity needs reduction");
            Console.WriteLine("• System layers require decoupling");
        }
    }
}