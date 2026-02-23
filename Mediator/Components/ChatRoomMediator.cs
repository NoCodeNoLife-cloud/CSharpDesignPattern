using Mediator.Models;

namespace Mediator.Components
{
    /// <summary>
    /// Chat room mediator implementation
    /// Coordinates communication between chat users
    /// </summary>
    public class ChatRoomMediator : IChatRoomMediator
    {
        private readonly Dictionary<string, IUser> _users = new Dictionary<string, IUser>();
        private readonly List<ChatMessage> _messageHistory = new List<ChatMessage>();
        private readonly string _roomName;

        public ChatRoomMediator(string roomName)
        {
            _roomName = roomName;
            Console.WriteLine($"[ChatRoom] {_roomName} created");
        }

        public void RegisterUser(IUser user)
        {
            if (!_users.ContainsKey(user.UserId))
            {
                _users[user.UserId] = user;
                user.Mediator = this;
                
                // Notify other users
                BroadcastNotification($"{user.UserName} joined the chat room");
                
                Console.WriteLine($"[ChatRoom] User {user.UserName} ({user.UserId}) registered");
            }
        }

        public void SendMessage(string fromUserId, string toUserId, string message)
        {
            if (!_users.ContainsKey(fromUserId))
            {
                Console.WriteLine($"[ChatRoom] Sender {fromUserId} not found");
                return;
            }

            if (!_users.ContainsKey(toUserId))
            {
                Console.WriteLine($"[ChatRoom] Receiver {toUserId} not found");
                _users[fromUserId].ReceiveNotification($"User {toUserId} is not online");
                return;
            }

            var sender = _users[fromUserId];
            var receiver = _users[toUserId];
            
            // Create message record
            var chatMessage = new ChatMessage
            {
                FromUserId = fromUserId,
                ToUserId = toUserId,
                Message = message,
                Timestamp = DateTime.Now,
                MessageType = MessageType.Private
            };
            
            _messageHistory.Add(chatMessage);
            
            // Deliver message
            receiver.ReceiveMessage(fromUserId, message);
            
            Console.WriteLine($"[ChatRoom] Private message from {sender.UserName} to {receiver.UserName}: {message}");
        }

        public void BroadcastMessage(string fromUserId, string message)
        {
            if (!_users.ContainsKey(fromUserId))
            {
                Console.WriteLine($"[ChatRoom] Broadcaster {fromUserId} not found");
                return;
            }

            var sender = _users[fromUserId];
            
            // Create message record
            var chatMessage = new ChatMessage
            {
                FromUserId = fromUserId,
                Message = message,
                Timestamp = DateTime.Now,
                MessageType = MessageType.Broadcast
            };
            
            _messageHistory.Add(chatMessage);
            
            // Send to all users except sender
            foreach (var user in _users.Values.Where(u => u.UserId != fromUserId))
            {
                user.ReceiveMessage(fromUserId, message);
            }
            
            Console.WriteLine($"[ChatRoom] Broadcast from {sender.UserName}: {message}");
        }

        public void UnregisterUser(string userId)
        {
            if (_users.TryGetValue(userId, out var user))
            {
                _users.Remove(userId);
                user.Mediator = null;
                
                // Notify remaining users
                BroadcastNotification($"{user.UserName} left the chat room");
                
                Console.WriteLine($"[ChatRoom] User {user.UserName} ({userId}) unregistered");
            }
        }

        public List<string> GetOnlineUsers()
        {
            return _users.Values.Select(u => u.UserName).ToList();
        }

        public IUser? GetUser(string userId)
        {
            return _users.TryGetValue(userId, out var user) ? user : null;
        }

        public string GetRoomName()
        {
            return _roomName;
        }

        public int GetUserCount()
        {
            return _users.Count;
        }

        public List<ChatMessage> GetMessageHistory()
        {
            return _messageHistory.ToList();
        }

        public void DisplayRoomInfo()
        {
            Console.WriteLine($"\n=== {_roomName} Room Info ===");
            Console.WriteLine($"Online Users: {GetUserCount()}");
            Console.WriteLine($"Messages Sent: {_messageHistory.Count}");
            
            if (_users.Any())
            {
                Console.WriteLine("Current Participants:");
                foreach (var user in _users.Values)
                {
                    Console.WriteLine($"  • {user.UserName} ({user.GetStatus()})");
                }
            }
            
            Console.WriteLine(new string('=', 30));
        }

        private void BroadcastNotification(string notification)
        {
            foreach (var user in _users.Values)
            {
                user.ReceiveNotification(notification);
            }
        }
    }
}