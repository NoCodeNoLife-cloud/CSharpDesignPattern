using Observer.Models;

namespace Observer.Observers
{
    /// <summary>
    /// Alert display implementation
    /// Monitors for extreme weather conditions and triggers alerts
    /// </summary>
    public class AlertDisplay : IWeatherObserver
    {
        private WeatherData _currentWeather = new WeatherData();
        private readonly string _displayName;
        private readonly List<AlertRule> _alertRules;
        private readonly List<WeatherAlert> _activeAlerts = new List<WeatherAlert>();

        public AlertDisplay(string displayName)
        {
            _displayName = displayName;
            _alertRules = InitializeAlertRules();
            Console.WriteLine($"[{_displayName}] Alert system initialized with {_alertRules.Count} rules");
        }

        public void Update(WeatherData weatherData)
        {
            _currentWeather = weatherData;
            CheckForAlerts();
            DisplayActiveAlerts();
        }

        private List<AlertRule> InitializeAlertRules()
        {
            return new List<AlertRule>
            {
                new AlertRule
                {
                    Name = "Extreme Heat Warning",
                    Condition = w => w.Temperature > 35,
                    Severity = AlertSeverity.High,
                    Message = "Extreme heat conditions detected! Temperature exceeds 35°C"
                },
                new AlertRule
                {
                    Name = "Freeze Warning",
                    Condition = w => w.Temperature < 0,
                    Severity = AlertSeverity.High,
                    Message = "Freezing conditions detected! Temperature below 0°C"
                },
                new AlertRule
                {
                    Name = "High Humidity Alert",
                    Condition = w => w.Humidity > 90,
                    Severity = AlertSeverity.Medium,
                    Message = "High humidity levels detected! Humidity exceeds 90%"
                },
                new AlertRule
                {
                    Name = "Low Pressure Warning",
                    Condition = w => w.Pressure < 980,
                    Severity = AlertSeverity.Medium,
                    Message = "Low atmospheric pressure detected! Possible storm approach"
                },
                new AlertRule
                {
                    Name = "Storm Alert",
                    Condition = w => w.Condition == WeatherCondition.Stormy,
                    Severity = AlertSeverity.High,
                    Message = "Severe storm conditions detected!"
                },
                new AlertRule
                {
                    Name = "Fog Advisory",
                    Condition = w => w.Condition == WeatherCondition.Foggy,
                    Severity = AlertSeverity.Medium,
                    Message = "Foggy conditions detected! Reduced visibility expected"
                }
            };
        }

        private void CheckForAlerts()
        {
            var newAlerts = new List<WeatherAlert>();

            foreach (var rule in _alertRules)
            {
                if (rule.Condition(_currentWeather))
                {
                    var existingAlert = _activeAlerts.FirstOrDefault(a => a.RuleName == rule.Name);
                    if (existingAlert == null)
                    {
                        // New alert
                        var alert = new WeatherAlert
                        {
                            RuleName = rule.Name,
                            Severity = rule.Severity,
                            Message = rule.Message,
                            TriggeredAt = DateTime.Now,
                            WeatherData = _currentWeather
                        };
                        newAlerts.Add(alert);
                    }
                    else
                    {
                        // Update existing alert timestamp
                        existingAlert.LastUpdated = DateTime.Now;
                    }
                }
                else
                {
                    // Remove cleared alerts
                    var alertToRemove = _activeAlerts.FirstOrDefault(a => a.RuleName == rule.Name);
                    if (alertToRemove != null)
                    {
                        alertToRemove.ClearedAt = DateTime.Now;
                        Console.WriteLine($"[{_displayName}] ALERT CLEARED: {alertToRemove.RuleName}");
                    }
                }
            }

            // Add new alerts
            _activeAlerts.AddRange(newAlerts);

            // Remove cleared alerts
            _activeAlerts.RemoveAll(a => a.ClearedAt.HasValue);

            // Display new alerts
            foreach (var alert in newAlerts)
            {
                DisplayAlert(alert);
            }
        }

        private void DisplayAlert(WeatherAlert alert)
        {
            var severitySymbol = alert.Severity switch
            {
                AlertSeverity.High => "🚨",
                AlertSeverity.Medium => "⚠️",
                AlertSeverity.Low => "ℹ️",
                _ => "🔔"
            };

            Console.WriteLine($"\n{severitySymbol} WEATHER ALERT from {_displayName} {severitySymbol}");
            Console.WriteLine($"Severity: {alert.Severity}");
            Console.WriteLine($"Alert: {alert.RuleName}");
            Console.WriteLine($"Message: {alert.Message}");
            Console.WriteLine($"Triggered: {alert.TriggeredAt:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Current Conditions: {alert.WeatherData}");
            Console.WriteLine(new string('=', 50));
        }

        private void DisplayActiveAlerts()
        {
            if (_activeAlerts.Any())
            {
                Console.WriteLine($"\n--- {_displayName} - Active Alerts ({_activeAlerts.Count}) ---");
                foreach (var alert in _activeAlerts.OrderByDescending(a => a.Severity))
                {
                    var duration = DateTime.Now - alert.TriggeredAt;
                    Console.WriteLine($"{alert.Severity}: {alert.RuleName} (Active for {duration.TotalMinutes:F0} min)");
                }

                Console.WriteLine(new string('-', 40));
            }
        }

        public void Display()
        {
            Console.WriteLine($"\n=== {_displayName} Status ===");
            Console.WriteLine($"Monitoring {_alertRules.Count} alert conditions");
            Console.WriteLine($"Active alerts: {_activeAlerts.Count}");
            Console.WriteLine($"Current weather: {_currentWeather}");
            Console.WriteLine(new string('=', 30));
        }

        public string GetName()
        {
            return _displayName;
        }

        public int GetActiveAlertCount()
        {
            return _activeAlerts.Count;
        }

        public List<WeatherAlert> GetActiveAlerts()
        {
            return _activeAlerts.ToList();
        }

        public void ClearAllAlerts()
        {
            _activeAlerts.Clear();
            Console.WriteLine($"[{_displayName}] All alerts cleared");
        }

        // Alert rule definition
        private class AlertRule
        {
            public string Name { get; set; } = string.Empty;
            public Func<WeatherData, bool> Condition { get; set; } = _ => false;
            public AlertSeverity Severity { get; set; }
            public string Message { get; set; } = string.Empty;
        }

        // Alert severity levels
        public enum AlertSeverity
        {
            Low,
            Medium,
            High
        }

        // Weather alert data structure
        public class WeatherAlert
        {
            public string RuleName { get; set; } = string.Empty;
            public AlertSeverity Severity { get; set; }
            public string Message { get; set; } = string.Empty;
            public DateTime TriggeredAt { get; set; }
            public DateTime LastUpdated { get; set; }
            public DateTime? ClearedAt { get; set; }
            public WeatherData WeatherData { get; set; } = new WeatherData();
        }
    }
}