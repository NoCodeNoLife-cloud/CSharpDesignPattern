using ChainOfResponsibility.Models;

namespace ChainOfResponsibility.Handlers
{
    /// <summary>
    /// Senior support handler implementation
    /// Handles medium priority tickets
    /// </summary>
    public class SeniorSupportHandler : BaseSupportHandler
    {
        public SeniorSupportHandler() : base("Senior Support", SupportPriority.Medium)
        {
        }

        protected override void ProcessTicket(SupportTicket ticket)
        {
            Console.WriteLine($"\n[{_handlerName}] Processing ticket #{ticket.TicketId}");
            ticket.Status = TicketStatus.InProgress;
            ticket.AssignedHandler = _handlerName;
            
            // Simulate processing time
            Thread.Sleep(1000);
            
            ticket.AddResolutionStep("Conducted detailed troubleshooting");
            ticket.AddResolutionStep("Analyzed system logs and error messages");
            ticket.AddResolutionStep("Performed remote desktop assistance");
            
            // Intermediate resolution for senior level
            var resolutions = new[]
            {
                "Configured application settings",
                "Applied software patch/update",
                "Modified user permissions",
                "Optimized system performance",
                "Integrated with third-party services",
                "Created custom configuration script"
            };
            
            var random = new Random();
            ticket.AddResolutionStep(resolutions[random.Next(resolutions.Length)]);
            ticket.AddResolutionStep("Documented solution in knowledge base");
            ticket.AddResolutionStep("Trained user on prevention measures");
            
            ticket.MarkResolved(_handlerName);
            
            Console.WriteLine($"[{_handlerName}] Ticket #{ticket.TicketId} resolved successfully");
        }
    }
}