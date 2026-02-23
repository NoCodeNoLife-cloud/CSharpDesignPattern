namespace Mediator.Components
{
    /// <summary>
    /// Chat room mediator interface
    /// Defines the contract for chat room communication
    /// </summary>
    public interface IChatRoomMediator
    {
        /// <summary>
        /// Registers a user in the chat room
        /// </summary>
        void RegisterUser(IUser user);
        
        /// <summary>
        /// Sends a message from one user to another
        /// </summary>
        void SendMessage(string fromUserId, string toUserId, string message);
        
        /// <summary>
        /// Sends a broadcast message to all users
        /// </summary>
        void BroadcastMessage(string fromUserId, string message);
        
        /// <summary>
        /// Removes a user from the chat room
        /// </summary>
        void UnregisterUser(string userId);
        
        /// <summary>
        /// Gets list of online users
        /// </summary>
        List<string> GetOnlineUsers();
        
        /// <summary>
        /// Gets user by ID
        /// </summary>
        IUser? GetUser(string userId);
    }
}