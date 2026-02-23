using State.Contexts;

namespace State.States
{
    /// <summary>
    /// Returned state implementation
    /// Order has been returned by customer
    /// </summary>
    public class ReturnedState : IOrderState
    {
        private readonly DateTime _returnDate;

        public ReturnedState()
        {
            _returnDate = DateTime.Now;
        }

        public void ProcessPayment(OrderContext context, decimal amount)
        {
            Console.WriteLine($"[Returned] Cannot process payment for returned order #{context.OrderId}");
        }

        public void ShipOrder(OrderContext context)
        {
            Console.WriteLine($"[Returned] Cannot ship returned order #{context.OrderId}");
        }

        public void DeliverOrder(OrderContext context)
        {
            Console.WriteLine($"[Returned] Cannot deliver returned order #{context.OrderId}");
        }

        public void CancelOrder(OrderContext context)
        {
            Console.WriteLine($"[Returned] Order #{context.OrderId} is already returned");
        }

        public void ReturnOrder(OrderContext context)
        {
            Console.WriteLine($"[Returned] Order #{context.OrderId} is already returned");
        }

        public string GetStateName()
        {
            return "Returned";
        }
    }
}