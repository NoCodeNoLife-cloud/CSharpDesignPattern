using ChainOfResponsibility.Models;

namespace ChainOfResponsibility.Handlers
{
    /// <summary>
    /// Critical support handler implementation
    /// Handles critical priority tickets (CEO level)
    /// </summary>
    public class CriticalSupportHandler : BaseSupportHandler
    {
        public CriticalSupportHandler() : base("CEO/Critical Support", SupportPriority.Critical)
        {
        }

        protected override void ProcessTicket(SupportTicket ticket)
        {
            Console.WriteLine($"\n[{_handlerName}] EMERGENCY HANDLING ticket #{ticket.TicketId}");
            ticket.Status = TicketStatus.InProgress;
            ticket.AssignedHandler = _handlerName;
            
            // Simulate urgent processing time
            Thread.Sleep(3000);
            
            ticket.AddResolutionStep("INITIATED CRISIS MANAGEMENT PROTOCOL");
            ticket.AddResolutionStep("Assembled emergency response team");
            ticket.AddResolutionStep("Conducted executive briefing");
            ticket.AddResolutionStep("Activated business continuity plan");
            ticket.AddResolutionStep("Coordinated with external vendors");
            ticket.AddResolutionStep("Implemented emergency fixes");
            
            // Critical resolution for CEO level
            var resolutions = new[]
            {
                "Deployed emergency system rollback",
                "Activated disaster recovery procedures",
                "Implemented temporary workaround solution",
                "Coordinated with legal/compliance teams",
                "Managed customer/stakeholder communications",
                "Established emergency escalation procedures"
            };
            
            var random = new Random();
            ticket.AddResolutionStep(resolutions[random.Next(resolutions.Length)]);
            ticket.AddResolutionStep("Documented incident for post-mortem analysis");
            ticket.AddResolutionStep("Scheduled executive review meeting");
            ticket.AddResolutionStep("Updated risk management protocols");
            
            ticket.MarkResolved(_handlerName);
            
            Console.WriteLine($"[{_handlerName}] CRITICAL ticket #{ticket.TicketId} RESOLVED - Business operations restored");
        }
    }
}