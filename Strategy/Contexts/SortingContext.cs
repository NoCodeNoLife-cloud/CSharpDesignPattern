using Strategy.Strategies;

namespace Strategy.Contexts
{
    /// <summary>
    /// Sorting context class
    /// Manages the current sorting strategy and provides sorting operations
    /// </summary>
    public class SortingContext<T> where T : IComparable<T>
    {
        private ISortingStrategy<T> _sortingStrategy;
        private readonly List<SortingOperationLog> _operationLogs = new List<SortingOperationLog>();

        public SortingContext(ISortingStrategy<T> initialStrategy)
        {
            _sortingStrategy = initialStrategy;
            Console.WriteLine($"[SortingContext] Initialized with {_sortingStrategy.GetName()}");
        }

        public ISortingStrategy<T> CurrentStrategy
        {
            get => _sortingStrategy;
            set
            {
                _sortingStrategy = value;
                Console.WriteLine($"[SortingContext] Strategy changed to {_sortingStrategy.GetName()}");
            }
        }

        public List<T> Sort(List<T> data)
        {
            if (data == null)
            {
                Console.WriteLine("[SortingContext] Warning: Null data provided");
                return new List<T>();
            }

            var startTime = DateTime.Now;
            Console.WriteLine($"\n[SortingContext] Starting sort with {_sortingStrategy.GetName()}");
            Console.WriteLine($"[SortingContext] Data size: {data.Count} elements");

            var sortedData = _sortingStrategy.Sort(new List<T>(data));

            var endTime = DateTime.Now;
            var duration = endTime - startTime;

            var logEntry = new SortingOperationLog
            {
                StrategyName = _sortingStrategy.GetName(),
                DataSize = data.Count,
                StartTime = startTime,
                EndTime = endTime,
                Duration = duration,
                TimeComplexity = _sortingStrategy.GetTimeComplexity()
            };

            _operationLogs.Add(logEntry);

            Console.WriteLine($"[SortingContext] Sort completed in {duration.TotalMilliseconds:F2} ms");
            Console.WriteLine($"[SortingContext] Time complexity: {_sortingStrategy.GetTimeComplexity()}");

            return sortedData;
        }

        public void SetStrategy(ISortingStrategy<T> strategy)
        {
            CurrentStrategy = strategy;
        }

        public List<SortingOperationLog> GetOperationLogs()
        {
            return _operationLogs.ToList();
        }

        public void DisplayPerformanceReport()
        {
            if (_operationLogs.Count == 0)
            {
                Console.WriteLine("[SortingContext] No sorting operations recorded");
                return;
            }

            Console.WriteLine("\n=== Sorting Performance Report ===");
            Console.WriteLine($"Total operations: {_operationLogs.Count}");

            foreach (var log in _operationLogs)
            {
                Console.WriteLine($"\nOperation: {log.StrategyName}");
                Console.WriteLine($"  Data size: {log.DataSize} elements");
                Console.WriteLine($"  Duration: {log.Duration.TotalMilliseconds:F2} ms");
                Console.WriteLine($"  Time complexity: {log.TimeComplexity}");
                Console.WriteLine($"  Timestamp: {log.StartTime:yyyy-MM-dd HH:mm:ss}");
            }

            Console.WriteLine("==================================\n");
        }

        public void ClearLogs()
        {
            _operationLogs.Clear();
            Console.WriteLine("[SortingContext] Operation logs cleared");
        }

        public bool IsSorted(List<T> data)
        {
            if (data == null || data.Count <= 1)
                return true;

            for (int i = 1; i < data.Count; i++)
            {
                if (data[i - 1].CompareTo(data[i]) > 0)
                    return false;
            }
            return true;
        }

        public string GetCurrentStrategyInfo()
        {
            return $"{_sortingStrategy.GetName()} - {_sortingStrategy.GetTimeComplexity()}";
        }
    }

    /// <summary>
    /// Sorting operation log entry
    /// </summary>
    public class SortingOperationLog
    {
        public string StrategyName { get; set; } = string.Empty;
        public int DataSize { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string TimeComplexity { get; set; } = string.Empty;
    }
}