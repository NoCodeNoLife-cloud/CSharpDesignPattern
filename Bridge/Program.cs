// See https://aka.ms/new-console-template for more information
using Bridge.Abstraction;
using Bridge.Controls;
using Bridge.Implementation;
using Bridge.Systems;

namespace Bridge
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Bridge Pattern Implementation Examples ===\n");

            try
            {
                // Demonstrate basic bridge pattern
                DemonstrateBasicBridge();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate remote control usage
                DemonstrateRemoteControl();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate home entertainment system
                DemonstrateHomeEntertainmentSystem();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate bridge pattern benefits
                DemonstrateBridgeBenefits();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\n=== Bridge Pattern Demo Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates basic bridge pattern concepts
        /// </summary>
        private static void DemonstrateBasicBridge()
        {
            Console.WriteLine("1. Basic Bridge Pattern Demo");
            Console.WriteLine("============================");
            
            // Create different device implementations
            var lgImplementation = new LGDeviceImplementation("OLED65C1PUB", 150);
            var sonyImplementation = new SonyDeviceImplementation("HT-A7000", 200);
            
            // Create devices with different implementations
            var lgTv = new Television(lgImplementation);
            var sonySound = new SoundSystem(sonyImplementation);
            
            Console.WriteLine("\n--- LG Television ---");
            lgTv.TurnOn();
            lgTv.ChangeChannel(5);
            lgTv.AdjustVolume(10);
            Console.WriteLine(lgTv.GetStatus());
            lgTv.Operate();
            lgTv.TurnOff();
            
            Console.WriteLine("\n--- Sony Sound System ---");
            sonySound.TurnOn();
            sonySound.AdjustBass(15);
            sonySound.ChangeSoundMode(SoundMode.Surround);
            Console.WriteLine(sonySound.GetStatus());
            sonySound.Operate();
            sonySound.TurnOff();
        }

        /// <summary>
        /// Demonstrates remote control functionality
        /// </summary>
        private static void DemonstrateRemoteControl()
        {
            Console.WriteLine("\n2. Remote Control Demo");
            Console.WriteLine("======================");
            
            // Create devices
            var tv = new Television(new LGDeviceImplementation("NanoCell 75UP8000", 180));
            var soundSystem = new SoundSystem(new SonyDeviceImplementation("BDV-N9200W", 160));
            
            // Create remotes
            var tvRemote = new RemoteControl(tv);
            var soundRemote = new AdvancedRemoteControl(soundSystem);
            
            // Show available controls
            tvRemote.ShowAvailableControls();
            soundRemote.ShowAvailableControls();
            
            // Control devices via remotes
            Console.WriteLine("\n--- Controlling Devices via Remotes ---");
            tvRemote.PowerToggle();
            tvRemote.DisplayDeviceInfo();
            tvRemote.OperateDevice();
            
            soundRemote.PowerToggle();
            soundRemote.DisplayDeviceInfo();
            soundRemote.OperateDevice();
            
            // Advanced remote features
            if (soundRemote is AdvancedRemoteControl advancedRemote)
            {
                advancedRemote.MuteDevice();
                advancedRemote.SetTimer(30);
            }
        }

        /// <summary>
        /// Demonstrates complete home entertainment system
        /// </summary>
        private static void DemonstrateHomeEntertainmentSystem()
        {
            Console.WriteLine("\n3. Home Entertainment System Demo");
            Console.WriteLine("=================================");
            
            // Create and configure home entertainment system
            var homeSystem = HomeEntertainmentSystem.CreateSampleSystem();
            
            // Display initial status
            homeSystem.DisplaySystemStatus();
            
            // Power on all devices
            homeSystem.PowerOnAllDevices();
            
            // Run movie scenario
            homeSystem.RunMovieScenario();
            
            // Display updated status
            homeSystem.DisplaySystemStatus();
            
            // Power off all devices
            homeSystem.PowerOffAllDevices();
            
            // Final status
            homeSystem.DisplaySystemStatus();
        }

        /// <summary>
        /// Demonstrates bridge pattern benefits and flexibility
        /// </summary>
        private static void DemonstrateBridgeBenefits()
        {
            Console.WriteLine("\n4. Bridge Pattern Benefits Demo");
            Console.WriteLine("================================");
            
            Console.WriteLine("Bridge Pattern Key Benefits:");
            Console.WriteLine("• Decouples abstraction from implementation");
            Console.WriteLine("• Enables independent variation of both parts");
            Console.WriteLine("• Supports runtime implementation switching");
            Console.WriteLine("• Reduces class explosion in hierarchies");
            Console.WriteLine("• Promotes composition over inheritance");
            
            Console.WriteLine("\nFlexibility Demonstrated:");
            Console.WriteLine("• Same TV abstraction works with LG and Sony implementations");
            Console.WriteLine("• Same remote control works with different device types");
            Console.WriteLine("• Can mix and match abstractions and implementations freely");
            Console.WriteLine("• Easy to add new brands or device types");
            
            Console.WriteLine("\nReal-world Applications:");
            Console.WriteLine("• GUI frameworks (platform-independent widgets)");
            Console.WriteLine("• Database drivers (same interface, different databases)");
            Console.WriteLine("• Graphics libraries (shapes with different rendering engines)");
            Console.WriteLine("• Network protocols (same service, different transport layers)");
            Console.WriteLine("• Home automation systems (devices with different manufacturers)");
        }
    }
}