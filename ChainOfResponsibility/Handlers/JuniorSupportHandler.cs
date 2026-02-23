using ChainOfResponsibility.Models;

namespace ChainOfResponsibility.Handlers
{
    /// <summary>
    /// Junior support handler implementation
    /// Handles low priority tickets
    /// </summary>
    public class JuniorSupportHandler : BaseSupportHandler
    {
        public JuniorSupportHandler() : base("Junior Support", SupportPriority.Low)
        {
        }

        protected override void ProcessTicket(SupportTicket ticket)
        {
            Console.WriteLine($"\n[{_handlerName}] Processing ticket #{ticket.TicketId}");
            ticket.Status = TicketStatus.InProgress;
            ticket.AssignedHandler = _handlerName;
            
            // Simulate processing time
            Thread.Sleep(500);
            
            ticket.AddResolutionStep("Initial assessment completed");
            ticket.AddResolutionStep("Checked knowledge base for similar issues");
            
            // Simple resolution for junior level
            var resolutions = new[]
            {
                "Provided user manual reference",
                "Restarted application/service",
                "Cleared browser cache",
                "Verified account credentials",
                "Checked network connectivity"
            };
            
            var random = new Random();
            ticket.AddResolutionStep(resolutions[random.Next(resolutions.Length)]);
            ticket.AddResolutionStep("Follow-up scheduled if issue persists");
            
            ticket.MarkResolved(_handlerName);
            
            Console.WriteLine($"[{_handlerName}] Ticket #{ticket.TicketId} resolved successfully");
        }
    }
}