using Observer.Models;

namespace Observer.Observers
{
    /// <summary>
    /// Forecast display implementation
    /// Makes weather predictions based on trends
    /// </summary>
    public class ForecastDisplay : IWeatherObserver
    {
        private readonly List<WeatherData> _recentData = new List<WeatherData>();
        private readonly string _displayName;
        private const int TrendAnalysisPeriod = 3; // Analyze last 3 data points

        public ForecastDisplay(string displayName)
        {
            _displayName = displayName;
        }

        public void Update(WeatherData weatherData)
        {
            _recentData.Add(weatherData);
            
            // Keep only recent data for trend analysis
            if (_recentData.Count > TrendAnalysisPeriod)
            {
                _recentData.RemoveAt(0);
            }
            
            Display();
        }

        public void Display()
        {
            Console.WriteLine($"\n=== {_displayName} ===");
            
            if (_recentData.Count < 2)
            {
                Console.WriteLine("Insufficient data for forecast. Need at least 2 data points.");
                Console.WriteLine("Current conditions: " + (_recentData.Any() ? _recentData.Last().ToString() : "No data"));
                Console.WriteLine(new string('=', 30));
                return;
            }

            var forecast = GenerateForecast();
            Console.WriteLine($"Forecast: {forecast.Prediction}");
            Console.WriteLine($"Confidence: {forecast.Confidence:P0}");
            
            if (!string.IsNullOrEmpty(forecast.Reasoning))
            {
                Console.WriteLine($"Reasoning: {forecast.Reasoning}");
            }
            
            Console.WriteLine($"\nBasis: {_recentData.Count} recent measurements");
            Console.WriteLine($"Latest: {_recentData.Last().ToString()}");
            Console.WriteLine(new string('=', 30));
        }

        private ForecastResult GenerateForecast()
        {
            var lastReading = _recentData.Last();
            var trend = AnalyzeTrends();
            
            // Simple forecast logic based on trends and current conditions
            if (lastReading.Condition == WeatherCondition.Stormy)
            {
                return new ForecastResult
                {
                    Prediction = "Stormy weather continuing",
                    Confidence = 0.8,
                    Reasoning = "Current stormy conditions observed"
                };
            }
            
            if (trend.TemperatureTrend > 0.5 && lastReading.Temperature > 25)
            {
                return new ForecastResult
                {
                    Prediction = "Hotter weather ahead",
                    Confidence = 0.7,
                    Reasoning = "Rising temperatures detected"
                };
            }
            
            if (trend.HumidityTrend > 2 && lastReading.Humidity > 80)
            {
                return new ForecastResult
                {
                    Prediction = "Rain likely soon",
                    Confidence = 0.75,
                    Reasoning = "Increasing humidity suggests precipitation"
                };
            }
            
            if (trend.PressureTrend < -2 && lastReading.Pressure < 1000)
            {
                return new ForecastResult
                {
                    Prediction = "Weather change approaching",
                    Confidence = 0.65,
                    Reasoning = "Falling pressure indicates weather system change"
                };
            }
            
            // Default forecast based on current conditions
            var defaultPrediction = lastReading.Condition switch
            {
                WeatherCondition.Sunny => "Continued sunny conditions",
                WeatherCondition.Cloudy => "Partly cloudy with possible clearing",
                WeatherCondition.Rainy => "Showers likely to continue",
                WeatherCondition.Snowy => "Cold conditions persisting",
                WeatherCondition.Foggy => "Low visibility conditions expected",
                _ => "Stable weather conditions"
            };
            
            return new ForecastResult
            {
                Prediction = defaultPrediction,
                Confidence = 0.6,
                Reasoning = "Based on current weather pattern"
            };
        }

        private WeatherTrend AnalyzeTrends()
        {
            if (_recentData.Count < 2)
                return new WeatherTrend();

            var temps = _recentData.Select(w => w.Temperature).ToList();
            var humids = _recentData.Select(w => w.Humidity).ToList();
            var pressures = _recentData.Select(w => w.Pressure).ToList();

            return new WeatherTrend
            {
                TemperatureTrend = CalculateTrend(temps),
                HumidityTrend = CalculateTrend(humids),
                PressureTrend = CalculateTrend(pressures)
            };
        }

        private double CalculateTrend(List<double> values)
        {
            if (values.Count < 2) return 0;
            
            // Simple linear trend calculation
            var n = values.Count;
            var sumX = n * (n - 1) / 2.0;
            var sumY = values.Sum();
            var sumXY = values.Select((value, index) => value * index).Sum();
            var sumXX = values.Select((_, index) => index * index).Sum();
            
            return (n * sumXY - sumX * sumY) / (n * sumXX - sumX * sumX);
        }

        public string GetName()
        {
            return _displayName;
        }

        public int GetAnalysisDataCount()
        {
            return _recentData.Count;
        }

        private class ForecastResult
        {
            public string Prediction { get; set; } = string.Empty;
            public double Confidence { get; set; }
            public string Reasoning { get; set; } = string.Empty;
        }

        private class WeatherTrend
        {
            public double TemperatureTrend { get; set; }
            public double HumidityTrend { get; set; }
            public double PressureTrend { get; set; }
        }
    }
}