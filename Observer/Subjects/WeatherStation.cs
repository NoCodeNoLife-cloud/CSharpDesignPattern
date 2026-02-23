using Observer.Observers;
using Observer.Models;

namespace Observer.Subjects
{
    /// <summary>
    /// Weather station implementation
    /// Acts as the subject in the observer pattern
    /// </summary>
    public class WeatherStation : IWeatherSubject
    {
        private readonly List<IWeatherObserver> _observers = new List<IWeatherObserver>();
        private WeatherData _currentWeather = new WeatherData();
        private readonly string _stationName;

        public WeatherStation(string stationName)
        {
            _stationName = stationName;
            Console.WriteLine($"[WeatherStation] {_stationName} initialized");
        }

        public void RegisterObserver(IWeatherObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                Console.WriteLine($"[WeatherStation] Registered observer: {observer.GetName()}");
            }
        }

        public void RemoveObserver(IWeatherObserver observer)
        {
            if (_observers.Remove(observer))
            {
                Console.WriteLine($"[WeatherStation] Removed observer: {observer.GetName()}");
            }
        }

        public void NotifyObservers()
        {
            Console.WriteLine($"\n[WeatherStation] Notifying {_observers.Count} observers about weather update");
            foreach (var observer in _observers)
            {
                try
                {
                    observer.Update(_currentWeather);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[WeatherStation] Error notifying {observer.GetName()}: {ex.Message}");
                }
            }
        }

        public WeatherData GetWeatherData()
        {
            return _currentWeather;
        }

        public string GetStationName()
        {
            return _stationName;
        }

        public int GetObserverCount()
        {
            return _observers.Count;
        }

        public List<string> GetRegisteredObservers()
        {
            return _observers.Select(o => o.GetName()).ToList();
        }

        // Weather data update methods
        public void SetMeasurements(double temperature, double humidity, double pressure, WeatherCondition condition)
        {
            Console.WriteLine($"\n[WeatherStation] New measurements received at {_stationName}");
            
            var oldWeather = _currentWeather;
            _currentWeather = new WeatherData(temperature, humidity, pressure, condition);
            
            Console.WriteLine($"Previous: {oldWeather}");
            Console.WriteLine($"Current:  {_currentWeather}");
            
            // Notify observers about the change
            NotifyObservers();
        }

        public void SetTemperature(double temperature)
        {
            _currentWeather.Temperature = temperature;
            _currentWeather.Timestamp = DateTime.Now;
            Console.WriteLine($"[WeatherStation] Temperature updated to {temperature:F1}°C");
            NotifyObservers();
        }

        public void SetHumidity(double humidity)
        {
            _currentWeather.Humidity = humidity;
            _currentWeather.Timestamp = DateTime.Now;
            Console.WriteLine($"[WeatherStation] Humidity updated to {humidity:F1}%");
            NotifyObservers();
        }

        public void SetPressure(double pressure)
        {
            _currentWeather.Pressure = pressure;
            _currentWeather.Timestamp = DateTime.Now;
            Console.WriteLine($"[WeatherStation] Pressure updated to {pressure:F1} hPa");
            NotifyObservers();
        }

        public void SetCondition(WeatherCondition condition)
        {
            _currentWeather.Condition = condition;
            _currentWeather.Timestamp = DateTime.Now;
            Console.WriteLine($"[WeatherStation] Weather condition updated to {condition}");
            NotifyObservers();
        }

        // Simulate automatic weather updates
        public void StartAutomaticUpdates(int intervalSeconds = 5)
        {
            Console.WriteLine($"[WeatherStation] Starting automatic updates every {intervalSeconds} seconds");
            
            var random = new Random();
            var conditions = Enum.GetValues<WeatherCondition>().Where(c => c != WeatherCondition.Unknown).ToArray();
            
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        // Generate realistic weather variations
                        var tempChange = (random.NextDouble() - 0.5) * 2; // ±1°C
                        var humidityChange = (random.NextDouble() - 0.5) * 10; // ±5%
                        var pressureChange = (random.NextDouble() - 0.5) * 10; // ±5 hPa
                        
                        _currentWeather.Temperature += tempChange;
                        _currentWeather.Humidity += humidityChange;
                        _currentWeather.Pressure += pressureChange;
                        _currentWeather.Condition = conditions[random.Next(conditions.Length)];
                        _currentWeather.Timestamp = DateTime.Now;
                        
                        Console.WriteLine($"\n[WeatherStation] Automatic update at {_stationName}");
                        Console.WriteLine($"Current: {_currentWeather}");
                        
                        NotifyObservers();
                        
                        await Task.Delay(intervalSeconds * 1000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[WeatherStation] Error in automatic updates: {ex.Message}");
                        break;
                    }
                }
            });
        }
    }
}