using ChainOfResponsibility.Handlers;
using ChainOfResponsibility.Models;

namespace ChainOfResponsibility.Managers
{
    /// <summary>
    /// Support system manager
    /// Orchestrates the support handler chain and manages tickets
    /// </summary>
    public class SupportSystemManager
    {
        private readonly ISupportHandler _handlerChain;
        private readonly List<SupportTicket> _tickets = new List<SupportTicket>();
        private readonly Dictionary<SupportPriority, int> _resolutionStats = new Dictionary<SupportPriority, int>();

        public SupportSystemManager()
        {
            // Initialize resolution statistics
            foreach (SupportPriority priority in Enum.GetValues(typeof(SupportPriority)))
            {
                _resolutionStats[priority] = 0;
            }

            // Build the handler chain
            var junior = new JuniorSupportHandler();
            var senior = new SeniorSupportHandler();
            var expert = new ExpertSupportHandler();
            var critical = new CriticalSupportHandler();

            // Set up the chain: Junior -> Senior -> Expert -> Critical
            junior.SetNext(senior).SetNext(expert).SetNext(critical);
            
            _handlerChain = junior;
            
            Console.WriteLine("\n=== Support Handler Chain Initialized ===");
            Console.WriteLine("Chain: Junior → Senior → Expert → CEO/Critical");
            Console.WriteLine("==========================================\n");
        }

        public void SubmitTicket(SupportTicket ticket)
        {
            Console.WriteLine($"\n📥 NEW TICKET SUBMITTED: {ticket}");
            _tickets.Add(ticket);
            _handlerChain.HandleRequest(ticket);
            
            if (ticket.Status == TicketStatus.Resolved)
            {
                _resolutionStats[ticket.Priority]++;
            }
        }

        public void SubmitTickets(params SupportTicket[] tickets)
        {
            foreach (var ticket in tickets)
            {
                SubmitTicket(ticket);
            }
        }

        public List<SupportTicket> GetTickets()
        {
            return _tickets.ToList();
        }

        public List<SupportTicket> GetResolvedTickets()
        {
            return _tickets.Where(t => t.Status == TicketStatus.Resolved).ToList();
        }

        public List<SupportTicket> GetOpenTickets()
        {
            return _tickets.Where(t => t.Status != TicketStatus.Resolved).ToList();
        }

        public void DisplayStatistics()
        {
            Console.WriteLine("\n=== Support System Statistics ===");
            
            var resolvedTickets = GetResolvedTickets();
            var openTickets = GetOpenTickets();
            
            Console.WriteLine($"Total Tickets: {_tickets.Count}");
            Console.WriteLine($"Resolved Tickets: {resolvedTickets.Count}");
            Console.WriteLine($"Open Tickets: {openTickets.Count}");
            
            if (_tickets.Count > 0)
            {
                Console.WriteLine($"\nResolution Rate: {(double)resolvedTickets.Count / _tickets.Count:P1}");
                
                Console.WriteLine("\nResolution by Priority:");
                foreach (var kvp in _resolutionStats)
                {
                    Console.WriteLine($"  {kvp.Key}: {kvp.Value} tickets");
                }
                
                if (resolvedTickets.Any())
                {
                    var avgResolutionTime = resolvedTickets.Average(t => t.GetTimeToResolve().TotalMinutes);
                    Console.WriteLine($"\nAverage Resolution Time: {avgResolutionTime:F1} minutes");
                    
                    var handlerStats = resolvedTickets
                        .GroupBy(t => t.AssignedHandler)
                        .Select(g => new { Handler = g.Key, Count = g.Count() })
                        .OrderByDescending(g => g.Count);
                    
                    Console.WriteLine("\nHandler Performance:");
                    foreach (var stat in handlerStats)
                    {
                        Console.WriteLine($"  {stat.Handler}: {stat.Count} tickets resolved");
                    }
                }
            }
            
            Console.WriteLine("===============================\n");
        }

        public void DisplayTicketDetails(int ticketId)
        {
            var ticket = _tickets.FirstOrDefault(t => t.TicketId == ticketId);
            if (ticket == null)
            {
                Console.WriteLine($"Ticket #{ticketId} not found");
                return;
            }

            Console.WriteLine($"\n--- Ticket #{ticketId} Details ---");
            Console.WriteLine($"Customer: {ticket.CustomerName}");
            Console.WriteLine($"Issue: {ticket.IssueDescription}");
            Console.WriteLine($"Priority: {ticket.Priority}");
            Console.WriteLine($"Status: {ticket.Status}");
            Console.WriteLine($"Created: {ticket.CreatedTime:yyyy-MM-dd HH:mm:ss}");
            
            if (ticket.ResolvedTime.HasValue)
            {
                Console.WriteLine($"Resolved: {ticket.ResolvedTime.Value:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine($"Resolution Time: {ticket.GetTimeToResolve().TotalMinutes:F1} minutes");
            }
            else
            {
                Console.WriteLine($"Waiting Time: {(DateTime.Now - ticket.CreatedTime).TotalMinutes:F1} minutes");
            }
            
            if (!string.IsNullOrEmpty(ticket.AssignedHandler))
            {
                Console.WriteLine($"Assigned To: {ticket.AssignedHandler}");
            }
            
            if (ticket.ResolutionSteps.Any())
            {
                Console.WriteLine("\nResolution Steps:");
                foreach (var step in ticket.ResolutionSteps)
                {
                    Console.WriteLine($"  • {step}");
                }
            }
            
            Console.WriteLine("--- End Details ---\n");
        }

        public void ClearStatistics()
        {
            _tickets.Clear();
            foreach (var priority in Enum.GetValues<SupportPriority>())
            {
                _resolutionStats[priority] = 0;
            }
            Console.WriteLine("Support system statistics cleared");
        }
    }
}