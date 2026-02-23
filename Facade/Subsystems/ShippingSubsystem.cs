namespace Facade.Subsystems
{
    /// <summary>
    /// Shipping and logistics subsystem
    /// Handles package shipping, tracking, and delivery operations
    /// </summary>
    public class ShippingSubsystem
    {
        private readonly List<Shipment> _shipments = new List<Shipment>();
        private readonly Dictionary<string, DeliveryRoute> _routes = new Dictionary<string, DeliveryRoute>();
        private int _shipmentCounter = 7000;

        public class Shipment
        {
            public int ShipmentId { get; set; }
            public int OrderId { get; set; }
            public string TrackingNumber { get; set; } = string.Empty;
            public string DestinationAddress { get; set; } = string.Empty;
            public decimal Weight { get; set; }
            public ShippingMethod Method { get; set; }
            public ShipmentStatus Status { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime? EstimatedDelivery { get; set; }
            public DateTime? ActualDelivery { get; set; }
        }

        public class DeliveryRoute
        {
            public string RouteId { get; set; } = string.Empty;
            public string Origin { get; set; } = string.Empty;
            public string Destination { get; set; } = string.Empty;
            public decimal BaseCost { get; set; }
            public int EstimatedDays { get; set; }
            public List<string> TransitPoints { get; set; } = new List<string>();
        }

        public enum ShippingMethod
        {
            Standard,
            Express,
            Overnight,
            International
        }

        public enum ShipmentStatus
        {
            Pending,
            Processing,
            InTransit,
            OutForDelivery,
            Delivered,
            Returned
        }

        public ShippingSubsystem()
        {
            // Initialize delivery routes
            InitializeRoutes();
        }

        private void InitializeRoutes()
        {
            AddRoute("R001", "Warehouse A", "New York", 15.99m, 3, new[] { "Chicago", "Cleveland" });
            AddRoute("R002", "Warehouse A", "Los Angeles", 25.99m, 5, new[] { "Phoenix", "Las Vegas" });
            AddRoute("R003", "Warehouse B", "Chicago", 12.99m, 2, new[] { "Detroit" });
            AddRoute("R004", "Warehouse B", "Miami", 22.99m, 4, new[] { "Atlanta", "Jacksonville" });
        }

        /// <summary>
        /// Creates a shipment for an order
        /// </summary>
        public int CreateShipment(int orderId, string destinationAddress, decimal weight, ShippingMethod method)
        {
            var route = FindBestRoute(destinationAddress, method);
            if (route == null) return -1;

            var shipment = new Shipment
            {
                ShipmentId = _shipmentCounter++,
                OrderId = orderId,
                TrackingNumber = GenerateTrackingNumber(),
                DestinationAddress = destinationAddress,
                Weight = weight,
                Method = method,
                Status = ShipmentStatus.Processing,
                CreatedDate = DateTime.Now,
                EstimatedDelivery = DateTime.Now.AddDays(route.EstimatedDays)
            };

            _shipments.Add(shipment);
            Console.WriteLine($"[Shipping] Shipment #{shipment.ShipmentId} created for order #{orderId}");
            Console.WriteLine($"[Shipping] Tracking #: {shipment.TrackingNumber}");
            Console.WriteLine($"[Shipping] Estimated delivery: {shipment.EstimatedDelivery:yyyy-MM-dd}");

            return shipment.ShipmentId;
        }

        /// <summary>
        /// Updates shipment status
        /// </summary>
        public bool UpdateShipmentStatus(int shipmentId, ShipmentStatus newStatus)
        {
            var shipment = _shipments.FirstOrDefault(s => s.ShipmentId == shipmentId);
            if (shipment == null) return false;

            var oldStatus = shipment.Status;
            shipment.Status = newStatus;
            
            if (newStatus == ShipmentStatus.Delivered)
            {
                shipment.ActualDelivery = DateTime.Now;
            }

            Console.WriteLine($"[Shipping] Shipment #{shipmentId} status updated: {oldStatus} → {newStatus}");
            return true;
        }

        /// <summary>
        /// Gets shipment tracking information
        /// </summary>
        public Shipment? TrackShipment(string trackingNumber)
        {
            return _shipments.FirstOrDefault(s => s.TrackingNumber == trackingNumber);
        }

        /// <summary>
        /// Gets shipment by ID
        /// </summary>
        public Shipment? GetShipmentDetails(int shipmentId)
        {
            return _shipments.FirstOrDefault(s => s.ShipmentId == shipmentId);
        }

        /// <summary>
        /// Gets shipments for order
        /// </summary>
        public List<Shipment> GetOrderShipments(int orderId)
        {
            return _shipments
                .Where(s => s.OrderId == orderId)
                .OrderBy(s => s.CreatedDate)
                .ToList();
        }

        /// <summary>
        /// Calculates shipping cost
        /// </summary>
        public decimal CalculateShippingCost(string destination, decimal weight, ShippingMethod method)
        {
            var route = FindBestRoute(destination, method);
            if (route == null) return -1;

            decimal baseCost = route.BaseCost;
            decimal weightSurcharge = weight > 10 ? (weight - 10) * 2 : 0;
            decimal methodMultiplier = method switch
            {
                ShippingMethod.Express => 1.5m,
                ShippingMethod.Overnight => 2.0m,
                ShippingMethod.International => 3.0m,
                _ => 1.0m
            };

            decimal totalCost = (baseCost + weightSurcharge) * methodMultiplier;
            Console.WriteLine($"[Shipping] Cost calculation: Base=${baseCost:F2}, Weight surcharge=${weightSurcharge:F2}, Method multiplier={methodMultiplier}, Total=${totalCost:F2}");
            
            return Math.Round(totalCost, 2);
        }

        /// <summary>
        /// Gets delivery estimate
        /// </summary>
        public DateTime? GetDeliveryEstimate(string destination, ShippingMethod method)
        {
            var route = FindBestRoute(destination, method);
            return route != null ? DateTime.Now.AddDays(route.EstimatedDays) : null;
        }

        /// <summary>
        /// Gets active shipments
        /// </summary>
        public List<Shipment> GetActiveShipments()
        {
            return _shipments
                .Where(s => s.Status != ShipmentStatus.Delivered && s.Status != ShipmentStatus.Returned)
                .ToList();
        }

        /// <summary>
        /// Gets shipping statistics
        /// </summary>
        public Dictionary<ShipmentStatus, int> GetShippingStatistics()
        {
            return _shipments
                .GroupBy(s => s.Status)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        /// <summary>
        /// Cancels a shipment
        /// </summary>
        public bool CancelShipment(int shipmentId)
        {
            var shipment = _shipments.FirstOrDefault(s => s.ShipmentId == shipmentId);
            if (shipment == null || shipment.Status == ShipmentStatus.InTransit) return false;

            shipment.Status = ShipmentStatus.Returned;
            Console.WriteLine($"[Shipping] Shipment #{shipmentId} cancelled and marked for return");
            return true;
        }

        private DeliveryRoute? FindBestRoute(string destination, ShippingMethod method)
        {
            // Simplified route finding logic
            return _routes.Values
                .FirstOrDefault(r => destination.Contains(r.Destination, StringComparison.OrdinalIgnoreCase));
        }

        private void AddRoute(string routeId, string origin, string destination, decimal baseCost, int estimatedDays, string[] transitPoints)
        {
            var route = new DeliveryRoute
            {
                RouteId = routeId,
                Origin = origin,
                Destination = destination,
                BaseCost = baseCost,
                EstimatedDays = estimatedDays,
                TransitPoints = transitPoints.ToList()
            };

            _routes[routeId] = route;
        }

        private static string GenerateTrackingNumber()
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return "TRK" + new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}