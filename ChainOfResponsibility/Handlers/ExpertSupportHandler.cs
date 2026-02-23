using ChainOfResponsibility.Models;

namespace ChainOfResponsibility.Handlers
{
    /// <summary>
    /// Expert support handler implementation
    /// Handles high priority tickets
    /// </summary>
    public class ExpertSupportHandler : BaseSupportHandler
    {
        public ExpertSupportHandler() : base("Expert Support", SupportPriority.High)
        {
        }

        protected override void ProcessTicket(SupportTicket ticket)
        {
            Console.WriteLine($"\n[{_handlerName}] Processing ticket #{ticket.TicketId}");
            ticket.Status = TicketStatus.InProgress;
            ticket.AssignedHandler = _handlerName;
            
            // Simulate processing time
            Thread.Sleep(2000);
            
            ticket.AddResolutionStep("Performed deep system analysis");
            ticket.AddResolutionStep("Reviewed architecture and integration points");
            ticket.AddResolutionStep("Conducted code review/debugging session");
            ticket.AddResolutionStep("Collaborated with development team");
            
            // Advanced resolution for expert level
            var resolutions = new[]
            {
                "Implemented custom workaround solution",
                "Designed and deployed hotfix",
                "Restructured database queries for performance",
                "Created automated monitoring scripts",
                "Established failover procedures",
                "Developed custom API integration"
            };
            
            var random = new Random();
            ticket.AddResolutionStep(resolutions[random.Next(resolutions.Length)]);
            ticket.AddResolutionStep("Created comprehensive documentation");
            ticket.AddResolutionStep("Scheduled follow-up review meeting");
            ticket.AddResolutionStep("Updated system architecture diagrams");
            
            ticket.MarkResolved(_handlerName);
            
            Console.WriteLine($"[{_handlerName}] Ticket #{ticket.TicketId} resolved successfully");
        }
    }
}