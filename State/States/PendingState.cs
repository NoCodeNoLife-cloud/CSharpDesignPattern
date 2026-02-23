using State.Contexts;

namespace State.States
{
    /// <summary>
    /// Pending state implementation
    /// Initial state where order is created but not yet paid
    /// </summary>
    public class PendingState : IOrderState
    {
        public void ProcessPayment(OrderContext context, decimal amount)
        {
            if (amount >= context.OrderAmount)
            {
                Console.WriteLine($"[Pending] Payment of ${amount:F2} processed for order #{context.OrderId}");
                context.CurrentState = new PaidState();
            }
            else
            {
                Console.WriteLine($"[Pending] Payment failed - insufficient amount. Required: ${context.OrderAmount:F2}, Provided: ${amount:F2}");
            }
        }

        public void ShipOrder(OrderContext context)
        {
            Console.WriteLine($"[Pending] Cannot ship order #{context.OrderId} - payment required first");
        }

        public void DeliverOrder(OrderContext context)
        {
            Console.WriteLine($"[Pending] Cannot deliver order #{context.OrderId} - order not paid and shipped");
        }

        public void CancelOrder(OrderContext context)
        {
            Console.WriteLine($"[Pending] Order #{context.OrderId} cancelled successfully");
            context.CurrentState = new CancelledState();
        }

        public void ReturnOrder(OrderContext context)
        {
            Console.WriteLine($"[Pending] Cannot return order #{context.OrderId} - order not delivered yet");
        }

        public string GetStateName()
        {
            return "Pending";
        }
    }
}