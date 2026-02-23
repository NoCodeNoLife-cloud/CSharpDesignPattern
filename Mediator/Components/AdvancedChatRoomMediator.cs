using Mediator.Models;

namespace Mediator.Components
{
    /// <summary>
    /// Advanced chat room mediator with additional features
    /// Extends basic chat room functionality
    /// </summary>
    public class AdvancedChatRoomMediator : IChatRoomMediator
    {
        private readonly Dictionary<string, IUser> _users = new Dictionary<string, IUser>();
        private readonly List<ChatMessage> _messageHistory = new List<ChatMessage>();
        private readonly Dictionary<string, List<string>> _userGroups = new Dictionary<string, List<string>>();
        private readonly string _roomName;
        private readonly int _maxMessageHistory;

        public AdvancedChatRoomMediator(string roomName, int maxMessageHistory = 1000)
        {
            _roomName = roomName;
            _maxMessageHistory = maxMessageHistory;
            Console.WriteLine($"[AdvancedChatRoom] {_roomName} created with max history: {maxMessageHistory}");
        }

        public void RegisterUser(IUser user)
        {
            if (!_users.ContainsKey(user.UserId))
            {
                _users[user.UserId] = user;
                user.Mediator = this;
                
                // Add to default group
                AddUserToGroup(user.UserId, "general");
                
                // Welcome message
                SendSystemMessage($"Welcome {user.UserName} to {_roomName}!");
                BroadcastNotification($"{user.UserName} joined the chat room");
                
                Console.WriteLine($"[AdvancedChatRoom] User {user.UserName} ({user.UserId}) registered");
            }
        }

        public void SendMessage(string fromUserId, string toUserId, string message)
        {
            if (!_users.ContainsKey(fromUserId))
            {
                Console.WriteLine($"[AdvancedChatRoom] Sender {fromUserId} not found");
                return;
            }

            if (!_users.ContainsKey(toUserId))
            {
                Console.WriteLine($"[AdvancedChatRoom] Receiver {toUserId} not found");
                _users[fromUserId].ReceiveNotification($"User {toUserId} is not online");
                return;
            }

            var chatMessage = new ChatMessage
            {
                FromUserId = fromUserId,
                ToUserId = toUserId,
                Message = message,
                Timestamp = DateTime.Now,
                MessageType = MessageType.Private
            };
            
            AddMessageToHistory(chatMessage);
            
            var receiver = _users[toUserId];
            receiver.ReceiveMessage(fromUserId, message);
            
            Console.WriteLine($"[AdvancedChatRoom] Private message delivered: {fromUserId} → {toUserId}");
        }

        public void BroadcastMessage(string fromUserId, string message)
        {
            if (!_users.ContainsKey(fromUserId))
            {
                Console.WriteLine($"[AdvancedChatRoom] Broadcaster {fromUserId} not found");
                return;
            }

            var chatMessage = new ChatMessage
            {
                FromUserId = fromUserId,
                Message = message,
                Timestamp = DateTime.Now,
                MessageType = MessageType.Broadcast
            };
            
            AddMessageToHistory(chatMessage);
            
            // Send to all users except sender
            foreach (var user in _users.Values.Where(u => u.UserId != fromUserId))
            {
                user.ReceiveMessage(fromUserId, message);
            }
            
            Console.WriteLine($"[AdvancedChatRoom] Broadcast message sent by {fromUserId}");
        }

        public void UnregisterUser(string userId)
        {
            if (_users.TryGetValue(userId, out var user))
            {
                // Remove from all groups
                foreach (var group in _userGroups.Keys.ToList())
                {
                    _userGroups[group].Remove(userId);
                    if (!_userGroups[group].Any())
                    {
                        _userGroups.Remove(group);
                    }
                }
                
                _users.Remove(userId);
                user.Mediator = null;
                
                SendSystemMessage($"{user.UserName} has left the chat room");
                BroadcastNotification($"{user.UserName} left the chat room");
                
                Console.WriteLine($"[AdvancedChatRoom] User {user.UserName} ({userId}) unregistered");
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

        // Advanced features
        public void CreateGroup(string groupName)
        {
            if (!_userGroups.ContainsKey(groupName))
            {
                _userGroups[groupName] = new List<string>();
                SendSystemMessage($"Group '{groupName}' created");
                Console.WriteLine($"[AdvancedChatRoom] Group '{groupName}' created");
            }
        }

        public void AddUserToGroup(string userId, string groupName)
        {
            if (!_userGroups.ContainsKey(groupName))
            {
                CreateGroup(groupName);
            }
            
            if (!_userGroups[groupName].Contains(userId))
            {
                _userGroups[groupName].Add(userId);
                Console.WriteLine($"[AdvancedChatRoom] User {userId} added to group '{groupName}'");
            }
        }

        public void RemoveUserFromGroup(string userId, string groupName)
        {
            if (_userGroups.ContainsKey(groupName))
            {
                _userGroups[groupName].Remove(userId);
                if (!_userGroups[groupName].Any())
                {
                    _userGroups.Remove(groupName);
                }
                Console.WriteLine($"[AdvancedChatRoom] User {userId} removed from group '{groupName}'");
            }
        }

        public void SendGroupMessage(string fromUserId, string groupName, string message)
        {
            if (!_userGroups.ContainsKey(groupName))
            {
                Console.WriteLine($"[AdvancedChatRoom] Group '{groupName}' not found");
                return;
            }

            if (!_users.ContainsKey(fromUserId))
            {
                Console.WriteLine($"[AdvancedChatRoom] Sender {fromUserId} not found");
                return;
            }

            var groupMembers = _userGroups[groupName];
            var chatMessage = new ChatMessage
            {
                FromUserId = fromUserId,
                Message = $"[Group:{groupName}] {message}",
                Timestamp = DateTime.Now,
                MessageType = MessageType.Broadcast
            };
            
            AddMessageToHistory(chatMessage);
            
            foreach (var memberId in groupMembers.Where(id => id != fromUserId))
            {
                if (_users.TryGetValue(memberId, out var user))
                {
                    user.ReceiveMessage(fromUserId, chatMessage.Message);
                }
            }
            
            Console.WriteLine($"[AdvancedChatRoom] Group message sent to '{groupName}' by {fromUserId}");
        }

        public List<string> GetGroupMembers(string groupName)
        {
            return _userGroups.ContainsKey(groupName) 
                ? _userGroups[groupName].Select(id => _users[id]?.UserName ?? id).ToList()
                : new List<string>();
        }

        public List<string> GetUserGroups(string userId)
        {
            return _userGroups
                .Where(kvp => kvp.Value.Contains(userId))
                .Select(kvp => kvp.Key)
                .ToList();
        }

        public void SendSystemMessage(string message)
        {
            var systemMessage = new ChatMessage
            {
                FromUserId = "SYSTEM",
                Message = message,
                Timestamp = DateTime.Now,
                MessageType = MessageType.System,
                Priority = MessagePriority.High
            };
            
            AddMessageToHistory(systemMessage);
            
            foreach (var user in _users.Values)
            {
                user.ReceiveNotification(message);
            }
            
            Console.WriteLine($"[AdvancedChatRoom] System message: {message}");
        }

        public List<ChatMessage> GetMessageHistory(int count = 50)
        {
            return _messageHistory
                .OrderByDescending(m => m.Timestamp)
                .Take(count)
                .Reverse()
                .ToList();
        }

        public void DisplayRoomStatistics()
        {
            Console.WriteLine($"\n=== {_roomName} Statistics ===");
            Console.WriteLine($"Total Users: {_users.Count}");
            Console.WriteLine($"Active Groups: {_userGroups.Count}");
            Console.WriteLine($"Message History: {_messageHistory.Count} messages");
            
            if (_userGroups.Any())
            {
                Console.WriteLine("\nGroups:");
                foreach (var group in _userGroups)
                {
                    Console.WriteLine($"  {group.Key}: {group.Value.Count} members");
                }
            }
            
            Console.WriteLine(new string('=', 35));
        }

        private void AddMessageToHistory(ChatMessage message)
        {
            _messageHistory.Add(message);
            
            // Maintain history size
            if (_messageHistory.Count > _maxMessageHistory)
            {
                _messageHistory.RemoveAt(0);
            }
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