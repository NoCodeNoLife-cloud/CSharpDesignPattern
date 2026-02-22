using Builder.Products;

namespace Builder.Builders
{
    /// <summary>
    /// Fluent computer builder
    /// Provides method chaining for intuitive construction
    /// </summary>
    public class FluentComputerBuilder
    {
        private Computer _computer = new Computer();

        /// <summary>
        /// Static factory method for fluent interface
        /// </summary>
        public static FluentComputerBuilder Create() => new FluentComputerBuilder();

        public FluentComputerBuilder WithCPU(string cpu)
        {
            _computer.CPU = cpu;
            return this;
        }

        public FluentComputerBuilder WithRAM(string ram)
        {
            _computer.RAM = ram;
            return this;
        }

        public FluentComputerBuilder WithStorage(string storage)
        {
            _computer.Storage = storage;
            return this;
        }

        public FluentComputerBuilder WithGraphicsCard(string graphicsCard)
        {
            _computer.GraphicsCard = graphicsCard;
            return this;
        }

        public FluentComputerBuilder WithMotherboard(string motherboard)
        {
            _computer.Motherboard = motherboard;
            return this;
        }

        public FluentComputerBuilder WithPowerSupply(string powerSupply)
        {
            _computer.PowerSupply = powerSupply;
            return this;
        }

        public FluentComputerBuilder WithCase(string computerCase)
        {
            _computer.Case = computerCase;
            return this;
        }

        public FluentComputerBuilder AddAccessory(string accessory)
        {
            _computer.Accessories.Add(accessory);
            return this;
        }

        public FluentComputerBuilder AddAccessories(params string[] accessories)
        {
            _computer.Accessories.AddRange(accessories);
            return this;
        }

        /// <summary>
        /// Builds and returns the computer
        /// </summary>
        public Computer Build()
        {
            ValidateComputer();
            return _computer;
        }

        /// <summary>
        /// Validates that required components are present
        /// </summary>
        private void ValidateComputer()
        {
            if (string.IsNullOrEmpty(_computer.CPU))
                throw new InvalidOperationException("CPU is required");
            
            if (string.IsNullOrEmpty(_computer.RAM))
                throw new InvalidOperationException("RAM is required");
            
            if (string.IsNullOrEmpty(_computer.Storage))
                throw new InvalidOperationException("Storage is required");
        }

        /// <summary>
        /// Resets builder for reuse
        /// </summary>
        public FluentComputerBuilder Reset()
        {
            _computer = new Computer();
            return this;
        }

        /// <summary>
        /// Implicit conversion to Computer
        /// </summary>
        public static implicit operator Computer(FluentComputerBuilder builder)
        {
            return builder.Build();
        }
    }
}