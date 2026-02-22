using Bridge.Abstraction;

namespace Bridge.Controls
{
    /// <summary>
    /// Remote control class
    /// Demonstrates bridge pattern usage in controlling different devices
    /// </summary>
    public class RemoteControl
    {
        private readonly Device _device;

        public RemoteControl(Device device)
        {
            _device = device;
        }

        public string DeviceName => _device.Name;

        public void PowerToggle()
        {
            Console.WriteLine($"\n--- Remote Control for {_device.Name} ---");
            if (IsDeviceOn())
            {
                TurnOffDevice();
            }
            else
            {
                TurnOnDevice();
            }
        }

        public void TurnOnDevice()
        {
            Console.WriteLine($"Remote: Sending POWER ON command to {_device.Name}");
            _device.TurnOn();
        }

        public void TurnOffDevice()
        {
            Console.WriteLine($"Remote: Sending POWER OFF command to {_device.Name}");
            _device.TurnOff();
        }

        public bool IsDeviceOn()
        {
            // This is a simplified check - in real implementation, 
            // you might need to query the device status
            return _device.GetStatus().Contains("ON") || _device.GetStatus().Contains("ACTIVE");
        }

        public void DisplayDeviceInfo()
        {
            Console.WriteLine($"\n=== Device Information: {_device.Name} ===");
            Console.WriteLine(_device.GetStatus());
            Console.WriteLine("=======================================\n");
        }

        public void OperateDevice()
        {
            Console.WriteLine($"Remote: Sending OPERATE command to {_device.Name}");
            _device.Operate();
        }

        /// <summary>
        /// Gets device-specific controls based on device type
        /// </summary>
        public void ShowAvailableControls()
        {
            Console.WriteLine($"\n--- Available Controls for {_device.Name} ---");
            
            if (_device is Television tv)
            {
                Console.WriteLine("TV Controls:");
                Console.WriteLine("  • Power Toggle");
                Console.WriteLine("  • Channel Up/Down");
                Console.WriteLine("  • Volume Adjust");
                Console.WriteLine($"  • Current Channel: {tv.Channel}");
                Console.WriteLine($"  • Current Volume: {tv.Volume}%");
            }
            else if (_device is SoundSystem ss)
            {
                Console.WriteLine("Sound System Controls:");
                Console.WriteLine("  • Power Toggle");
                Console.WriteLine("  • Bass/Treble Adjust");
                Console.WriteLine("  • Sound Mode Change");
                Console.WriteLine($"  • Bass Level: {ss.Bass}%");
                Console.WriteLine($"  • Treble Level: {ss.Treble}%");
                Console.WriteLine($"  • Current Mode: {ss.SoundMode}");
            }
            Console.WriteLine("----------------------------------------\n");
        }
    }

    /// <summary>
    /// Advanced remote control with additional features
    /// </summary>
    public class AdvancedRemoteControl : RemoteControl
    {
        public AdvancedRemoteControl(Device device) : base(device)
        {
        }

        public void MuteDevice()
        {
            Console.WriteLine($"Advanced Remote: Sending MUTE command to {DeviceName}");
            if (IsDeviceOn())
            {
                // Implementation would depend on device type
                Console.WriteLine("Device muted");
            }
            else
            {
                Console.WriteLine("Cannot mute - device is off");
            }
        }

        public void SetTimer(int minutes)
        {
            Console.WriteLine($"Advanced Remote: Setting {minutes}-minute timer for {DeviceName}");
            // Timer implementation would go here
        }
    }
}