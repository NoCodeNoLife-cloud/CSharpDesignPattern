using Bridge.Implementation;

namespace Bridge.Abstraction
{
    /// <summary>
    /// Device abstraction base class
    /// Defines the interface for devices and holds a reference to implementation
    /// </summary>
    public abstract class Device
    {
        protected readonly IDeviceImplementation _implementation;

        protected Device(IDeviceImplementation implementation)
        {
            _implementation = implementation;
        }

        /// <summary>
        /// Turns the device on
        /// </summary>
        public abstract void TurnOn();

        /// <summary>
        /// Turns the device off
        /// </summary>
        public abstract void TurnOff();

        /// <summary>
        /// Gets device status information
        /// </summary>
        public abstract string GetStatus();

        /// <summary>
        /// Performs device-specific operation
        /// </summary>
        public abstract void Operate();

        /// <summary>
        /// Gets device name
        /// </summary>
        public virtual string Name => _implementation.DeviceName;
        
        /// <summary>
        /// Gets device power consumption
        /// </summary>
        public int PowerConsumption => _implementation.PowerConsumption;
    }
}