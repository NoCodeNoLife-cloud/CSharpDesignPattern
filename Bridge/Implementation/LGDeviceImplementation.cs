namespace Bridge.Implementation
{
    /// <summary>
    /// LG device implementation
    /// Concrete implementation for LG branded devices
    /// </summary>
    public class LGDeviceImplementation : IDeviceImplementation
    {
        private bool _isPoweredOn;
        private readonly int _powerConsumption;

        public LGDeviceImplementation(string modelName, int powerConsumption)
        {
            DeviceName = $"LG {modelName}";
            _powerConsumption = powerConsumption;
        }

        public string DeviceName { get; private set; }
        
        public bool IsPoweredOn => _isPoweredOn;
        
        public int PowerConsumption => _isPoweredOn ? _powerConsumption : 0;

        public void PowerOn()
        {
            if (!_isPoweredOn)
            {
                _isPoweredOn = true;
                Console.WriteLine($"[LG] Powering ON {DeviceName}");
            }
        }

        public void PowerOff()
        {
            if (_isPoweredOn)
            {
                _isPoweredOn = false;
                Console.WriteLine($"[LG] Powering OFF {DeviceName}");
            }
        }

        public string GetDeviceStatus()
        {
            var status = _isPoweredOn ? "ON" : "OFF";
            var consumption = _isPoweredOn ? $"{_powerConsumption}W" : "0W";
            return $"[LG Device Status]\nModel: {DeviceName}\nPower: {status}\nConsumption: {consumption}";
        }
    }
}