namespace Memento.Pattern
{
    /// <summary>
    /// Caretaker class
    /// Manages mementos without accessing their internal state
    /// </summary>
    public class Caretaker
    {
        private readonly List<IMemento> _mementos;
        private readonly string _originatorName;
        private readonly int _maxHistorySize;

        public Caretaker(string originatorName, int maxHistorySize = 100)
        {
            _mementos = new List<IMemento>();
            _originatorName = originatorName;
            _maxHistorySize = maxHistorySize;
            Console.WriteLine($"[Caretaker] Created for {_originatorName} with max history: {maxHistorySize}");
        }

        /// <summary>
        /// Adds a memento to history
        /// </summary>
        public void AddMemento(IMemento memento)
        {
            // Remove oldest memento if we exceed maximum size
            if (_mementos.Count >= _maxHistorySize)
            {
                var oldest = _mementos.First();
                _mementos.RemoveAt(0);
                Console.WriteLine($"[Caretaker] Removed oldest memento: {oldest.GetName()}");
            }

            _mementos.Add(memento);
            Console.WriteLine($"[Caretaker] Added memento: {memento.GetName()}");
        }

        /// <summary>
        /// Gets memento by index
        /// </summary>
        public IMemento GetMemento(int index)
        {
            if (index < 0 || index >= _mementos.Count)
            {
                throw new IndexOutOfRangeException($"Index {index} is out of range [0, {_mementos.Count - 1}]");
            }

            return _mementos[index];
        }

        /// <summary>
        /// Gets the most recent memento
        /// </summary>
        public IMemento GetLatestMemento()
        {
            if (!_mementos.Any())
            {
                throw new InvalidOperationException("No mementos available");
            }

            return _mementos.Last();
        }

        /// <summary>
        /// Gets memento by name
        /// </summary>
        public IMemento GetMementoByName(string name)
        {
            var memento = _mementos.FirstOrDefault(m => m.GetName() == name);
            if (memento == null)
            {
                throw new ArgumentException($"Memento with name '{name}' not found");
            }
            return memento;
        }

        /// <summary>
        /// Removes memento by index
        /// </summary>
        public void RemoveMemento(int index)
        {
            if (index < 0 || index >= _mementos.Count)
            {
                throw new IndexOutOfRangeException($"Index {index} is out of range");
            }

            var removed = _mementos[index];
            _mementos.RemoveAt(index);
            Console.WriteLine($"[Caretaker] Removed memento: {removed.GetName()}");
        }

        /// <summary>
        /// Clears all mementos
        /// </summary>
        public void ClearHistory()
        {
            var count = _mementos.Count;
            _mementos.Clear();
            Console.WriteLine($"[Caretaker] Cleared {count} mementos from history");
        }

        /// <summary>
        /// Gets memento count
        /// </summary>
        public int GetMementoCount()
        {
            return _mementos.Count;
        }

        /// <summary>
        /// Checks if history is empty
        /// </summary>
        public bool IsEmpty()
        {
            return !_mementos.Any();
        }

        /// <summary>
        /// Displays history information
        /// </summary>
        public void DisplayHistory()
        {
            Console.WriteLine($"\n=== {_originatorName} History ===");
            Console.WriteLine($"Total snapshots: {_mementos.Count}");
            Console.WriteLine($"Max capacity: {_maxHistorySize}");
            
            if (_mementos.Any())
            {
                Console.WriteLine("\nSnapshots (newest first):");
                for (int i = _mementos.Count - 1; i >= 0; i--)
                {
                    var memento = _mementos[i];
                    var age = DateTime.Now - memento.GetTimestamp();
                    Console.WriteLine($"  [{i}] {memento.GetName()}");
                    Console.WriteLine($"      Time: {memento.GetTimestamp():yyyy-MM-dd HH:mm:ss} ({FormatAge(age)} ago)");
                    Console.WriteLine($"      State: {memento.GetStateSummary()}");
                }
            }
            else
            {
                Console.WriteLine("  No snapshots available");
            }
            Console.WriteLine(new string('=', 40));
        }

        /// <summary>
        /// Gets mementos within a time range
        /// </summary>
        public List<IMemento> GetMementosByTimeRange(DateTime startTime, DateTime endTime)
        {
            return _mementos
                .Where(m => m.GetTimestamp() >= startTime && m.GetTimestamp() <= endTime)
                .ToList();
        }

        /// <summary>
        /// Gets mementos older than specified duration
        /// </summary>
        public List<IMemento> GetOldMementos(TimeSpan duration)
        {
            return _mementos
                .Where(m => (DateTime.Now - m.GetTimestamp()) > duration)
                .ToList();
        }

        /// <summary>
        /// Removes old mementos
        /// </summary>
        public int RemoveOldMementos(TimeSpan duration)
        {
            var oldMementos = GetOldMementos(duration);
            foreach (var memento in oldMementos)
            {
                _mementos.Remove(memento);
            }
            
            Console.WriteLine($"[Caretaker] Removed {oldMementos.Count} old mementos");
            return oldMementos.Count;
        }

        /// <summary>
        /// Gets chronological list of mementos
        /// </summary>
        public List<IMemento> GetChronologicalMementos()
        {
            return _mementos.OrderBy(m => m.GetTimestamp()).ToList();
        }

        private string FormatAge(TimeSpan age)
        {
            if (age.TotalDays >= 1)
                return $"{age.Days}d {age.Hours}h";
            if (age.TotalHours >= 1)
                return $"{age.Hours}h {age.Minutes}m";
            if (age.TotalMinutes >= 1)
                return $"{age.Minutes}m {age.Seconds}s";
            return $"{age.Seconds}s";
        }
    }
}