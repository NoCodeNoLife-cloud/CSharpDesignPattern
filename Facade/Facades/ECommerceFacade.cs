using Facade.Subsystems;

namespace Facade.Facades
{
    /// <summary>
    /// E-commerce system facade
    /// Provides simplified interface for complex e-commerce operations
    /// </summary>
    public class ECommerceFacade
    {
        private readonly OrderProcessingSubsystem _orderSystem;
        private readonly InventorySubsystem _inventorySystem;
        private readonly PaymentSubsystem _paymentSystem;
        private readonly ShippingSubsystem _shippingSystem;

        public ECommerceFacade()
        {
            _orderSystem = new OrderProcessingSubsystem();
            _inventorySystem = new InventorySubsystem();
            _paymentSystem = new PaymentSubsystem();
            _shippingSystem = new ShippingSubsystem();
        }

        /// <summary>
        /// Places a complete order with all subsystems
        /// </summary>
        public OrderResult PlaceOrder(string customerName, string destinationAddress, List<OrderProcessingSubsystem.OrderItem> items, string paymentMethodId)
        {
            Console.WriteLine($"\n=== Placing Order for {customerName} ===");

            try
            {
                // Step 1: Create order
                var orderId = _orderSystem.CreateOrder(customerName, items);
                if (orderId <= 0)
                {
                    return new OrderResult { Success = false, Message = "Failed to create order" };
                }

                // Step 2: Validate inventory
                foreach (var item in items)
                {
                    if (!_inventorySystem.CheckAvailability(item.ProductId, item.Quantity))
                    {
                        _orderSystem.CancelOrder(orderId);
                        return new OrderResult { Success = false, Message = $"Insufficient inventory for {item.ProductName}" };
                    }
                }

                // Step 3: Validate order
                if (!_orderSystem.ValidateOrder(orderId))
                {
                    return new OrderResult { Success = false, Message = "Order validation failed" };
                }

                // Step 4: Reserve inventory
                foreach (var item in items)
                {
                    _inventorySystem.ReserveStock(item.ProductId, item.Quantity);
                }

                // Step 5: Process payment
                var totalAmount = items.Sum(item => item.Price * item.Quantity);
                var transactionId = _paymentSystem.ProcessPayment(paymentMethodId, orderId, totalAmount);
                if (transactionId <= 0)
                {
                    // Release reserved inventory on payment failure
                    foreach (var item in items)
                    {
                        _inventorySystem.ReleaseStock(item.ProductId, item.Quantity);
                    }

                    _orderSystem.CancelOrder(orderId);
                    return new OrderResult { Success = false, Message = "Payment processing failed" };
                }

                // Step 6: Process payment confirmation
                if (!_orderSystem.ProcessPayment(orderId, totalAmount))
                {
                    // Handle payment confirmation failure
                    _paymentSystem.ProcessRefund(transactionId, totalAmount);
                    foreach (var item in items)
                    {
                        _inventorySystem.ReleaseStock(item.ProductId, item.Quantity);
                    }

                    _orderSystem.CancelOrder(orderId);
                    return new OrderResult { Success = false, Message = "Payment confirmation failed" };
                }

                // Step 7: Create shipment
                var totalWeight = items.Sum(item => item.Quantity * 0.5m); // Assume 0.5kg per item
                var shipmentId = _shippingSystem.CreateShipment(orderId, destinationAddress, totalWeight, ShippingSubsystem.ShippingMethod.Standard);
                if (shipmentId <= 0)
                {
                    Console.WriteLine("[Warning] Failed to create shipment, but order is confirmed");
                }

                Console.WriteLine($"=== Order #{orderId} placed successfully ===\n");

                return new OrderResult
                {
                    Success = true,
                    OrderId = orderId,
                    TransactionId = transactionId,
                    ShipmentId = shipmentId,
                    TotalAmount = totalAmount,
                    Message = "Order placed successfully"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Order placement failed: {ex.Message}");
                return new OrderResult { Success = false, Message = $"System error: {ex.Message}" };
            }
        }

        /// <summary>
        /// Tracks order status across all subsystems
        /// </summary>
        public OrderStatusResult TrackOrder(int orderId)
        {
            Console.WriteLine($"\n=== Tracking Order #{orderId} ===");

            var order = _orderSystem.GetOrderDetails(orderId);
            if (order == null)
            {
                return new OrderStatusResult { Success = false, Message = "Order not found" };
            }

            var shipments = _shippingSystem.GetOrderShipments(orderId);
            var transactions = _paymentSystem.GetTransactionHistory(orderId);

            var result = new OrderStatusResult
            {
                Success = true,
                OrderId = orderId,
                OrderStatus = order.Status,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Shipments = shipments,
                Transactions = transactions,
                Message = "Order status retrieved successfully"
            };

            // Display formatted status
            Console.WriteLine($"Order Status: {order.Status}");
            Console.WriteLine($"Order Date: {order.OrderDate:yyyy-MM-dd HH:mm}");
            Console.WriteLine($"Total Amount: ${order.TotalAmount:F2}");
            Console.WriteLine($"Items: {order.Items.Count} items");

            if (shipments.Any())
            {
                Console.WriteLine("Shipments:");
                foreach (var shipment in shipments)
                {
                    Console.WriteLine($"  #{shipment.ShipmentId} - {shipment.Status} - Tracking: {shipment.TrackingNumber}");
                }
            }

            if (transactions.Any())
            {
                Console.WriteLine("Transactions:");
                foreach (var transaction in transactions)
                {
                    Console.WriteLine($"  #{transaction.TransactionId} - {transaction.Status} - ${transaction.Amount:F2}");
                }
            }

            Console.WriteLine("========================\n");
            return result;
        }

        /// <summary>
        /// Cancels an order and handles all cleanup
        /// </summary>
        public bool CancelOrder(int orderId)
        {
            Console.WriteLine($"\n=== Cancelling Order #{orderId} ===");

            var order = _orderSystem.GetOrderDetails(orderId);
            if (order == null) return false;

            // Cancel shipments
            var shipments = _shippingSystem.GetOrderShipments(orderId);
            foreach (var shipment in shipments)
            {
                if (shipment.Status != ShippingSubsystem.ShipmentStatus.Delivered)
                {
                    _shippingSystem.CancelShipment(shipment.ShipmentId);
                }
            }

            // Process refund
            var transactions = _paymentSystem.GetTransactionHistory(orderId);
            foreach (var transaction in transactions.Where(t => t.Status == PaymentSubsystem.TransactionStatus.Completed))
            {
                _paymentSystem.ProcessRefund(transaction.TransactionId, transaction.Amount);
            }

            // Release inventory
            foreach (var item in order.Items)
            {
                _inventorySystem.ReleaseStock(item.ProductId, item.Quantity);
            }

            // Cancel order
            var result = _orderSystem.CancelOrder(orderId);

            Console.WriteLine(result ? "Order cancelled successfully" : "Failed to cancel order");
            Console.WriteLine("========================\n");

            return result;
        }

        /// <summary>
        /// Gets product catalog with availability
        /// </summary>
        public List<ProductCatalogItem> GetProductCatalog()
        {
            var inventoryReport = _inventorySystem.GetInventoryReport();

            return inventoryReport.Select(kvp => new ProductCatalogItem
                {
                    ProductId = $"PROD_{kvp.Key.Replace(" ", "_").ToUpper()}",
                    Name = kvp.Key,
                    Price = 0, // Price not available from inventory report
                    StockQuantity = kvp.Value,
                    Category = "General",
                    IsAvailable = kvp.Value > 0
                })
                .ToList();
        }

        /// <summary>
        /// Calculates total cost including shipping
        /// </summary>
        public decimal CalculateTotalCost(List<OrderProcessingSubsystem.OrderItem> items, string destination, ShippingSubsystem.ShippingMethod shippingMethod)
        {
            var productCost = items.Sum(item => item.Price * item.Quantity);
            var totalWeight = items.Sum(item => item.Quantity * 0.5m);
            var shippingCost = _shippingSystem.CalculateShippingCost(destination, totalWeight, shippingMethod);

            return productCost + shippingCost;
        }

        /// <summary>
        /// Gets system health status
        /// </summary>
        public SystemHealthStatus GetSystemHealth()
        {
            var health = new SystemHealthStatus
            {
                OrderSystemStatus = "Operational",
                InventorySystemStatus = "Operational",
                PaymentSystemStatus = "Operational",
                ShippingSystemStatus = "Operational",
                LastCheckTime = DateTime.Now
            };

            // Check for issues
            var lowStockItems = _inventorySystem.GetLowStockProducts(5).Count;
            if (lowStockItems > 10)
            {
                health.InventorySystemStatus = "Warning - Low stock items detected";
            }

            var pendingOrders = _orderSystem.GetAllOrders()
                .Count(o => o.Status == OrderProcessingSubsystem.OrderStatus.Pending);

            if (pendingOrders > 50)
            {
                health.OrderSystemStatus = "Warning - High pending orders";
            }

            return health;
        }
    }

    #region Result Classes

    public class OrderResult
    {
        public bool Success { get; set; }
        public int OrderId { get; set; }
        public int TransactionId { get; set; }
        public int ShipmentId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class OrderStatusResult
    {
        public bool Success { get; set; }
        public int OrderId { get; set; }
        public OrderProcessingSubsystem.OrderStatus OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<ShippingSubsystem.Shipment> Shipments { get; set; } = new List<ShippingSubsystem.Shipment>();
        public List<PaymentSubsystem.Transaction> Transactions { get; set; } = new List<PaymentSubsystem.Transaction>();
        public string Message { get; set; } = string.Empty;
    }

    public class ProductCatalogItem
    {
        public string ProductId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }

    public class SystemHealthStatus
    {
        public string OrderSystemStatus { get; set; } = string.Empty;
        public string InventorySystemStatus { get; set; } = string.Empty;
        public string PaymentSystemStatus { get; set; } = string.Empty;
        public string ShippingSystemStatus { get; set; } = string.Empty;
        public DateTime LastCheckTime { get; set; }
    }

    #endregion
}