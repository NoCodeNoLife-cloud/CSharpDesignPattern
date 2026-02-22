namespace Bridge.Implementation
{
    /// <summary>
    /// Sony device implementation
    /// Concrete implementation for Sony branded devices
    /// </summary>
    public class SonyDeviceImplementation : IDeviceImplementation
    {
        private bool _isPoweredOn;
        private readonly int _powerConsumption;
        private readonly string _serialNumber;

        public SonyDeviceImplementation(string modelName, int powerConsumption)
        {
            DeviceName = $"Sony {modelName}";
            _powerConsumption = powerConsumption;
            _serialNumber = GenerateSerialNumber();
        }

        public string DeviceName { get; private set; }
        
        public bool IsPoweredOn => _isPoweredOn;
        
        public int PowerConsumption => _isPoweredOn ? _powerConsumption : 0;
        
        public string SerialNumber => _serialNumber;

        public void PowerOn()
        {
            if (!_isPoweredOn)
            {
                _isPoweredOn = true;
                Console.WriteLine($"[Sony] α (alpha) powering ON {DeviceName}");
            }
        }

        public void PowerOff()
        {
            if (_isPoweredOn)
            {
                _isPoweredOn = false;
                Console.WriteLine($"[Sony] α (alpha) powering OFF {DeviceName}");
            }
        }

        public string GetDeviceStatus()
        {
            var status = _isPoweredOn ? "ACTIVE" : "STANDBY";
            var consumption = _isPoweredOn ? $"{_powerConsumption}W" : "0W";
            return $"[Sony α Device Status]\nModel: {DeviceName}\nSerial: {_serialNumber}\nStatus: {status}\nPower: {consumption}";
        }

        private static string GenerateSerialNumber()
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}