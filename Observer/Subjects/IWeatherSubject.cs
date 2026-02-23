using Observer.Models;
using Observer.Observers;

namespace Observer.Subjects
{
    /// <summary>
    /// Weather subject interface
    /// Defines the contract for weather data providers
    /// </summary>
    public interface IWeatherSubject
    {
        /// <summary>
        /// Registers an observer
        /// </summary>
        void RegisterObserver(IWeatherObserver observer);

        /// <summary>
        /// Removes an observer
        /// </summary>
        void RemoveObserver(IWeatherObserver observer);

        /// <summary>
        /// Notifies all registered observers
        /// </summary>
        void NotifyObservers();

        /// <summary>
        /// Gets the current weather data
        /// </summary>
        WeatherData GetWeatherData();
    }
}