namespace Singleton.Pattern
{
    /// <summary>
    /// Thread-safe singleton implementation using lazy initialization
    /// Most recommended approach for .NET applications
    /// </summary>
    public sealed class LoggerSingleton
    {
        // Private static readonly instance using Lazy<T>
        private static readonly Lazy<LoggerSingleton> _instance = 
            new Lazy<LoggerSingleton>(() => new LoggerSingleton());

        // Private constructor prevents direct instantiation
        private LoggerSingleton()
        {
            // Initialize logger configuration
            InitializeLogger();
        }

        /// <summary>
        /// Gets the singleton instance
        /// </summary>
        public static LoggerSingleton Instance => _instance.Value;

        // Instance fields
        private readonly List<string> _logEntries = new List<string>();
        private readonly object _lockObject = new object();
        public string LoggerName { get; private set; } = "DefaultLogger";
        public DateTime CreationTime { get; private set; }
        public int LogCount => _logEntries.Count;

        /// <summary>
        /// Initializes logger configuration
        /// </summary>
        private void InitializeLogger()
        {
            LoggerName = $"Logger_{Guid.NewGuid().ToString()[..8]}";
            CreationTime = DateTime.Now;
            LogInfo("Logger initialized successfully");
        }

        /// <summary>
        /// Logs information message
        /// </summary>
        public void LogInfo(string message)
        {
            LogMessage("INFO", message);
        }

        /// <summary>
        /// Logs warning message
        /// </summary>
        public void LogWarning(string message)
        {
            LogMessage("WARNING", message);
        }

        /// <summary>
        /// Logs error message
        /// </summary>
        public void LogError(string message)
        {
            LogMessage("ERROR", message);
        }

        /// <summary>
        /// Generic logging method
        /// </summary>
        private void LogMessage(string level, string message)
        {
            lock (_lockObject)
            {
                var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                _logEntries.Add(logEntry);
                
                // Also output to console
                Console.WriteLine(logEntry);
            }
        }

        /// <summary>
        /// Gets all log entries
        /// </summary>
        public IReadOnlyList<string> GetLogEntries()
        {
            lock (_lockObject)
            {
                return _logEntries.ToList().AsReadOnly();
            }
        }

        /// <summary>
        /// Clears all log entries
        /// </summary>
        public void ClearLogs()
        {
            lock (_lockObject)
            {
                _logEntries.Clear();
                LogInfo("Log entries cleared");
            }
        }

        /// <summary>
        /// Displays logger statistics
        /// </summary>
        public void DisplayStatistics()
        {
            Console.WriteLine($"\n=== Logger Statistics ===");
            Console.WriteLine($"Logger Name: {LoggerName}");
            Console.WriteLine($"Creation Time: {CreationTime:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Total Logs: {LogCount}");
            Console.WriteLine($"Instance HashCode: {GetInstanceHashCode()}");
            Console.WriteLine("========================\n");
        }

        /// <summary>
        /// Gets instance hash code for verification
        /// </summary>
        public int GetInstanceHashCode() => GetHashCode();
    }
}