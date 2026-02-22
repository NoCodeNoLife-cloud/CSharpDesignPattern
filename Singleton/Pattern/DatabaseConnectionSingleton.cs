namespace Singleton.Pattern
{
    /// <summary>
    /// Traditional thread-safe singleton implementation
    /// Uses double-checked locking pattern
    /// </summary>
    public sealed class DatabaseConnectionSingleton
    {
        // Volatile keyword ensures thread visibility
        private static volatile DatabaseConnectionSingleton _instance;
        private static readonly object _lockObject = new object();

        // Private constructor
        private DatabaseConnectionSingleton()
        {
            ConnectionString = "Server=localhost;Database=MyApp;Trusted_Connection=true;";
            InitializeConnection();
        }

        /// <summary>
        /// Gets the singleton instance using double-checked locking
        /// </summary>
        public static DatabaseConnectionSingleton Instance
        {
            get
            {
                // First check (without locking for performance)
                if (_instance == null)
                {
                    // Lock for thread safety
                    lock (_lockObject)
                    {
                        // Second check (with locking)
                        if (_instance == null)
                        {
                            _instance = new DatabaseConnectionSingleton();
                        }
                    }
                }
                return _instance;
            }
        }

        // Properties
        public string ConnectionString { get; private set; }
        public bool IsConnected { get; private set; }
        public DateTime LastAccessTime { get; private set; }
        public int QueryCount { get; private set; }

        /// <summary>
        /// Initializes database connection
        /// </summary>
        private void InitializeConnection()
        {
            Console.WriteLine("Initializing database connection...");
            // Simulate connection establishment
            Thread.Sleep(100); // Simulate network delay
            IsConnected = true;
            LastAccessTime = DateTime.Now;
            Console.WriteLine("Database connection established successfully");
        }

        /// <summary>
        /// Executes a database query
        /// </summary>
        public string ExecuteQuery(string query)
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("Database not connected");
            }

            LastAccessTime = DateTime.Now;
            QueryCount++;
            
            // Simulate query execution
            var result = $"Query Result #{QueryCount}: {query} - Executed at {DateTime.Now:HH:mm:ss}";
            Console.WriteLine($"Executing: {query}");
            Thread.Sleep(50); // Simulate query processing time
            
            return result;
        }

        /// <summary>
        /// Closes database connection
        /// </summary>
        public void CloseConnection()
        {
            if (IsConnected)
            {
                IsConnected = false;
                Console.WriteLine("Database connection closed");
            }
        }

        /// <summary>
        /// Reconnects to database
        /// </summary>
        public void Reconnect()
        {
            CloseConnection();
            InitializeConnection();
        }

        /// <summary>
        /// Displays connection status
        /// </summary>
        public void DisplayStatus()
        {
            Console.WriteLine($"\n=== Database Connection Status ===");
            Console.WriteLine($"Connected: {IsConnected}");
            Console.WriteLine($"Connection String: {ConnectionString}");
            Console.WriteLine($"Last Access: {LastAccessTime:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Queries Executed: {QueryCount}");
            Console.WriteLine($"Instance HashCode: {GetInstanceHashCode()}");
            Console.WriteLine("==================================\n");
        }

        /// <summary>
        /// Gets instance hash code for verification
        /// </summary>
        public int GetInstanceHashCode() => GetHashCode();
    }
}