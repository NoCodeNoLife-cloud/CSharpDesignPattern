using State.Contexts;

namespace State.States
{
    /// <summary>
    /// Shipped state implementation
    /// Order has been shipped but not yet delivered
    /// </summary>
    public class ShippedState : IOrderState
    {
        public void ProcessPayment(OrderContext context, decimal amount)
        {
            Console.WriteLine($"[Shipped] Order #{context.OrderId} is already paid - cannot process payment");
        }

        public void ShipOrder(OrderContext context)
        {
            Console.WriteLine($"[Shipped] Order #{context.OrderId} is already shipped");
        }

        public void DeliverOrder(OrderContext context)
        {
            Console.WriteLine($"[Shipped] Order #{context.OrderId} delivered successfully");
            context.CurrentState = new DeliveredState();
        }

        public void CancelOrder(OrderContext context)
        {
            Console.WriteLine($"[Shipped] Order #{context.OrderId} cancellation requested - contact customer service");
        }

        public void ReturnOrder(OrderContext context)
        {
            Console.WriteLine($"[Shipped] Cannot return order #{context.OrderId} - order not delivered yet");
        }

        public string GetStateName()
        {
            return "Shipped";
        }
    }
}