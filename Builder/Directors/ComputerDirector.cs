using Builder.Builders;
using Builder.Products;

namespace Builder.Directors
{
    /// <summary>
    /// Computer director
    /// Controls the construction process using builders
    /// </summary>
    public class ComputerDirector
    {
        private IComputerBuilder _builder;

        public ComputerDirector(IComputerBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Changes the builder
        /// </summary>
        public void SetBuilder(IComputerBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Builds basic computer configuration
        /// </summary>
        public Computer BuildBasicComputer()
        {
            Console.WriteLine("\n--- Building Basic Computer ---");
            return _builder
                .SetCPU("Intel Core i5-12400")
                .SetRAM("16GB DDR4")
                .SetStorage("500GB SSD")
                .GetComputer();
        }

        /// <summary>
        /// Builds high-end computer configuration
        /// </summary>
        public Computer BuildHighEndComputer()
        {
            Console.WriteLine("\n--- Building High-End Computer ---");
            return _builder
                .SetCPU("AMD Ryzen 9 7900X")
                .SetRAM("32GB DDR5")
                .SetStorage("1TB NVMe SSD")
                .SetGraphicsCard("NVIDIA RTX 4080")
                .SetMotherboard("ATX Gaming Motherboard")
                .SetPowerSupply("850W Gold")
                .SetCase("Full Tower RGB")
                .AddAccessory("RGB Lighting Kit")
                .AddAccessory("Liquid Cooling System")
                .GetComputer();
        }

        /// <summary>
        /// Builds budget computer configuration
        /// </summary>
        public Computer BuildBudgetComputer()
        {
            Console.WriteLine("\n--- Building Budget Computer ---");
            return _builder
                .SetCPU("Intel Core i3-12100")
                .SetRAM("8GB DDR4")
                .SetStorage("250GB SSD")
                .SetGraphicsCard("Integrated Graphics")
                .SetCase("Mini Tower")
                .GetComputer();
        }

        /// <summary>
        /// Builds custom computer configuration
        /// </summary>
        public Computer BuildCustomComputer(
            string cpu, 
            string ram, 
            string storage, 
            string graphicsCard = "Integrated",
            string motherboard = "Standard",
            string powerSupply = "500W",
            string computerCase = "Mid Tower",
            params string[] accessories)
        {
            Console.WriteLine("\n--- Building Custom Computer ---");
            
            var builder = _builder
                .SetCPU(cpu)
                .SetRAM(ram)
                .SetStorage(storage)
                .SetGraphicsCard(graphicsCard)
                .SetMotherboard(motherboard)
                .SetPowerSupply(powerSupply)
                .SetCase(computerCase);

            foreach (var accessory in accessories)
            {
                builder.AddAccessory(accessory);
            }

            return builder.GetComputer();
        }

        /// <summary>
        /// Demonstrates step-by-step construction
        /// </summary>
        public Computer BuildStepByStepComputer()
        {
            Console.WriteLine("\n--- Building Computer Step by Step ---");
            
            _builder.SetCPU("Intel Core i7-12700K");
            Console.WriteLine("Step 1: CPU installed");
            
            _builder.SetRAM("32GB DDR4");
            Console.WriteLine("Step 2: RAM installed");
            
            _builder.SetStorage("1TB SSD");
            Console.WriteLine("Step 3: Storage installed");
            
            _builder.SetGraphicsCard("RTX 3070");
            Console.WriteLine("Step 4: Graphics card installed");
            
            _builder.SetMotherboard("Z690 ATX");
            Console.WriteLine("Step 5: Motherboard installed");
            
            _builder.SetPowerSupply("750W 80+ Gold");
            Console.WriteLine("Step 6: Power supply installed");
            
            _builder.SetCase("Mid Tower Black");
            Console.WriteLine("Step 7: Case assembled");
            
            _builder.AddAccessory("WiFi Card");
            _builder.AddAccessory("Bluetooth Adapter");
            Console.WriteLine("Step 8: Accessories added");
            
            var computer = _builder.GetComputer();
            Console.WriteLine("Step 9: Final quality check completed");
            
            return computer;
        }
    }
}