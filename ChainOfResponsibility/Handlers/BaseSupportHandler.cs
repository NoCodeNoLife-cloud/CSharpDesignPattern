using ChainOfResponsibility.Models;

namespace ChainOfResponsibility.Handlers
{
    /// <summary>
    /// Base support handler abstract class
    /// Provides common functionality for all support handlers
    /// </summary>
    public abstract class BaseSupportHandler : ISupportHandler
    {
        protected ISupportHandler? _nextHandler;
        protected readonly string _handlerName;
        protected readonly SupportPriority _handledPriority;

        protected BaseSupportHandler(string handlerName, SupportPriority handledPriority)
        {
            _handlerName = handlerName;
            _handledPriority = handledPriority;
        }

        public ISupportHandler SetNext(ISupportHandler nextHandler)
        {
            _nextHandler = nextHandler;
            Console.WriteLine($"[{_handlerName}] Next handler set to: {nextHandler.GetHandlerLevel()}");
            return nextHandler;
        }

        public virtual void HandleRequest(SupportTicket ticket)
        {
            if (ticket.Priority <= _handledPriority)
            {
                ProcessTicket(ticket);
            }
            else if (_nextHandler != null)
            {
                Console.WriteLine($"[{_handlerName}] Escalating ticket #{ticket.TicketId} to {_nextHandler.GetHandlerLevel()}");
                ticket.MarkEscalated(_handlerName);
                _nextHandler.HandleRequest(ticket);
            }
            else
            {
                Console.WriteLine($"[{_handlerName}] ERROR: No handler available for priority {ticket.Priority}. Ticket #{ticket.TicketId} remains unresolved.");
                ticket.Status = TicketStatus.Escalated;
            }
        }

        public string GetHandlerLevel()
        {
            return $"{_handlerName} (handles {Enum.GetName(typeof(SupportPriority), _handledPriority)} priority)";
        }

        protected abstract void ProcessTicket(SupportTicket ticket);
    }
}