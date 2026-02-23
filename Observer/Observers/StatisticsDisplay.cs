using Observer.Models;

namespace Observer.Observers
{
    /// <summary>
    /// Statistics display implementation
    /// Tracks and displays weather statistics
    /// </summary>
    public class StatisticsDisplay : IWeatherObserver
    {
        private readonly List<WeatherData> _weatherHistory = new List<WeatherData>();
        private readonly string _displayName;
        private const int MaxHistory = 24; // Keep last 24 readings

        public StatisticsDisplay(string displayName)
        {
            _displayName = displayName;
        }

        public void Update(WeatherData weatherData)
        {
            _weatherHistory.Add(weatherData);
            
            // Maintain history size
            if (_weatherHistory.Count > MaxHistory)
            {
                _weatherHistory.RemoveAt(0);
            }
            
            Display();
        }

        public void Display()
        {
            if (_weatherHistory.Count == 0)
            {
                Console.WriteLine($"\n=== {_displayName} ===");
                Console.WriteLine("No weather data available yet.");
                Console.WriteLine(new string('=', 30));
                return;
            }

            Console.WriteLine($"\n=== {_displayName} ===");
            Console.WriteLine($"Data Points: {_weatherHistory.Count}");
            
            // Temperature statistics
            var temperatures = _weatherHistory.Select(w => w.Temperature).ToList();
            Console.WriteLine($"\nTemperature Statistics:");
            Console.WriteLine($"  Min: {temperatures.Min():F1}°C");
            Console.WriteLine($"  Max: {temperatures.Max():F1}°C");
            Console.WriteLine($"  Avg: {temperatures.Average():F1}°C");
            
            // Humidity statistics
            var humidities = _weatherHistory.Select(w => w.Humidity).ToList();
            Console.WriteLine($"\nHumidity Statistics:");
            Console.WriteLine($"  Min: {humidities.Min():F1}%");
            Console.WriteLine($"  Max: {humidities.Max():F1}%");
            Console.WriteLine($"  Avg: {humidities.Average():F1}%");
            
            // Pressure statistics
            var pressures = _weatherHistory.Select(w => w.Pressure).ToList();
            Console.WriteLine($"\nPressure Statistics:");
            Console.WriteLine($"  Min: {pressures.Min():F1} hPa");
            Console.WriteLine($"  Max: {pressures.Max():F1} hPa");
            Console.WriteLine($"  Avg: {pressures.Average():F1} hPa");
            
            // Trend analysis
            if (_weatherHistory.Count >= 2)
            {
                var recentTemp = _weatherHistory[_weatherHistory.Count - 1].Temperature;
                var previousTemp = _weatherHistory[_weatherHistory.Count - 2].Temperature;
                var tempTrend = recentTemp > previousTemp ? "rising" : recentTemp < previousTemp ? "falling" : "stable";
                
                Console.WriteLine($"\nRecent Trends:");
                Console.WriteLine($"  Temperature: {tempTrend}");
                Console.WriteLine($"  Time Span: {_weatherHistory.First().Timestamp:HH:mm} - {_weatherHistory.Last().Timestamp:HH:mm}");
            }
            
            Console.WriteLine(new string('=', 30));
        }

        public string GetName()
        {
            return _displayName;
        }

        public int GetDataPointCount()
        {
            return _weatherHistory.Count;
        }

        public void ClearHistory()
        {
            _weatherHistory.Clear();
            Console.WriteLine($"[{_displayName}] History cleared");
        }
    }
}