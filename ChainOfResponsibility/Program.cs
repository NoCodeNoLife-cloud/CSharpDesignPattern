// See https://aka.ms/new-console-template for more information
using ChainOfResponsibility.Managers;
using ChainOfResponsibility.Models;

namespace ChainOfResponsibility
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Chain of Responsibility Pattern Implementation Examples ===\n");

            try
            {
                // Demonstrate basic chain operation
                DemonstrateBasicChain();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate escalation process
                DemonstrateEscalation();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate mixed priority handling
                DemonstrateMixedPriorities();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate critical situation handling
                DemonstrateCriticalSituation();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate pattern benefits
                DemonstratePatternBenefits();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\n=== Chain of Responsibility Demo Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates basic chain of responsibility operation
        /// </summary>
        private static void DemonstrateBasicChain()
        {
            Console.WriteLine("1. Basic Chain Operation Demo");
            Console.WriteLine("=============================");
            
            var supportSystem = new SupportSystemManager();
            
            // Test different priority levels
            var lowTicket = new SupportTicket("John Doe", "Cannot print document", SupportPriority.Low);
            var mediumTicket = new SupportTicket("Jane Smith", "Application crashes on startup", SupportPriority.Medium);
            var highTicket = new SupportTicket("Bob Johnson", "Database connection timeout", SupportPriority.High);
            
            supportSystem.SubmitTicket(lowTicket);
            supportSystem.SubmitTicket(mediumTicket);
            supportSystem.SubmitTicket(highTicket);
            
            supportSystem.DisplayStatistics();
        }

        /// <summary>
        /// Demonstrates escalation process through the chain
        /// </summary>
        private static void DemonstrateEscalation()
        {
            Console.WriteLine("\n2. Escalation Process Demo");
            Console.WriteLine("===========================");
            
            var supportSystem = new SupportSystemManager();
            
            // Tickets that will be escalated through the chain
            var tickets = new[]
            {
                new SupportTicket("Alice Brown", "Need help with basic navigation", SupportPriority.Low),
                new SupportTicket("Charlie Wilson", "API integration failing", SupportPriority.Medium),
                new SupportTicket("Diana Lee", "System performance degradation", SupportPriority.High),
                new SupportTicket("Edward Davis", "Complete system outage affecting customers", SupportPriority.Critical)
            };
            
            Console.WriteLine("\nSubmitting tickets that will demonstrate escalation:");
            supportSystem.SubmitTickets(tickets);
            
            // Show detailed information for escalated tickets
            Console.WriteLine("\nDetailed escalation tracking:");
            foreach (var ticket in tickets)
            {
                supportSystem.DisplayTicketDetails(ticket.TicketId);
            }
            
            supportSystem.DisplayStatistics();
        }

        /// <summary>
        /// Demonstrates handling of mixed priority tickets
        /// </summary>
        private static void DemonstrateMixedPriorities()
        {
            Console.WriteLine("\n3. Mixed Priority Handling Demo");
            Console.WriteLine("================================");
            
            var supportSystem = new SupportSystemManager();
            
            // Mix of different priority tickets submitted simultaneously
            var mixedTickets = new[]
            {
                new SupportTicket("User1", "Forgot password", SupportPriority.Low),
                new SupportTicket("User2", "Feature request", SupportPriority.Low),
                new SupportTicket("User3", "Report generation slow", SupportPriority.Medium),
                new SupportTicket("User4", "Permission denied error", SupportPriority.Medium),
                new SupportTicket("User5", "Data synchronization issue", SupportPriority.High),
                new SupportTicket("User6", "Security vulnerability detected", SupportPriority.Critical)
            };
            
            Console.WriteLine($"\nProcessing {mixedTickets.Length} tickets with mixed priorities:");
            supportSystem.SubmitTickets(mixedTickets);
            
            // Show resolution distribution
            Console.WriteLine("\nPriority Distribution Analysis:");
            var priorityGroups = mixedTickets.GroupBy(t => t.Priority)
                .OrderBy(g => g.Key)
                .Select(g => new { Priority = g.Key, Count = g.Count() });
            
            foreach (var group in priorityGroups)
            {
                Console.WriteLine($"  {group.Priority}: {group.Count} tickets");
            }
            
            supportSystem.DisplayStatistics();
        }

        /// <summary>
        /// Demonstrates critical situation handling
        /// </summary>
        private static void DemonstrateCriticalSituation()
        {
            Console.WriteLine("\n4. Critical Situation Handling Demo");
            Console.WriteLine("====================================");
            
            var supportSystem = new SupportSystemManager();
            
            Console.WriteLine("\n🚨 EMERGENCY SCENARIO: Multiple critical issues reported");
            
            // Critical situation simulation
            var criticalTickets = new[]
            {
                new SupportTicket("CTO", "Production database corruption", SupportPriority.Critical),
                new SupportTicket("CEO", "Customer-facing service downtime", SupportPriority.Critical),
                new SupportTicket("Security Team", "Potential data breach detected", SupportPriority.Critical),
                new SupportTicket("Operations", "Payment processing system failure", SupportPriority.Critical)
            };
            
            Console.WriteLine($"\nHandling {criticalTickets.Length} CRITICAL tickets:");
            supportSystem.SubmitTickets(criticalTickets);
            
            // Detailed analysis of critical handling
            Console.WriteLine("\nCritical Incident Response Analysis:");
            foreach (var ticket in criticalTickets)
            {
                Console.WriteLine($"  Ticket #{ticket.TicketId}: {ticket.GetTimeToResolve().TotalMinutes:F1} minutes to resolve");
            }
            
            supportSystem.DisplayStatistics();
        }

        /// <summary>
        /// Demonstrates chain of responsibility pattern benefits
        /// </summary>
        private static void DemonstratePatternBenefits()
        {
            Console.WriteLine("\n5. Chain of Responsibility Pattern Benefits Demo");
            Console.WriteLine("================================================");
            
            Console.WriteLine("Chain of Responsibility Pattern Key Benefits:");
            Console.WriteLine("• Decouples senders from receivers");
            Console.WriteLine("• Provides flexible request handling");
            Console.WriteLine("• Enables dynamic chain configuration");
            Console.WriteLine("• Supports hierarchical processing");
            Console.WriteLine("• Allows easy addition of new handlers");
            
            Console.WriteLine("\nCommon Use Cases:");
            Console.WriteLine("• Customer support ticket routing");
            Console.WriteLine("• Approval workflows");
            Console.WriteLine("• Event handling systems");
            Console.WriteLine("• Middleware processing");
            Console.WriteLine("• Exception handling chains");
            Console.WriteLine("• Logging frameworks");
            
            Console.WriteLine("\nWhen to Use Chain of Responsibility Pattern:");
            Console.WriteLine("• Multiple objects can handle a request");
            Console.WriteLine("• Handler should be determined automatically");
            Console.WriteLine("• Need flexible assignment of responsibilities");
            Console.WriteLine("• Want to avoid coupling request senders to receivers");
            Console.WriteLine("• Need to process requests in a specific order");
            
            Console.WriteLine("\nHandler Chain Structure:");
            Console.WriteLine("Junior Support → Senior Support → Expert Support → CEO/Critical Support");
            Console.WriteLine("Priority Levels: Low → Medium → High → Critical");
        }
    }
}