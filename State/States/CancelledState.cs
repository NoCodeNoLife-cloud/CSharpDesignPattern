using State.Contexts;

namespace State.States
{
    /// <summary>
    /// Cancelled state implementation
    /// Order has been cancelled
    /// </summary>
    public class CancelledState : IOrderState
    {
        public void ProcessPayment(OrderContext context, decimal amount)
        {
            Console.WriteLine($"[Cancelled] Cannot process payment for cancelled order #{context.OrderId}");
        }

        public void ShipOrder(OrderContext context)
        {
            Console.WriteLine($"[Cancelled] Cannot ship cancelled order #{context.OrderId}");
        }

        public void DeliverOrder(OrderContext context)
        {
            Console.WriteLine($"[Cancelled] Cannot deliver cancelled order #{context.OrderId}");
        }

        public void CancelOrder(OrderContext context)
        {
            Console.WriteLine($"[Cancelled] Order #{context.OrderId} is already cancelled");
        }

        public void ReturnOrder(OrderContext context)
        {
            Console.WriteLine($"[Cancelled] Cannot return cancelled order #{context.OrderId}");
        }

        public string GetStateName()
        {
            return "Cancelled";
        }
    }
}