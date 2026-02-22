namespace Bridge.Implementation
{
    /// <summary>
    /// Device implementation interface
    /// Defines the implementation interface for device operations
    /// </summary>
    public interface IDeviceImplementation
    {
        /// <summary>
        /// Gets the device name
        /// </summary>
        string DeviceName { get; }
        
        /// <summary>
        /// Powers on the device
        /// </summary>
        void PowerOn();
        
        /// <summary>
        /// Powers off the device
        /// </summary>
        void PowerOff();
        
        /// <summary>
        /// Gets device status information
        /// </summary>
        string GetDeviceStatus();
        
        /// <summary>
        /// Indicates whether the device is powered on
        /// </summary>
        bool IsPoweredOn { get; }
        
        /// <summary>
        /// Gets device power consumption in watts
        /// </summary>
        int PowerConsumption { get; }
    }
}