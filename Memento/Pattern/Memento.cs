namespace Memento.Pattern
{
    /// <summary>
    /// Concrete memento implementation
    /// Stores the internal state of an originator object
    /// </summary>
    public class Memento : IMemento
    {
        private readonly object _state;
        private readonly DateTime _timestamp;
        private readonly string _name;

        public Memento(object state, string name = "")
        {
            _state = state ?? throw new ArgumentNullException(nameof(state));
            _timestamp = DateTime.Now;
            _name = string.IsNullOrEmpty(name) ? $"Snapshot_{_timestamp:yyyyMMdd_HHmmss}" : name;
        }

        public object GetState()
        {
            // Return a deep copy to prevent external modification
            return DeepCopy(_state);
        }

        public DateTime GetTimestamp()
        {
            return _timestamp;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetStateSummary()
        {
            return _state.ToString() ?? "Unknown State";
        }

        public override string ToString()
        {
            return $"Memento[{_name}] - {_timestamp:yyyy-MM-dd HH:mm:ss}";
        }

        /// <summary>
        /// Creates a deep copy of the state object
        /// </summary>
        private object DeepCopy(object obj)
        {
            if (obj == null)
                return null;

            // Handle primitive types and strings
            if (obj is string || obj.GetType().IsValueType)
            {
                return obj;
            }

            // Handle arrays
            if (obj is Array array)
            {
                var newArray = Array.CreateInstance(array.GetType().GetElementType(), array.Length);
                Array.Copy(array, newArray, array.Length);
                return newArray;
            }

            // For complex objects, we'll serialize/deserialize
            // In a real implementation, you might use a serialization library
            try
            {
                // Simple reflection-based copying for demonstration
                var type = obj.GetType();
                var newInstance = Activator.CreateInstance(type);
                
                foreach (var prop in type.GetProperties())
                {
                    if (prop.CanRead && prop.CanWrite)
                    {
                        var value = prop.GetValue(obj);
                        prop.SetValue(newInstance, value);
                    }
                }
                
                return newInstance;
            }
            catch
            {
                // If copying fails, return the original reference
                // Note: This is not ideal for production code
                return obj;
            }
        }

        /// <summary>
        /// Gets the age of this memento
        /// </summary>
        public TimeSpan GetAge()
        {
            return DateTime.Now - _timestamp;
        }

        /// <summary>
        /// Checks if this memento is older than specified duration
        /// </summary>
        public bool IsOlderThan(TimeSpan duration)
        {
            return GetAge() > duration;
        }
    }
}