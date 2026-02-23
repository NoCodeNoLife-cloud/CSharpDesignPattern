namespace ChainOfResponsibility.Models
{
    /// <summary>
    /// Support ticket model
    /// Represents a customer support request
    /// </summary>
    public class SupportTicket
    {
        public int TicketId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string IssueDescription { get; set; } = string.Empty;
        public SupportPriority Priority { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? ResolvedTime { get; set; }
        public string AssignedHandler { get; set; } = string.Empty;
        public TicketStatus Status { get; set; }
        public List<string> ResolutionSteps { get; set; } = new List<string>();

        public SupportTicket()
        {
            TicketId = new Random().Next(1000, 9999);
            CreatedTime = DateTime.Now;
            Status = TicketStatus.Open;
        }

        public SupportTicket(string customerName, string issueDescription, SupportPriority priority)
            : this()
        {
            CustomerName = customerName;
            IssueDescription = issueDescription;
            Priority = priority;
        }

        public void AddResolutionStep(string step)
        {
            ResolutionSteps.Add($"{DateTime.Now:HH:mm:ss} - {step}");
        }

        public void MarkResolved(string handlerName)
        {
            Status = TicketStatus.Resolved;
            ResolvedTime = DateTime.Now;
            AssignedHandler = handlerName;
            AddResolutionStep($"Ticket resolved by {handlerName}");
        }

        public void MarkEscalated(string handlerName)
        {
            AddResolutionStep($"Escalated from {handlerName}");
        }

        public TimeSpan GetTimeToResolve()
        {
            if (ResolvedTime.HasValue)
            {
                return ResolvedTime.Value - CreatedTime;
            }
            return DateTime.Now - CreatedTime;
        }

        public override string ToString()
        {
            var statusInfo = Status == TicketStatus.Resolved && ResolvedTime.HasValue
                ? $"Resolved in {GetTimeToResolve().TotalMinutes:F1} minutes by {AssignedHandler}"
                : $"{Status} - Waiting {(DateTime.Now - CreatedTime).TotalMinutes:F1} minutes";

            return $"Ticket #{TicketId} [{Priority}] - {CustomerName}: {IssueDescription} ({statusInfo})";
        }
    }

    /// <summary>
    /// Support priority levels
    /// </summary>
    public enum SupportPriority
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }

    /// <summary>
    /// Ticket status enumeration
    /// </summary>
    public enum TicketStatus
    {
        Open,
        InProgress,
        Resolved,
        Escalated
    }
}