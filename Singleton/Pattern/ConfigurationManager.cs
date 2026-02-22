namespace Singleton.Pattern
{
    /// <summary>
    /// Simple singleton implementation (not thread-safe)
    /// Suitable for single-threaded scenarios or when performance is critical
    /// </summary>
    public sealed class ConfigurationManager
    {
        // Static instance
        private static ConfigurationManager _instance;

        // Private constructor
        private ConfigurationManager()
        {
            Settings = new Dictionary<string, string>();
            LoadDefaultSettings();
        }

        /// <summary>
        /// Gets the singleton instance (not thread-safe)
        /// </summary>
        public static ConfigurationManager Instance
        {
            get
            {
                // Not thread-safe - multiple instances could be created
                if (_instance == null)
                {
                    _instance = new ConfigurationManager();
                }
                return _instance;
            }
        }

        // Properties
        public Dictionary<string, string> Settings { get; private set; }
        public DateTime LastModified { get; private set; }
        public int ModificationCount { get; private set; }

        /// <summary>
        /// Loads default configuration settings
        /// </summary>
        private void LoadDefaultSettings()
        {
            Settings["AppName"] = "MyApplication";
            Settings["Version"] = "1.0.0";
            Settings["Environment"] = "Development";
            Settings["MaxRetries"] = "3";
            Settings["Timeout"] = "30";
            
            LastModified = DateTime.Now;
            Console.WriteLine("Default configuration loaded");
        }

        /// <summary>
        /// Gets setting value by key
        /// </summary>
        public string GetSetting(string key)
        {
            return Settings.TryGetValue(key, out var value) ? value : string.Empty;
        }

        /// <summary>
        /// Sets configuration setting
        /// </summary>
        public void SetSetting(string key, string value)
        {
            Settings[key] = value;
            LastModified = DateTime.Now;
            ModificationCount++;
            Console.WriteLine($"Setting '{key}' updated to '{value}'");
        }

        /// <summary>
        /// Removes setting by key
        /// </summary>
        public void RemoveSetting(string key)
        {
            if (Settings.Remove(key))
            {
                LastModified = DateTime.Now;
                ModificationCount++;
                Console.WriteLine($"Setting '{key}' removed");
            }
        }

        /// <summary>
        /// Checks if setting exists
        /// </summary>
        public bool ContainsSetting(string key)
        {
            return Settings.ContainsKey(key);
        }

        /// <summary>
        /// Gets all settings as key-value pairs
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> GetAllSettings()
        {
            return Settings.ToList();
        }

        /// <summary>
        /// Displays all configuration settings
        /// </summary>
        public void DisplaySettings()
        {
            Console.WriteLine($"\n=== Configuration Settings ===");
            Console.WriteLine($"Last Modified: {LastModified:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Modifications: {ModificationCount}");
            Console.WriteLine($"Total Settings: {Settings.Count}");
            Console.WriteLine($"Instance HashCode: {GetInstanceHashCode()}");
            Console.WriteLine("\nCurrent Settings:");
            
            foreach (var setting in Settings.OrderBy(s => s.Key))
            {
                Console.WriteLine($"  {setting.Key}: {setting.Value}");
            }
            Console.WriteLine("=============================\n");
        }

        /// <summary>
        /// Resets to default settings
        /// </summary>
        public void ResetToDefaults()
        {
            Settings.Clear();
            LoadDefaultSettings();
            ModificationCount++;
            Console.WriteLine("Configuration reset to defaults");
        }

        /// <summary>
        /// Gets instance hash code for verification
        /// </summary>
        public int GetInstanceHashCode() => GetHashCode();
    }
}