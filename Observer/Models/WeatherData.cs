namespace Observer.Models
{
    /// <summary>
    /// Weather data model
    /// Contains current weather measurements
    /// </summary>
    public class WeatherData
    {
        public double Temperature { get; set; } // Celsius
        public double Humidity { get; set; }    // Percentage
        public double Pressure { get; set; }    // hPa
        public DateTime Timestamp { get; set; }
        public WeatherCondition Condition { get; set; }

        public WeatherData()
        {
            Timestamp = DateTime.Now;
            Condition = WeatherCondition.Unknown;
        }

        public WeatherData(double temperature, double humidity, double pressure, WeatherCondition condition)
        {
            Temperature = temperature;
            Humidity = humidity;
            Pressure = pressure;
            Condition = condition;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Temperature:F1}°C, {Humidity:F1}% humidity, {Pressure:F1} hPa, {Condition}";
        }

        public string ToDetailedString()
        {
            return $"Weather Data [{Timestamp:yyyy-MM-dd HH:mm:ss}]:\n" +
                   $"  Temperature: {Temperature:F1}°C\n" +
                   $"  Humidity: {Humidity:F1}%\n" +
                   $"  Pressure: {Pressure:F1} hPa\n" +
                   $"  Condition: {Condition}";
        }
    }

    /// <summary>
    /// Weather condition enumeration
    /// </summary>
    public enum WeatherCondition
    {
        Sunny,
        Cloudy,
        Rainy,
        Stormy,
        Snowy,
        Foggy,
        Unknown
    }
}