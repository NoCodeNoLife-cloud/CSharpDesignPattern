namespace Mediator.Components
{
    /// <summary>
    /// Chat user interface
    /// Defines the contract for chat participants
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Gets the user ID
        /// </summary>
        string UserId { get; }
        
        /// <summary>
        /// Gets the user name
        /// </summary>
        string UserName { get; }
        
        /// <summary>
        /// Gets or sets the chat room mediator
        /// </summary>
        IChatRoomMediator? Mediator { get; set; }
        
        /// <summary>
        /// Sends a message to another user
        /// </summary>
        void SendMessage(string toUserId, string message);
        
        /// <summary>
        /// Sends a broadcast message to all users
        /// </summary>
        void BroadcastMessage(string message);
        
        /// <summary>
        /// Receives a message from another user
        /// </summary>
        void ReceiveMessage(string fromUserId, string message);
        
        /// <summary>
        /// Receives a system notification
        /// </summary>
        void ReceiveNotification(string notification);
        
        /// <summary>
        /// Joins the chat room
        /// </summary>
        void JoinChatRoom(IChatRoomMediator mediator);
        
        /// <summary>
        /// Leaves the chat room
        /// </summary>
        void LeaveChatRoom();
        
        /// <summary>
        /// Gets user status
        /// </summary>
        UserStatus GetStatus();
    }

    /// <summary>
    /// User status enumeration
    /// </summary>
    public enum UserStatus
    {
        Offline,
        Online,
        Away,
        Busy
    }
}