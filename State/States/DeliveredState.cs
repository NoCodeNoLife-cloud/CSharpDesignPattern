using State.Contexts;

namespace State.States
{
    /// <summary>
    /// Delivered state implementation
    /// Order has been delivered to customer
    /// </summary>
    public class DeliveredState : IOrderState
    {
        private readonly DateTime _deliveryDate;

        public DeliveredState()
        {
            _deliveryDate = DateTime.Now;
        }

        public void ProcessPayment(OrderContext context, decimal amount)
        {
            Console.WriteLine($"[Delivered] Order #{context.OrderId} is already paid and delivered");
        }

        public void ShipOrder(OrderContext context)
        {
            Console.WriteLine($"[Delivered] Order #{context.OrderId} is already delivered");
        }

        public void DeliverOrder(OrderContext context)
        {
            Console.WriteLine($"[Delivered] Order #{context.OrderId} is already delivered");
        }

        public void CancelOrder(OrderContext context)
        {
            var daysSinceDelivery = (DateTime.Now - _deliveryDate).Days;
            if (daysSinceDelivery <= 30)
            {
                Console.WriteLine($"[Delivered] Order #{context.OrderId} return accepted within warranty period");
                context.CurrentState = new ReturnedState();
            }
            else
            {
                Console.WriteLine($"[Delivered] Order #{context.OrderId} return rejected - outside warranty period ({daysSinceDelivery} days)");
            }
        }

        public void ReturnOrder(OrderContext context)
        {
            var daysSinceDelivery = (DateTime.Now - _deliveryDate).Days;
            if (daysSinceDelivery <= 30)
            {
                Console.WriteLine($"[Delivered] Order #{context.OrderId} return processed successfully");
                context.CurrentState = new ReturnedState();
            }
            else
            {
                Console.WriteLine($"[Delivered] Order #{context.OrderId} return rejected - outside warranty period ({daysSinceDelivery} days)");
            }
        }

        public string GetStateName()
        {
            return "Delivered";
        }
    }
}