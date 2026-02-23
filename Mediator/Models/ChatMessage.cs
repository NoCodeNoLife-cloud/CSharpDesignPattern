namespace Mediator.Models
{
    /// <summary>
    /// Chat message model
    /// Represents a message exchanged in the chat system
    /// </summary>
    public class ChatMessage
    {
        public string FromUserId { get; set; } = string.Empty;
        public string ToUserId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public MessageType MessageType { get; set; }
        public MessagePriority Priority { get; set; }

        public ChatMessage()
        {
            Timestamp = DateTime.Now;
            Priority = MessagePriority.Normal;
        }

        public override string ToString()
        {
            var typeIndicator = MessageType == MessageType.Private ? "[Private]" : "[Broadcast]";
            var priorityIndicator = Priority == MessagePriority.High ? "!" : "";
            
            return $"{typeIndicator}{priorityIndicator} {Timestamp:HH:mm:ss} - {FromUserId}: {Message}";
        }

        public string ToDetailedString()
        {
            return $"Message [{MessageType}] from {FromUserId} to {ToUserId}:\n" +
                   $"  Content: {Message}\n" +
                   $"  Time: {Timestamp:yyyy-MM-dd HH:mm:ss}\n" +
                   $"  Priority: {Priority}";
        }
    }

    /// <summary>
    /// Message type enumeration
    /// </summary>
    public enum MessageType
    {
        Private,
        Broadcast,
        System
    }

    /// <summary>
    /// Message priority enumeration
    /// </summary>
    public enum MessagePriority
    {
        Low,
        Normal,
        High
    }
}