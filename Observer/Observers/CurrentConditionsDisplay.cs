using Observer.Models;

namespace Observer.Observers
{
    /// <summary>
    /// Current conditions display implementation
    /// Shows current weather measurements
    /// </summary>
    public class CurrentConditionsDisplay : IWeatherObserver
    {
        private WeatherData _currentWeather = new WeatherData();
        private readonly string _displayName;

        public CurrentConditionsDisplay(string displayName)
        {
            _displayName = displayName;
        }

        public void Update(WeatherData weatherData)
        {
            _currentWeather = weatherData;
            Display();
        }

        public void Display()
        {
            Console.WriteLine($"\n=== {_displayName} ===");
            Console.WriteLine($"Current Weather Conditions:");
            Console.WriteLine($"Temperature: {_currentWeather.Temperature:F1}°C");
            Console.WriteLine($"Humidity: {_currentWeather.Humidity:F1}%");
            Console.WriteLine($"Pressure: {_currentWeather.Pressure:F1} hPa");
            Console.WriteLine($"Condition: {_currentWeather.Condition}");
            Console.WriteLine($"Last Update: {_currentWeather.Timestamp:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine(new string('=', 30));
        }

        public string GetName()
        {
            return _displayName;
        }

        public WeatherData GetCurrentWeather()
        {
            return _currentWeather;
        }
    }
}