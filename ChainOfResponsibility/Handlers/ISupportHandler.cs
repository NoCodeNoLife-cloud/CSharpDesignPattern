using ChainOfResponsibility.Models;

namespace ChainOfResponsibility.Handlers
{
    /// <summary>
    /// Support ticket handler interface
    /// Defines the contract for handling support tickets in a chain
    /// </summary>
    public interface ISupportHandler
    {
        /// <summary>
        /// Sets the next handler in the chain
        /// </summary>
        ISupportHandler SetNext(ISupportHandler nextHandler);

        /// <summary>
        /// Handles the support ticket
        /// </summary>
        void HandleRequest(SupportTicket ticket);

        /// <summary>
        /// Gets the handler level/name
        /// </summary>
        string GetHandlerLevel();
    }
}