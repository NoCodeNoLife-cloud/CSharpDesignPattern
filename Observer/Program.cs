// See https://aka.ms/new-console-template for more information
using Observer.Subjects;
using Observer.Observers;
using Observer.Models;

namespace Observer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Observer Pattern Implementation Examples ===\n");

            try
            {
                // Demonstrate basic observer pattern
                DemonstrateBasicObserver();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate dynamic observer registration
                DemonstrateDynamicRegistration();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate alert system
                DemonstrateAlertSystem();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate multiple weather stations
                DemonstrateMultipleStations();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate observer pattern benefits
                DemonstratePatternBenefits();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\n=== Observer Pattern Demo Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates basic observer pattern operation
        /// </summary>
        private static void DemonstrateBasicObserver()
        {
            Console.WriteLine("1. Basic Observer Pattern Demo");
            Console.WriteLine("===============================");
            
            var weatherStation = new WeatherStation("Central Weather Station");
            
            // Create observers
            var currentDisplay = new CurrentConditionsDisplay("Main Display");
            var statsDisplay = new StatisticsDisplay("Statistics Monitor");
            var forecastDisplay = new ForecastDisplay("Weather Forecast");
            
            // Register observers
            weatherStation.RegisterObserver(currentDisplay);
            weatherStation.RegisterObserver(statsDisplay);
            weatherStation.RegisterObserver(forecastDisplay);
            
            // Simulate weather updates
            Console.WriteLine("\nSending initial weather data:");
            weatherStation.SetMeasurements(25.5, 65.0, 1013.2, WeatherCondition.Sunny);
            
            Console.WriteLine("\nSending updated weather data:");
            weatherStation.SetMeasurements(27.8, 70.5, 1010.8, WeatherCondition.Cloudy);
            
            Console.WriteLine("\nSending another update:");
            weatherStation.SetMeasurements(22.3, 85.2, 1005.5, WeatherCondition.Rainy);
            
            Console.WriteLine($"\nStation has {weatherStation.GetObserverCount()} registered observers");
        }

        /// <summary>
        /// Demonstrates dynamic observer registration and removal
        /// </summary>
        private static void DemonstrateDynamicRegistration()
        {
            Console.WriteLine("\n2. Dynamic Registration Demo");
            Console.WriteLine("=============================");
            
            var weatherStation = new WeatherStation("Dynamic Weather Station");
            
            // Start with basic observer
            var basicDisplay = new CurrentConditionsDisplay("Basic Monitor");
            weatherStation.RegisterObserver(basicDisplay);
            
            Console.WriteLine("\nInitial weather update:");
            weatherStation.SetMeasurements(20.0, 50.0, 1020.0, WeatherCondition.Sunny);
            
            // Add more observers dynamically
            Console.WriteLine("\nAdding advanced monitoring:");
            var alertDisplay = new AlertDisplay("Emergency Alerts");
            var forecastDisplay = new ForecastDisplay("Advanced Forecast");
            
            weatherStation.RegisterObserver(alertDisplay);
            weatherStation.RegisterObserver(forecastDisplay);
            
            Console.WriteLine($"\nNow monitoring with {weatherStation.GetObserverCount()} observers");
            Console.WriteLine("Registered observers: " + string.Join(", ", weatherStation.GetRegisteredObservers()));
            
            // Send extreme weather to trigger alerts
            Console.WriteLine("\n⚠️ Sending extreme weather data:");
            weatherStation.SetMeasurements(36.5, 45.0, 975.0, WeatherCondition.Stormy);
            
            // Remove an observer
            Console.WriteLine("\nRemoving basic monitor:");
            weatherStation.RemoveObserver(basicDisplay);
            
            Console.WriteLine($"\nRemaining observers: {weatherStation.GetObserverCount()}");
            weatherStation.SetMeasurements(15.2, 90.0, 995.0, WeatherCondition.Rainy);
        }

        /// <summary>
        /// Demonstrates alert system functionality
        /// </summary>
        private static void DemonstrateAlertSystem()
        {
            Console.WriteLine("\n3. Alert System Demo");
            Console.WriteLine("====================");
            
            var weatherStation = new WeatherStation("Safety Monitoring Station");
            var alertDisplay = new AlertDisplay("Critical Weather Alerts");
            var currentDisplay = new CurrentConditionsDisplay("Safety Dashboard");
            
            weatherStation.RegisterObserver(alertDisplay);
            weatherStation.RegisterObserver(currentDisplay);
            
            Console.WriteLine("\n🧪 Testing various alert conditions:");
            
            // Test extreme heat
            Console.WriteLine("\n1. Testing extreme heat condition:");
            weatherStation.SetMeasurements(38.0, 40.0, 1015.0, WeatherCondition.Sunny);
            
            // Test freezing conditions
            Console.WriteLine("\n2. Testing freezing condition:");
            weatherStation.SetMeasurements(-2.5, 60.0, 1025.0, WeatherCondition.Snowy);
            
            // Test storm conditions
            Console.WriteLine("\n3. Testing storm condition:");
            weatherStation.SetMeasurements(22.0, 95.0, 980.0, WeatherCondition.Stormy);
            
            // Return to normal conditions
            Console.WriteLine("\n4. Returning to normal conditions:");
            weatherStation.SetMeasurements(23.5, 65.0, 1018.0, WeatherCondition.Cloudy);
            
            Console.WriteLine($"\nFinal active alerts: {alertDisplay.GetActiveAlertCount()}");
        }

        /// <summary>
        /// Demonstrates multiple weather stations with shared observers
        /// </summary>
        private static void DemonstrateMultipleStations()
        {
            Console.WriteLine("\n4. Multiple Stations Demo");
            Console.WriteLine("=========================");
            
            // Create shared observers
            var masterDisplay = new CurrentConditionsDisplay("Master Control");
            var statsDisplay = new StatisticsDisplay("Regional Statistics");
            
            // Create multiple weather stations
            var station1 = new WeatherStation("North Station");
            var station2 = new WeatherStation("South Station");
            var station3 = new WeatherStation("East Station");
            
            // Register observers to all stations
            station1.RegisterObserver(masterDisplay);
            station1.RegisterObserver(statsDisplay);
            
            station2.RegisterObserver(masterDisplay);
            station2.RegisterObserver(statsDisplay);
            
            station3.RegisterObserver(masterDisplay);
            station3.RegisterObserver(statsDisplay);
            
            Console.WriteLine("\n📊 Simulating coordinated weather monitoring:");
            
            // Send coordinated updates
            station1.SetMeasurements(24.5, 60.0, 1012.0, WeatherCondition.Sunny);
            Thread.Sleep(1000);
            
            station2.SetMeasurements(26.8, 55.0, 1010.5, WeatherCondition.Cloudy);
            Thread.Sleep(1000);
            
            station3.SetMeasurements(22.3, 70.0, 1015.8, WeatherCondition.Rainy);
            
            Console.WriteLine($"\nMaster display has received data from {statsDisplay.GetDataPointCount()} stations");
        }

        /// <summary>
        /// Demonstrates observer pattern benefits and concepts
        /// </summary>
        private static void DemonstratePatternBenefits()
        {
            Console.WriteLine("\n5. Observer Pattern Benefits Demo");
            Console.WriteLine("==================================");
            
            Console.WriteLine("Observer Pattern Key Benefits:");
            Console.WriteLine("• Loose coupling between subjects and observers");
            Console.WriteLine("• Dynamic subscription management");
            Console.WriteLine("• Broadcast communication mechanism");
            Console.WriteLine("• Easy addition of new observers");
            Console.WriteLine("• Support for multiple subscribers");
            
            Console.WriteLine("\nCommon Use Cases:");
            Console.WriteLine("• GUI event handling");
            Console.WriteLine("• Model-View-Controller (MVC) architecture");
            Console.WriteLine("• Real-time data feeds");
            Console.WriteLine("• Notification systems");
            Console.WriteLine("• Sensor networks");
            Console.WriteLine("• Social media updates");
            
            Console.WriteLine("\nWhen to Use Observer Pattern:");
            Console.WriteLine("• One-to-many dependency relationships");
            Console.WriteLine("• Need for automatic notification");
            Console.WriteLine("• Want to avoid polling mechanisms");
            Console.WriteLine("• Need loose coupling between components");
            Console.WriteLine("• Want to support multiple listeners");
            
            Console.WriteLine("\nWeather Monitoring System Features Demonstrated:");
            Console.WriteLine("• Real-time condition monitoring");
            Console.WriteLine("• Statistical analysis and trending");
            Console.WriteLine("• Predictive forecasting");
            Console.WriteLine("• Emergency alert systems");
            Console.WriteLine("• Multi-station coordination");
        }
    }
}