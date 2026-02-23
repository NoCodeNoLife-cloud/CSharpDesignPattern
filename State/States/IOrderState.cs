using State.Contexts;

namespace State.States
{
    /// <summary>
    /// Order state interface
    /// Defines the contract for different order states
    /// </summary>
    public interface IOrderState
    {
        /// <summary>
        /// Processes payment for the order
        /// </summary>
        void ProcessPayment(OrderContext context, decimal amount);
        
        /// <summary>
        /// Ships the order
        /// </summary>
        void ShipOrder(OrderContext context);
        
        /// <summary>
        /// Delivers the order
        /// </summary>
        void DeliverOrder(OrderContext context);
        
        /// <summary>
        /// Cancels the order
        /// </summary>
        void CancelOrder(OrderContext context);
        
        /// <summary>
        /// Returns the order
        /// </summary>
        void ReturnOrder(OrderContext context);
        
        /// <summary>
        /// Gets the current state name
        /// </summary>
        string GetStateName();
    }
}