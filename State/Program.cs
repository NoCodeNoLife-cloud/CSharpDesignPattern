// See https://aka.ms/new-console-template for more information
using State.Contexts;
using State.States;

namespace State
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== State Pattern Implementation Examples ===\n");

            try
            {
                // Demonstrate normal order flow
                DemonstrateNormalOrderFlow();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate order cancellation
                DemonstrateOrderCancellation();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate order return
                DemonstrateOrderReturn();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate invalid operations
                DemonstrateInvalidOperations();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate state pattern benefits
                DemonstrateStatePatternBenefits();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\n=== State Pattern Demo Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates normal order processing flow
        /// </summary>
        private static void DemonstrateNormalOrderFlow()
        {
            Console.WriteLine("1. Normal Order Flow Demo");
            Console.WriteLine("=========================");
            
            var order = new OrderContext("ORD-001", 299.99m);
            Console.WriteLine($"Created: {order}\n");
            
            // Process payment
            order.ProcessPayment(299.99m);
            Console.WriteLine($"Current: {order}\n");
            
            // Ship order
            order.ShipOrder();
            Console.WriteLine($"Current: {order}\n");
            
            // Deliver order
            order.DeliverOrder();
            Console.WriteLine($"Final: {order}\n");
            
            DisplayOrderSummary(order);
        }

        /// <summary>
        /// Demonstrates order cancellation at different stages
        /// </summary>
        private static void DemonstrateOrderCancellation()
        {
            Console.WriteLine("\n2. Order Cancellation Demo");
            Console.WriteLine("===========================");
            
            // Cancel during pending state
            var pendingOrder = new OrderContext("ORD-002", 150.00m);
            Console.WriteLine($"Created: {pendingOrder}");
            pendingOrder.CancelOrder();
            Console.WriteLine($"Result: {pendingOrder}\n");
            
            // Cancel during paid state
            var paidOrder = new OrderContext("ORD-003", 75.50m);
            Console.WriteLine($"Created: {paidOrder}");
            paidOrder.ProcessPayment(75.50m);
            paidOrder.CancelOrder();
            Console.WriteLine($"Result: {paidOrder}\n");
            
            // Try to cancel delivered order (should fail)
            var deliveredOrder = new OrderContext("ORD-004", 199.99m);
            deliveredOrder.ProcessPayment(199.99m);
            deliveredOrder.ShipOrder();
            deliveredOrder.DeliverOrder();
            Console.WriteLine($"Delivered: {deliveredOrder}");
            deliveredOrder.CancelOrder();
            Console.WriteLine($"Result: {deliveredOrder}\n");
        }

        /// <summary>
        /// Demonstrates order return process
        /// </summary>
        private static void DemonstrateOrderReturn()
        {
            Console.WriteLine("\n3. Order Return Demo");
            Console.WriteLine("====================");
            
            var order = new OrderContext("ORD-005", 125.75m);
            order.ProcessPayment(125.75m);
            order.ShipOrder();
            order.DeliverOrder();
            Console.WriteLine($"Delivered: {order}");
            
            // Return within warranty period
            order.ReturnOrder();
            Console.WriteLine($"Result: {order}\n");
            
            // Try to return already returned order
            order.ReturnOrder();
            Console.WriteLine($"Second attempt: {order}\n");
        }

        /// <summary>
        /// Demonstrates invalid operations in different states
        /// </summary>
        private static void DemonstrateInvalidOperations()
        {
            Console.WriteLine("\n4. Invalid Operations Demo");
            Console.WriteLine("===========================");
            
            var order = new OrderContext("ORD-006", 89.99m);
            Console.WriteLine($"Initial: {order}");
            
            // Try operations in wrong sequence
            Console.WriteLine("\nAttempting invalid operations:");
            order.DeliverOrder(); // Should fail - not shipped
            order.ShipOrder();    // Should fail - not paid
            
            // Proper sequence
            Console.WriteLine("\nCorrecting with proper sequence:");
            order.ProcessPayment(89.99m);
            order.ShipOrder();
            order.DeliverOrder();
            Console.WriteLine($"Final: {order}\n");
        }

        /// <summary>
        /// Demonstrates state pattern benefits and concepts
        /// </summary>
        private static void DemonstrateStatePatternBenefits()
        {
            Console.WriteLine("\n5. State Pattern Benefits Demo");
            Console.WriteLine("===============================");
            
            Console.WriteLine("State Pattern Key Benefits:");
            Console.WriteLine("• Eliminates complex conditional logic");
            Console.WriteLine("• Makes state transitions explicit and manageable");
            Console.WriteLine("• Allows easy addition of new states");
            Console.WriteLine("• Centralizes state-specific behavior");
            Console.WriteLine("• Improves code maintainability and readability");
            
            Console.WriteLine("\nCommon Use Cases:");
            Console.WriteLine("• Order processing systems");
            Console.WriteLine("• Workflow management");
            Console.WriteLine("• Game character states");
            Console.WriteLine("• UI component states");
            Console.WriteLine("• Protocol state machines");
            Console.WriteLine("• Finite state automata");
            
            Console.WriteLine("\nWhen to Use State Pattern:");
            Console.WriteLine("• Object behavior depends on its state");
            Console.WriteLine("• Complex conditional logic based on state");
            Console.WriteLine("• Frequent state transitions");
            Console.WriteLine("• Need to add new states easily");
            Console.WriteLine("• Want to avoid large switch/if-else statements");
        }

        /// <summary>
        /// Displays comprehensive order summary
        /// </summary>
        private static void DisplayOrderSummary(OrderContext order)
        {
            var info = order.GetOrderInfo();
            Console.WriteLine("Order Summary:");
            Console.WriteLine($"  Order ID: {info.OrderId}");
            Console.WriteLine($"  Amount: ${info.Amount:F2}");
            Console.WriteLine($"  Created: {info.CreatedDate:yyyy-MM-dd HH:mm}");
            Console.WriteLine($"  Current State: {info.CurrentState}");
            Console.WriteLine($"  Age: {info.DaysSinceCreation} days");
        }
    }
}