namespace Mediator.Components
{
    /// <summary>
    /// Chat user implementation
    /// Represents a participant in the chat system
    /// </summary>
    public class ChatUser : IUser
    {
        public string UserId { get; private set; }
        public string UserName { get; private set; }
        public IChatRoomMediator? Mediator { get; set; }
        public UserStatus Status { get; set; }

        public ChatUser(string userId, string userName)
        {
            UserId = userId;
            UserName = userName;
            Status = UserStatus.Online;
            Console.WriteLine($"[User] {UserName} created with ID: {UserId}");
        }

        public void SendMessage(string toUserId, string message)
        {
            if (Mediator == null)
            {
                Console.WriteLine($"[{UserName}] Cannot send message - not connected to chat room");
                return;
            }

            Console.WriteLine($"[{UserName}] Sending private message to {toUserId}: {message}");
            Mediator.SendMessage(UserId, toUserId, message);
        }

        public void BroadcastMessage(string message)
        {
            if (Mediator == null)
            {
                Console.WriteLine($"[{UserName}] Cannot broadcast message - not connected to chat room");
                return;
            }

            Console.WriteLine($"[{UserName}] Broadcasting: {message}");
            Mediator.BroadcastMessage(UserId, message);
        }

        public void ReceiveMessage(string fromUserId, string message)
        {
            var fromUser = Mediator?.GetUser(fromUserId);
            var fromUserName = fromUser?.UserName ?? fromUserId;
            
            Console.WriteLine($"[{UserName}] Received message from {fromUserName}: {message}");
        }

        public void ReceiveNotification(string notification)
        {
            Console.WriteLine($"[{UserName}] Notification: {notification}");
        }

        public void JoinChatRoom(IChatRoomMediator mediator)
        {
            Mediator = mediator;
            mediator.RegisterUser(this);
            Status = UserStatus.Online;
            Console.WriteLine($"[{UserName}] Joined chat room");
        }

        public void LeaveChatRoom()
        {
            if (Mediator != null)
            {
                Mediator.UnregisterUser(UserId);
                Mediator = null;
                Status = UserStatus.Offline;
                Console.WriteLine($"[{UserName}] Left chat room");
            }
        }

        public UserStatus GetStatus()
        {
            return Status;
        }

        public void SetStatus(UserStatus status)
        {
            Status = status;
            Console.WriteLine($"[{UserName}] Status changed to {status}");
        }

        public void SetAway(string reason = "")
        {
            Status = UserStatus.Away;
            var message = string.IsNullOrEmpty(reason) ? "is away" : $"is away ({reason})";
            Mediator?.BroadcastMessage(UserId, message);
        }

        public void SetBusy(string reason = "")
        {
            Status = UserStatus.Busy;
            var message = string.IsNullOrEmpty(reason) ? "is busy" : $"is busy ({reason})";
            Mediator?.BroadcastMessage(UserId, message);
        }

        public override string ToString()
        {
            return $"{UserName} ({UserId}) - {Status}";
        }
    }
}