using State.States;

namespace State.Contexts
{
    /// <summary>
    /// Order context class
    /// Manages the current state and delegates operations to the current state
    /// </summary>
    public class OrderContext
    {
        private IOrderState _currentState;
        private readonly string _orderId;
        private decimal _orderAmount;
        private DateTime _createdDate;

        public OrderContext(string orderId, decimal amount)
        {
            _orderId = orderId;
            _orderAmount = amount;
            _createdDate = DateTime.Now;
            _currentState = new PendingState(); // Initial state
        }

        public IOrderState CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                Console.WriteLine($"[State Change] Order {_orderId} transitioned to: {value.GetStateName()}");
            }
        }

        public string OrderId => _orderId;
        public decimal OrderAmount => _orderAmount;
        public DateTime CreatedDate => _createdDate;

        // Delegate methods to current state
        public void ProcessPayment(decimal amount)
        {
            _currentState.ProcessPayment(this, amount);
        }

        public void ShipOrder()
        {
            _currentState.ShipOrder(this);
        }

        public void DeliverOrder()
        {
            _currentState.DeliverOrder(this);
        }

        public void CancelOrder()
        {
            _currentState.CancelOrder(this);
        }

        public void ReturnOrder()
        {
            _currentState.ReturnOrder(this);
        }

        public string GetCurrentStateName()
        {
            return _currentState.GetStateName();
        }

        public OrderInfo GetOrderInfo()
        {
            return new OrderInfo
            {
                OrderId = _orderId,
                Amount = _orderAmount,
                CreatedDate = _createdDate,
                CurrentState = _currentState.GetStateName(),
                DaysSinceCreation = (DateTime.Now - _createdDate).Days
            };
        }

        public override string ToString()
        {
            return $"Order #{_orderId} - ${_orderAmount:F2} - {_currentState.GetStateName()}";
        }
    }

    /// <summary>
    /// Order information data structure
    /// </summary>
    public class OrderInfo
    {
        public string OrderId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CurrentState { get; set; } = string.Empty;
        public int DaysSinceCreation { get; set; }
    }
}