using Builder.Products;

namespace Builder.Builders
{
    /// <summary>
    /// Gaming computer builder
    /// Implements builder for high-performance gaming computers
    /// </summary>
    public class GamingComputerBuilder : IComputerBuilder
    {
        private Computer _computer = new Computer();

        public IComputerBuilder SetCPU(string cpu)
        {
            _computer.CPU = cpu;
            Console.WriteLine($"Setting gaming CPU: {cpu}");
            return this;
        }

        public IComputerBuilder SetRAM(string ram)
        {
            _computer.RAM = ram;
            Console.WriteLine($"Setting gaming RAM: {ram}");
            return this;
        }

        public IComputerBuilder SetStorage(string storage)
        {
            _computer.Storage = storage;
            Console.WriteLine($"Setting gaming storage: {storage}");
            return this;
        }

        public IComputerBuilder SetGraphicsCard(string graphicsCard)
        {
            _computer.GraphicsCard = graphicsCard;
            Console.WriteLine($"Setting gaming graphics card: {graphicsCard}");
            return this;
        }

        public IComputerBuilder SetMotherboard(string motherboard)
        {
            _computer.Motherboard = motherboard;
            Console.WriteLine($"Setting gaming motherboard: {motherboard}");
            return this;
        }

        public IComputerBuilder SetPowerSupply(string powerSupply)
        {
            _computer.PowerSupply = powerSupply;
            Console.WriteLine($"Setting gaming power supply: {powerSupply}");
            return this;
        }

        public IComputerBuilder SetCase(string computerCase)
        {
            _computer.Case = computerCase;
            Console.WriteLine($"Setting gaming case: {computerCase}");
            return this;
        }

        public IComputerBuilder AddAccessory(string accessory)
        {
            _computer.Accessories.Add(accessory);
            Console.WriteLine($"Adding gaming accessory: {accessory}");
            return this;
        }

        public Computer GetComputer()
        {
            Console.WriteLine("Gaming computer construction completed!");
            return _computer;
        }

        /// <summary>
        /// Resets builder to create new computer
        /// </summary>
        public void Reset()
        {
            _computer = new Computer();
        }
    }
}