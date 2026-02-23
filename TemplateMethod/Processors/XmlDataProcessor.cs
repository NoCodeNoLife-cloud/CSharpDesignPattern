namespace TemplateMethod.Processors
{
    /// <summary>
    /// XML data processor implementation
    /// Processes XML configuration data using template method pattern
    /// </summary>
    public class XmlDataProcessor : Framework.DataProcessor
    {
        private readonly string _configPath;
        private readonly List<XmlConfiguration> _configs = new List<XmlConfiguration>();

        public XmlDataProcessor(string configPath) : base("XML Processor")
        {
            _configPath = configPath;
        }

        protected override object LoadData()
        {
            Console.WriteLine($"[XML Processor] Loading configuration from: {_configPath}");
            
            // Simulate XML configuration data
            var xmlData = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    ["name"] = "DatabaseConnection",
                    ["type"] = "connection",
                    ["value"] = "Server=localhost;Database=MyApp;User=admin;",
                    ["enabled"] = "true",
                    ["priority"] = "1"
                },
                new Dictionary<string, string>
                {
                    ["name"] = "LoggingLevel",
                    ["type"] = "setting",
                    ["value"] = "Information",
                    ["enabled"] = "true",
                    ["priority"] = "2"
                },
                new Dictionary<string, string>
                {
                    ["name"] = "CacheTimeout",
                    ["type"] = "setting",
                    ["value"] = "3600",
                    ["enabled"] = "true",
                    ["priority"] = "1"
                },
                new Dictionary<string, string>
                {
                    ["name"] = "EmailService",
                    ["type"] = "service",
                    ["value"] = "smtp.gmail.com:587",
                    ["enabled"] = "false",
                    ["priority"] = "3"
                }
            };
            
            Console.WriteLine($"[XML Processor] Loaded {xmlData.Count} configuration items");
            return xmlData;
        }

        protected override bool ValidateData(object data)
        {
            Console.WriteLine("[XML Processor] Validating XML configuration structure");
            
            if (data is not List<Dictionary<string, string>> xmlData)
                return false;
            
            // Validate required attributes exist
            var requiredAttributes = new[] { "name", "type", "value", "enabled", "priority" };
            
            foreach (var item in xmlData)
            {
                foreach (var attr in requiredAttributes)
                {
                    if (!item.ContainsKey(attr))
                    {
                        Console.WriteLine($"[XML Processor] Missing required attribute: {attr}");
                        return false;
                    }
                }
                
                // Validate enabled is boolean
                if (item["enabled"] != "true" && item["enabled"] != "false")
                {
                    Console.WriteLine($"[XML Processor] Invalid enabled value: {item["enabled"]}");
                    return false;
                }
                
                // Validate priority is numeric
                if (!int.TryParse(item["priority"], out _))
                {
                    Console.WriteLine($"[XML Processor] Invalid priority value: {item["priority"]}");
                    return false;
                }
            }
            
            Console.WriteLine($"[XML Processor] XML validation passed ({xmlData.Count} items)");
            return true;
        }

        protected override object TransformData(object rawData)
        {
            Console.WriteLine("[XML Processor] Transforming XML configuration to typed objects");
            
            var xmlData = (List<Dictionary<string, string>>)rawData;
            _configs.Clear();
            
            foreach (var item in xmlData)
            {
                var config = new XmlConfiguration
                {
                    Name = item["name"],
                    Type = item["type"],
                    Value = item["value"],
                    Enabled = item["enabled"] == "true",
                    Priority = int.Parse(item["priority"])
                };
                _configs.Add(config);
            }
            
            Console.WriteLine($"[XML Processor] Transformed {_configs.Count} configuration items");
            return _configs;
        }

        protected override object ProcessTransformedData(object transformedData)
        {
            Console.WriteLine("[XML Processor] Processing configuration validation and optimization");
            
            var configs = (List<XmlConfiguration>)transformedData;
            
            // Validate configuration values
            var validationResults = new List<ConfigValidationResult>();
            
            foreach (var config in configs)
            {
                var result = new ConfigValidationResult
                {
                    Name = config.Name,
                    IsValid = true,
                    Messages = new List<string>()
                };
                
                // Specific validation rules
                switch (config.Type)
                {
                    case "connection":
                        if (!config.Value.Contains("Server=") || !config.Value.Contains("Database="))
                        {
                            result.IsValid = false;
                            result.Messages.Add("Connection string missing required components");
                        }
                        break;
                        
                    case "setting":
                        if (config.Name == "LoggingLevel" && 
                            !new[] { "Trace", "Debug", "Information", "Warning", "Error", "Critical" }.Contains(config.Value))
                        {
                            result.IsValid = false;
                            result.Messages.Add("Invalid logging level");
                        }
                        else if (config.Name == "CacheTimeout")
                        {
                            if (int.TryParse(config.Value, out int timeout))
                            {
                                if (timeout < 60 || timeout > 86400)
                                {
                                    result.IsValid = false;
                                    result.Messages.Add("Cache timeout out of acceptable range (60-86400 seconds)");
                                }
                            }
                            else
                            {
                                result.IsValid = false;
                                result.Messages.Add("Cache timeout must be numeric");
                            }
                        }
                        break;
                }
                
                validationResults.Add(result);
            }
            
            // Apply optimizations
            var enabledConfigs = configs.Where(c => c.Enabled).ToList();
            var sortedConfigs = enabledConfigs.OrderBy(c => c.Priority).ThenBy(c => c.Name).ToList();
            
            Console.WriteLine("[XML Processor] Configuration Analysis:");
            Console.WriteLine($"  Total Configurations: {configs.Count}");
            Console.WriteLine($"  Enabled Configurations: {enabledConfigs.Count}");
            Console.WriteLine($"  Disabled Configurations: {configs.Count - enabledConfigs.Count}");
            Console.WriteLine($"  Invalid Configurations: {validationResults.Count(v => !v.IsValid)}");
            
            return new { ProcessedConfigs = sortedConfigs, ValidationResults = validationResults };
        }

        protected override void SaveResults(object processedData)
        {
            Console.WriteLine("[XML Processor] Saving validated configuration results");
            
            var result = (dynamic)processedData;
            var configs = (List<XmlConfiguration>)result.ProcessedConfigs;
            var validations = (List<ConfigValidationResult>)result.ValidationResults;
            
            // Simulate saving to configuration store
            Console.WriteLine($"[XML Processor] Configuration Summary:");
            Console.WriteLine($"  Valid Configurations: {configs.Count}");
            
            foreach (var config in configs)
            {
                Console.WriteLine($"    {config.Name} ({config.Type}): {config.Value}");
            }
            
            // Report validation issues
            var invalidConfigs = validations.Where(v => !v.IsValid).ToList();
            if (invalidConfigs.Any())
            {
                Console.WriteLine($"\n  Validation Issues ({invalidConfigs.Count}):");
                foreach (var validation in invalidConfigs)
                {
                    Console.WriteLine($"    {validation.Name}: {string.Join(", ", validation.Messages)}");
                }
            }
        }

        protected override void GenerateReport()
        {
            Console.WriteLine("[XML Processor] Generating configuration audit report");
            
            var configTypes = _configs.GroupBy(c => c.Type)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToList();
            
            var enabledCount = _configs.Count(c => c.Enabled);
            var disabledCount = _configs.Count(c => !c.Enabled);
            
            Console.WriteLine($"[XML Processor] Configuration Report:");
            Console.WriteLine($"  Total Items: {_configs.Count}");
            Console.WriteLine($"  Enabled: {enabledCount}");
            Console.WriteLine($"  Disabled: {disabledCount}");
            Console.WriteLine($"  By Type:");
            
            foreach (var typeGroup in configTypes)
            {
                Console.WriteLine($"    {typeGroup.Type}: {typeGroup.Count} items");
            }
            
            // Priority distribution
            var priorityGroups = _configs.GroupBy(c => c.Priority)
                .OrderBy(g => g.Key)
                .Select(g => new { Priority = g.Key, Count = g.Count() });
            
            Console.WriteLine($"  Priority Distribution:");
            foreach (var priorityGroup in priorityGroups)
            {
                Console.WriteLine($"    Priority {priorityGroup.Priority}: {priorityGroup.Count} items");
            }
        }

        protected override void HandleError(Exception ex)
        {
            Console.WriteLine($"[XML Processor] XML processing error: {ex.Message}");
            // Log to error tracking system
        }

        protected override void HandleValidationError()
        {
            Console.WriteLine("[XML Processor] XML configuration validation failed - check schema compliance");
        }

        // XML Configuration data structure
        private class XmlConfiguration
        {
            public string Name { get; set; } = string.Empty;
            public string Type { get; set; } = string.Empty;
            public string Value { get; set; } = string.Empty;
            public bool Enabled { get; set; }
            public int Priority { get; set; }
        }

        // Configuration validation result
        private class ConfigValidationResult
        {
            public string Name { get; set; } = string.Empty;
            public bool IsValid { get; set; }
            public List<string> Messages { get; set; } = new List<string>();
        }
    }
}