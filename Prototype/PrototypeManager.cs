using Prototype.Models;

namespace Prototype
{
    /// <summary>
    /// Prototype Manager
    /// Responsible for storing and managing prototype objects
    /// </summary>
    public class PrototypeManager
    {
        private readonly Dictionary<string, Employee> _prototypes = new Dictionary<string, Employee>();

        /// <summary>
        /// Add prototype to manager
        /// </summary>
        public void AddPrototype(string key, Employee prototype)
        {
            _prototypes[key] = prototype;
        }

        /// <summary>
        /// Get shallow clone of prototype by key
        /// </summary>
        public Employee GetClone(string key)
        {
            return _prototypes.TryGetValue(key, out var prototype) ? prototype.Clone() : throw new ArgumentException($"Prototype with key '{key}' not found.");
        }

        /// <summary>
        /// Get deep clone of prototype by key
        /// </summary>
        public Employee GetDeepClone(string key)
        {
            return _prototypes.TryGetValue(key, out var value) ? value.DeepClone() : throw new ArgumentException($"Prototype with key '{key}' not found.");
        }

        /// <summary>
        /// Remove prototype
        /// </summary>
        public void RemovePrototype(string key)
        {
            _prototypes.Remove(key);
        }

        /// <summary>
        /// Get all prototype keys
        /// </summary>
        public IEnumerable<string> GetAllKeys()
        {
            return _prototypes.Keys;
        }
    }
}