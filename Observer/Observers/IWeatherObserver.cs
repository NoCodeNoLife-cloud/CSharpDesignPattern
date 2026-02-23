using Observer.Models;

namespace Observer.Observers
{
    /// <summary>
    /// Weather observer interface
    /// Defines the contract for weather data observers
    /// </summary>
    public interface IWeatherObserver
    {
        /// <summary>
        /// Updates the observer with new weather data
        /// </summary>
        void Update(WeatherData weatherData);

        /// <summary>
        /// Gets the observer name/description
        /// </summary>
        string GetName();
    }
}