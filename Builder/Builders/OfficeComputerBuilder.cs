using Builder.Products;

namespace Builder.Builders
{
    /// <summary>
    /// Office computer builder
    /// Implements builder for business/office computers
    /// </summary>
    public class OfficeComputerBuilder : IComputerBuilder
    {
        private Computer _computer = new Computer();

        public IComputerBuilder SetCPU(string cpu)
        {
            _computer.CPU = cpu;
            Console.WriteLine($"Setting office CPU: {cpu}");
            return this;
        }

        public IComputerBuilder SetRAM(string ram)
        {
            _computer.RAM = ram;
            Console.WriteLine($"Setting office RAM: {ram}");
            return this;
        }

        public IComputerBuilder SetStorage(string storage)
        {
            _computer.Storage = storage;
            Console.WriteLine($"Setting office storage: {storage}");
            return this;
        }

        public IComputerBuilder SetGraphicsCard(string graphicsCard)
        {
            _computer.GraphicsCard = graphicsCard;
            Console.WriteLine($"Setting office graphics card: {graphicsCard}");
            return this;
        }

        public IComputerBuilder SetMotherboard(string motherboard)
        {
            _computer.Motherboard = motherboard;
            Console.WriteLine($"Setting office motherboard: {motherboard}");
            return this;
        }

        public IComputerBuilder SetPowerSupply(string powerSupply)
        {
            _computer.PowerSupply = powerSupply;
            Console.WriteLine($"Setting office power supply: {powerSupply}");
            return this;
        }

        public IComputerBuilder SetCase(string computerCase)
        {
            _computer.Case = computerCase;
            Console.WriteLine($"Setting office case: {computerCase}");
            return this;
        }

        public IComputerBuilder AddAccessory(string accessory)
        {
            _computer.Accessories.Add(accessory);
            Console.WriteLine($"Adding office accessory: {accessory}");
            return this;
        }

        public Computer GetComputer()
        {
            Console.WriteLine("Office computer construction completed!");
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