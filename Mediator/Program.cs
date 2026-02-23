// See https://aka.ms/new-console-template for more information

using Mediator.Components;

namespace Mediator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Mediator Pattern Implementation Examples ===\n");

            try
            {
                // Demonstrate basic chat room
                DemonstrateBasicChatRoom();

                Console.WriteLine(new string('=', 70));

                // Demonstrate advanced chat room with groups
                DemonstrateAdvancedChatRoom();

                Console.WriteLine(new string('=', 70));

                // Demonstrate user status management
                DemonstrateUserStatusManagement();

                Console.WriteLine(new string('=', 70));

                // Demonstrate dynamic user management
                DemonstrateDynamicUserManagement();

                Console.WriteLine(new string('=', 70));

                // Demonstrate mediator pattern benefits
                DemonstratePatternBenefits();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            Console.WriteLine("\n=== Mediator Pattern Demo Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates basic chat room functionality
        /// </summary>
        private static void DemonstrateBasicChatRoom()
        {
            Console.WriteLine("1. Basic Chat Room Demo");
            Console.WriteLine("=======================");

            var chatRoom = new ChatRoomMediator("General Chat");

            // Create users
            var alice = new ChatUser("USR001", "Alice");
            var bob = new ChatUser("USR002", "Bob");
            var charlie = new ChatUser("USR003", "Charlie");

            // Users join chat room
            alice.JoinChatRoom(chatRoom);
            bob.JoinChatRoom(chatRoom);
            charlie.JoinChatRoom(chatRoom);

            // Display room info
            chatRoom.DisplayRoomInfo();

            // Private messaging
            Console.WriteLine("\nPrivate conversations:");
            alice.SendMessage("USR002", "Hi Bob, how are you?");
            bob.SendMessage("USR001", "Hello Alice! I'm doing great, thanks for asking.");

            // Broadcast messaging
            Console.WriteLine("\nBroadcast messages:");
            charlie.BroadcastMessage("Good morning everyone!");
            alice.BroadcastMessage("Has anyone seen the project documentation?");

            // User leaves
            bob.LeaveChatRoom();
            chatRoom.DisplayRoomInfo();
        }

        /// <summary>
        /// Demonstrates advanced chat room with group functionality
        /// </summary>
        private static void DemonstrateAdvancedChatRoom()
        {
            Console.WriteLine("\n2. Advanced Chat Room with Groups Demo");
            Console.WriteLine("=======================================");

            var advancedChat = new AdvancedChatRoomMediator("Team Collaboration");

            // Create users
            var developer1 = new ChatUser("DEV001", "John Developer");
            var developer2 = new ChatUser("DEV002", "Jane Developer");
            var designer = new ChatUser("DES001", "Mike Designer");
            var manager = new ChatUser("MGR001", "Sarah Manager");

            // Users join chat room
            developer1.JoinChatRoom(advancedChat);
            developer2.JoinChatRoom(advancedChat);
            designer.JoinChatRoom(advancedChat);
            manager.JoinChatRoom(advancedChat);

            // Create specialized groups
            advancedChat.CreateGroup("developers");
            advancedChat.CreateGroup("design");
            advancedChat.CreateGroup("management");

            // Add users to appropriate groups
            advancedChat.AddUserToGroup("DEV001", "developers");
            advancedChat.AddUserToGroup("DEV002", "developers");
            advancedChat.AddUserToGroup("DES001", "design");
            advancedChat.AddUserToGroup("MGR001", "management");
            advancedChat.AddUserToGroup("MGR001", "developers"); // Manager also in developers group

            // Group messaging
            Console.WriteLine("\nGroup communications:");
            developer1.SendMessage("DEV002", "Hey Jane, can you review my pull request?");
            advancedChat.SendGroupMessage("DEV001", "developers", "Code review meeting at 2 PM today");
            advancedChat.SendGroupMessage("DES001", "design", "New UI mockups are ready for review");
            advancedChat.SendGroupMessage("MGR001", "management", "Weekly status report due Friday");

            // Display statistics
            advancedChat.DisplayRoomStatistics();

            // Show group memberships
            Console.WriteLine("\nGroup Memberships:");
            foreach (var user in new[] { developer1, developer2, designer, manager })
            {
                var groups = advancedChat.GetUserGroups(user.UserId);
                Console.WriteLine($"  {user.UserName}: {string.Join(", ", groups)}");
            }
        }

        /// <summary>
        /// Demonstrates user status management
        /// </summary>
        private static void DemonstrateUserStatusManagement()
        {
            Console.WriteLine("\n3. User Status Management Demo");
            Console.WriteLine("===============================");

            var chatRoom = new ChatRoomMediator("Status Demo Room");

            var user1 = new ChatUser("USER001", "ActiveUser");
            var user2 = new ChatUser("USER002", "AwayUser");
            var user3 = new ChatUser("USER003", "BusyUser");

            user1.JoinChatRoom(chatRoom);
            user2.JoinChatRoom(chatRoom);
            user3.JoinChatRoom(chatRoom);

            Console.WriteLine("\nInitial status notifications:");
            user1.BroadcastMessage("Hello everyone!");

            Console.WriteLine("\nStatus changes:");
            user2.SetAway("In meeting");
            user3.SetBusy("Deep work session");

            Console.WriteLine("\nStatus-based messaging:");
            user1.SendMessage("USER002", "Quick question about the project");
            user1.SendMessage("USER003", "Urgent bug fix needed");

            // Display current statuses
            chatRoom.DisplayRoomInfo();
        }

        /// <summary>
        /// Demonstrates dynamic user management
        /// </summary>
        private static void DemonstrateDynamicUserManagement()
        {
            Console.WriteLine("\n4. Dynamic User Management Demo");
            Console.WriteLine("================================");

            var chatRoom = new AdvancedChatRoomMediator("Dynamic Room");

            // Create initial users
            var users = new List<ChatUser>
            {
                new ChatUser("TEMP001", "TemporaryUser1"),
                new ChatUser("TEMP002", "TemporaryUser2"),
                new ChatUser("TEMP003", "TemporaryUser3")
            };

            // All users join
            foreach (var user in users)
            {
                user.JoinChatRoom(chatRoom);
            }

            chatRoom.DisplayRoomStatistics();

            // Simulate dynamic behavior
            Console.WriteLine("\nDynamic user activity:");
            users[0].BroadcastMessage("Starting work on feature X");
            users[1].SendMessage("TEMP003", "Need help with the database schema");

            // User leaves and rejoins
            Console.WriteLine("\nUser cycling:");
            var leavingUser = users[1];
            leavingUser.LeaveChatRoom();
            chatRoom.DisplayRoomStatistics();

            // Same user rejoins with different ID
            var returningUser = new ChatUser("TEMP004", "TemporaryUser2-Back");
            returningUser.JoinChatRoom(chatRoom);
            returningUser.BroadcastMessage("I'm back!");

            chatRoom.DisplayRoomStatistics();

            // Clean up
            foreach (var user in users)
            {
                user.LeaveChatRoom();
            }

            returningUser.LeaveChatRoom();
        }

        /// <summary>
        /// Demonstrates mediator pattern benefits and concepts
        /// </summary>
        private static void DemonstratePatternBenefits()
        {
            Console.WriteLine("\n5. Mediator Pattern Benefits Demo");
            Console.WriteLine("==================================");

            Console.WriteLine("Mediator Pattern Key Benefits:");
            Console.WriteLine("• Reduces coupling between colleagues");
            Console.WriteLine("• Centralizes complex communication logic");
            Console.WriteLine("• Makes object protocols easier to understand");
            Console.WriteLine("• Allows easy addition of new colleagues");
            Console.WriteLine("• Simplifies maintenance and testing");

            Console.WriteLine("\nCommon Use Cases:");
            Console.WriteLine("• Chat applications");
            Console.WriteLine("• GUI dialog boxes");
            Console.WriteLine("• Air traffic control systems");
            Console.WriteLine("• Workflow engines");
            Console.WriteLine("• Event management systems");
            Console.WriteLine("• Distributed systems coordination");

            Console.WriteLine("\nWhen to Use Mediator Pattern:");
            Console.WriteLine("• Objects communicate in well-defined but complex ways");
            Console.WriteLine("• Reusing an object is difficult because it refers to and communicates with many other objects");
            Console.WriteLine("• Behavior distributed between several classes should be customizable without subclassing");
            Console.WriteLine("• Want to eliminate tight coupling between components");

            Console.WriteLine("\nChat System Features Demonstrated:");
            Console.WriteLine("• One-to-one private messaging");
            Console.WriteLine("• One-to-many broadcast messaging");
            Console.WriteLine("• Group-based communication");
            Console.WriteLine("• User presence and status management");
            Console.WriteLine("• Message history and logging");
            Console.WriteLine("• Dynamic user joining/leaving");
        }
    }
}