using Bridge.Implementation;

namespace Bridge.Abstraction
{
    /// <summary>
    /// Television concrete abstraction
    /// Implements TV-specific functionality
    /// </summary>
    public class Television : Device
    {
        private int _channel = 1;
        private int _volume = 50;

        public Television(IDeviceImplementation implementation) : base(implementation)
        {
        }

        public override void TurnOn()
        {
            _implementation.PowerOn();
            Console.WriteLine($"Television '{Name}' is now ON");
        }

        public override void TurnOff()
        {
            _implementation.PowerOff();
            Console.WriteLine($"Television '{Name}' is now OFF");
        }

        public override string GetStatus()
        {
            var baseStatus = _implementation.GetDeviceStatus();
            return $"{baseStatus}\nChannel: {_channel}\nVolume: {_volume}%";
        }

        public override void Operate()
        {
            if (_implementation.IsPoweredOn)
            {
                ChangeChannel(_channel + 1);
                AdjustVolume(5);
                Console.WriteLine($"TV Operation: Channel {_channel}, Volume {_volume}%");
            }
            else
            {
                Console.WriteLine("Cannot operate TV - device is powered off");
            }
        }

        public void ChangeChannel(int channel)
        {
            _channel = Math.Max(1, Math.Min(999, channel));
            Console.WriteLine($"Channel changed to {_channel}");
        }

        public void AdjustVolume(int adjustment)
        {
            _volume = Math.Max(0, Math.Min(100, _volume + adjustment));
            Console.WriteLine($"Volume adjusted to {_volume}%");
        }

        public int Channel => _channel;
        public int Volume => _volume;
    }
}