using Bridge.Abstraction;
using Bridge.Controls;
using Bridge.Implementation;

namespace Bridge.Systems
{
    /// <summary>
    /// Home entertainment system manager
    /// Demonstrates bridge pattern in a complete home entertainment scenario
    /// </summary>
    public class HomeEntertainmentSystem
    {
        private readonly List<Device> _devices = new List<Device>();
        private readonly List<RemoteControl> _remotes = new List<RemoteControl>();

        /// <summary>
        /// Adds a device to the system
        /// </summary>
        public void AddDevice(Device device)
        {
            _devices.Add(device);
            _remotes.Add(new RemoteControl(device));
            Console.WriteLine($"Added device: {device.Name}");
        }

        /// <summary>
        /// Gets all devices
        /// </summary>
        public IEnumerable<Device> GetDevices() => _devices.AsReadOnly();

        /// <summary>
        /// Gets all remotes
        /// </summary>
        public IEnumerable<RemoteControl> GetRemotes() => _remotes.AsReadOnly();

        /// <summary>
        /// Powers on all devices
        /// </summary>
        public void PowerOnAllDevices()
        {
            Console.WriteLine("\n=== Powering ON All Devices ===");
            foreach (var remote in _remotes)
            {
                if (!remote.IsDeviceOn())
                {
                    remote.PowerToggle();
                }
            }
            Console.WriteLine("===============================\n");
        }

        /// <summary>
        /// Powers off all devices
        /// </summary>
        public void PowerOffAllDevices()
        {
            Console.WriteLine("\n=== Powering OFF All Devices ===");
            foreach (var remote in _remotes)
            {
                if (remote.IsDeviceOn())
                {
                    remote.PowerToggle();
                }
            }
            Console.WriteLine("================================\n");
        }

        /// <summary>
        /// Displays system status
        /// </summary>
        public void DisplaySystemStatus()
        {
            Console.WriteLine("\n=== Home Entertainment System Status ===");
            Console.WriteLine($"Total Devices: {_devices.Count}");
            
            var poweredOnDevices = _remotes.Count(r => r.IsDeviceOn());
            Console.WriteLine($"Powered ON: {poweredOnDevices}");
            Console.WriteLine($"Powered OFF: {_devices.Count - poweredOnDevices}");
            
            var totalPower = _devices.Sum(d => d.PowerConsumption);
            Console.WriteLine($"Total Power Consumption: {totalPower}W");
            
            Console.WriteLine("\nDevice Details:");
            foreach (var remote in _remotes)
            {
                remote.DisplayDeviceInfo();
            }
            Console.WriteLine("========================================\n");
        }

        /// <summary>
        /// Runs a movie scenario
        /// </summary>
        public void RunMovieScenario()
        {
            Console.WriteLine("\n=== Movie Night Scenario ===");
            
            // Power on TV and sound system
            var tvRemote = _remotes.FirstOrDefault(r => r.DeviceName.Contains("TV"));
            var soundRemote = _remotes.FirstOrDefault(r => r.DeviceName.Contains("Sound"));
            
            if (tvRemote != null && soundRemote != null)
            {
                Console.WriteLine("Setting up for movie night...");
                
                // Turn on devices
                if (!tvRemote.IsDeviceOn()) tvRemote.PowerToggle();
                if (!soundRemote.IsDeviceOn()) soundRemote.PowerToggle();
                
                // Configure devices
                if (tvRemote is RemoteControl tvControl)
                {
                    // Simulate changing to HDMI input
                    Console.WriteLine("TV: Switching to HDMI input");
                }
                
                if (soundRemote is RemoteControl soundControl)
                {
                    // Simulate setting surround sound
                    Console.WriteLine("Sound System: Setting to surround sound mode");
                }
                
                Console.WriteLine("Movie setup complete! Enjoy your movie!");
            }
            else
            {
                Console.WriteLine("Required devices not found for movie scenario");
            }
            
            Console.WriteLine("============================\n");
        }

        /// <summary>
        /// Creates a sample entertainment system
        /// </summary>
        public static HomeEntertainmentSystem CreateSampleSystem()
        {
            var system = new HomeEntertainmentSystem();
            
            // Create devices with different implementations
            var lgTv = new Television(new LGDeviceImplementation("OLED55C1PUB", 120));
            var sonySound = new SoundSystem(new SonyDeviceImplementation("HT-A7000", 200));
            var lgSound2 = new SoundSystem(new LGDeviceImplementation("SK10Y", 150));
            
            system.AddDevice(lgTv);
            system.AddDevice(sonySound);
            system.AddDevice(lgSound2);
            
            return system;
        }
    }
}