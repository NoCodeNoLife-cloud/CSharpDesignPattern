using State.Contexts;

namespace State.States
{
    /// <summary>
    /// Paid state implementation
    /// Order has been paid but not yet shipped
    /// </summary>
    public class PaidState : IOrderState
    {
        public void ProcessPayment(OrderContext context, decimal amount)
        {
            Console.WriteLine($"[Paid] Order #{context.OrderId} is already paid - cannot process duplicate payment");
        }

        public void ShipOrder(OrderContext context)
        {
            Console.WriteLine($"[Paid] Order #{context.OrderId} shipped successfully");
            context.CurrentState = new ShippedState();
        }

        public void DeliverOrder(OrderContext context)
        {
            Console.WriteLine($"[Paid] Cannot deliver order #{context.OrderId} - order must be shipped first");
        }

        public void CancelOrder(OrderContext context)
        {
            Console.WriteLine($"[Paid] Order #{context.OrderId} cancelled - refund will be processed");
            context.CurrentState = new CancelledState();
        }

        public void ReturnOrder(OrderContext context)
        {
            Console.WriteLine($"[Paid] Cannot return order #{context.OrderId} - order not delivered yet");
        }

        public string GetStateName()
        {
            return "Paid";
        }
    }
}