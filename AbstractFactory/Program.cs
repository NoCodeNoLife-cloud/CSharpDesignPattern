using AbstractFactory.Application;
using AbstractFactory.Factories;

namespace AbstractFactory
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Abstract Factory Pattern Example ===\n");

            // Demonstrate Windows theme
            Console.WriteLine("1. Windows Theme Demo:");
            DemonstrateTheme(new WindowsFactory());

            Console.WriteLine("\n" + new string('=', 60) + "\n");

            // Demonstrate Mac theme
            Console.WriteLine("2. Mac Theme Demo:");
            DemonstrateTheme(new MacFactory());

            Console.WriteLine("\n" + new string('=', 60) + "\n");

            // Demonstrate dynamic theme switching
            Console.WriteLine("3. Dynamic Theme Switching Demo:");
            DemonstrateDynamicSwitching();

            Console.WriteLine("\n=== Example Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates UI creation with a specific theme
        /// </summary>
        private static void DemonstrateTheme(IUIFactory factory)
        {
            // Create application with specific factory
            var app = new UIApplication(factory);
            
            // Create and render UI
            app.CreateUI();
            app.RenderUI();
            app.SimulateUserActions();
            
            // Customize button
            app.SetButtonText("Submit");
            app.RenderUI();
        }

        /// <summary>
        /// Demonstrates dynamic theme switching capability
        /// </summary>
        private static void DemonstrateDynamicSwitching()
        {
            Console.WriteLine("Creating application with Windows theme initially...");
            
            // Start with Windows theme
            var app = new UIApplication(new WindowsFactory());
            app.CreateUI();
            app.RenderUI();
            
            Console.WriteLine("\nSwitching to Mac theme...");
            
            // Switch to Mac theme (in real scenario, would recreate app)
            var macApp = new UIApplication(new MacFactory());
            macApp.CreateUI();
            macApp.RenderUI();
            
            Console.WriteLine("\nTheme switching demonstrated successfully!");
            Console.WriteLine("Both applications maintain consistent component families.");
        }
    }
}