namespace Facade.Subsystems
{
    /// <summary>
    /// Order processing subsystem
    /// Handles order creation, validation, and management
    /// </summary>
    public class OrderProcessingSubsystem
    {
        private readonly List<Order> _orders = new List<Order>();
        private int _orderCounter = 1000;

        public class Order
        {
            public int OrderId { get; set; }
            public string CustomerName { get; set; } = string.Empty;
            public List<OrderItem> Items { get; set; } = new List<OrderItem>();
            public DateTime OrderDate { get; set; }
            public OrderStatus Status { get; set; }
            public decimal TotalAmount => Items.Sum(item => item.Price * item.Quantity);
        }

        public class OrderItem
        {
            public string ProductId { get; set; } = string.Empty;
            public string ProductName { get; set; } = string.Empty;
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }

        public enum OrderStatus
        {
            Pending,
            Confirmed,
            Processing,
            Shipped,
            Delivered,
            Cancelled
        }

        /// <summary>
        /// Creates a new order
        /// </summary>
        public int CreateOrder(string customerName, List<OrderItem> items)
        {
            var order = new Order
            {
                OrderId = _orderCounter++,
                CustomerName = customerName,
                Items = items,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending
            };

            _orders.Add(order);
            Console.WriteLine($"[Order System] Order #{order.OrderId} created for {customerName}");
            return order.OrderId;
        }

        /// <summary>
        /// Validates order availability
        /// </summary>
        public bool ValidateOrder(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null) return false;

            // Simulate inventory check
            foreach (var item in order.Items)
            {
                if (item.Quantity > 100) // Simulated stock limit
                {
                    Console.WriteLine($"[Order System] Insufficient stock for {item.ProductName}");
                    return false;
                }
            }

            order.Status = OrderStatus.Confirmed;
            Console.WriteLine($"[Order System] Order #{orderId} validated and confirmed");
            return true;
        }

        /// <summary>
        /// Processes order payment
        /// </summary>
        public bool ProcessPayment(int orderId, decimal amount)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null || order.Status != OrderStatus.Confirmed) return false;

            if (Math.Abs(amount - order.TotalAmount) < 0.01m)
            {
                order.Status = OrderStatus.Processing;
                Console.WriteLine($"[Order System] Payment of ${amount:F2} processed for order #{orderId}");
                return true;
            }

            Console.WriteLine($"[Order System] Payment mismatch for order #{orderId}");
            return false;
        }

        /// <summary>
        /// Gets order details
        /// </summary>
        public Order? GetOrderDetails(int orderId)
        {
            return _orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        /// <summary>
        /// Cancels an order
        /// </summary>
        public bool CancelOrder(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null || order.Status == OrderStatus.Shipped) return false;

            order.Status = OrderStatus.Cancelled;
            Console.WriteLine($"[Order System] Order #{orderId} cancelled");
            return true;
        }

        /// <summary>
        /// Gets all orders
        /// </summary>
        public List<Order> GetAllOrders()
        {
            return _orders.ToList();
        }
    }
}