using Singleton.Pattern;

namespace Singleton.Application
{
    /// <summary>
    /// Singleton manager demonstrating various singleton usage patterns
    /// Shows thread safety comparison and practical applications
    /// </summary>
    public class SingletonDemoManager
    {
        public void RunAllDemos()
        {
            Console.WriteLine("=== Singleton Pattern Comprehensive Demo ===\n");

            DemonstrateLazySingleton();
            Console.WriteLine(new string('-', 60));
            
            DemonstrateThreadSafeSingleton();
            Console.WriteLine(new string('-', 60));
            
            DemonstrateSimpleSingleton();
            Console.WriteLine(new string('-', 60));
            
            DemonstrateThreadSafetyComparison();
            Console.WriteLine(new string('-', 60));
            
            DemonstrateRealWorldUsage();
        }

        /// <summary>
        /// Demonstrates Lazy<T> based singleton (recommended approach)
        /// </summary>
        private static void DemonstrateLazySingleton()
        {
            Console.WriteLine("1. Lazy Singleton Demo (Recommended Approach)");
            Console.WriteLine("============================================");
            
            // Get instances multiple times
            var logger1 = LoggerSingleton.Instance;
            var logger2 = LoggerSingleton.Instance;
            var logger3 = LoggerSingleton.Instance;
            
            // Verify same instance
            Console.WriteLine($"All instances are same: {ReferenceEquals(logger1, logger2) && ReferenceEquals(logger2, logger3)}");
            Console.WriteLine($"Instance HashCodes: {logger1.GetInstanceHashCode()}, {logger2.GetInstanceHashCode()}, {logger3.GetInstanceHashCode()}");
            
            // Use the logger
            logger1.LogInfo("Application started");
            logger2.LogWarning("Low disk space detected");
            logger3.LogError("Database connection failed");
            
            logger1.DisplayStatistics();
        }

        /// <summary>
        /// Demonstrates traditional thread-safe singleton
        /// </summary>
        private static void DemonstrateThreadSafeSingleton()
        {
            Console.WriteLine("\n2. Thread-Safe Singleton Demo (Double-Checked Locking)");
            Console.WriteLine("===================================================");
            
            var db1 = DatabaseConnectionSingleton.Instance;
            var db2 = DatabaseConnectionSingleton.Instance;
            
            Console.WriteLine($"Same instance: {ReferenceEquals(db1, db2)}");
            Console.WriteLine($"Instance HashCodes: {db1.GetInstanceHashCode()}, {db2.GetInstanceHashCode()}");
            
            // Use database connection
            var result1 = db1.ExecuteQuery("SELECT * FROM Users");
            var result2 = db2.ExecuteQuery("UPDATE Users SET LastLogin = NOW()");
            
            db1.DisplayStatus();
        }

        /// <summary>
        /// Demonstrates simple singleton (not thread-safe)
        /// </summary>
        private static void DemonstrateSimpleSingleton()
        {
            Console.WriteLine("\n3. Simple Singleton Demo (Non-Thread-Safe)");
            Console.WriteLine("========================================");
            
            var config1 = ConfigurationManager.Instance;
            var config2 = ConfigurationManager.Instance;
            
            Console.WriteLine($"Same instance: {ReferenceEquals(config1, config2)}");
            Console.WriteLine($"Instance HashCodes: {config1.GetInstanceHashCode()}, {config2.GetInstanceHashCode()}");
            
            // Configure settings
            config1.SetSetting("DatabaseHost", "localhost");
            config2.SetSetting("Port", "5432");
            config1.SetSetting("DebugMode", "true");
            
            config1.DisplaySettings();
        }

        /// <summary>
        /// Demonstrates thread safety comparison
        /// </summary>
        private static void DemonstrateThreadSafetyComparison()
        {
            Console.WriteLine("\n4. Thread Safety Comparison Demo");
            Console.WriteLine("================================");
            
            Console.WriteLine("Simulating concurrent access to singletons...\n");
            
            // Test thread safety with multiple threads
            var tasks = new List<Task>();
            
            // Test Lazy singleton (thread-safe)
            for (int i = 0; i < 10; i++)
            {
                int taskId = i;
                tasks.Add(Task.Run(() =>
                {
                    var logger = LoggerSingleton.Instance;
                    logger.LogInfo($"Thread {taskId} accessed lazy singleton");
                }));
            }
            
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Lazy singleton handled concurrent access successfully");
            
            // Clear tasks for next test
            tasks.Clear();
            
            // Test traditional thread-safe singleton
            for (int i = 0; i < 10; i++)
            {
                int taskId = i;
                tasks.Add(Task.Run(() =>
                {
                    var db = DatabaseConnectionSingleton.Instance;
                    db.ExecuteQuery($"Concurrent query from thread {taskId}");
                }));
            }
            
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Traditional singleton handled concurrent access successfully");
        }

        /// <summary>
        /// Demonstrates real-world usage scenarios
        /// </summary>
        private static void DemonstrateRealWorldUsage()
        {
            Console.WriteLine("\n5. Real-World Usage Scenario");
            Console.WriteLine("===========================");
            
            Console.WriteLine("Simulating a complete application workflow...\n");
            
            // Application startup
            var logger = LoggerSingleton.Instance;
            var db = DatabaseConnectionSingleton.Instance;
            var config = ConfigurationManager.Instance;
            
            logger.LogInfo("Application starting up");
            
            // Configure application
            config.SetSetting("LogLevel", "Info");
            config.SetSetting("DatabaseTimeout", "60");
            
            // Connect to database
            var connectionResult = db.ExecuteQuery("CONNECT TO PRODUCTION_DATABASE");
            logger.LogInfo($"Database connection result: {connectionResult}");
            
            // Perform business operations
            var userQuery = db.ExecuteQuery("SELECT COUNT(*) FROM Users WHERE Active = 1");
            logger.LogInfo($"Active users count: {userQuery}");
            
            // Application shutdown
            logger.LogWarning("Application shutting down");
            db.CloseConnection();
            logger.LogError("Database connection closed");
            
            // Display final states
            logger.DisplayStatistics();
            db.DisplayStatus();
            config.DisplaySettings();
            
            Console.WriteLine("\nApplication workflow completed successfully!");
        }
    }
}