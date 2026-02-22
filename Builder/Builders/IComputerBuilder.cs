using Builder.Products;

namespace Builder.Builders
{
    /// <summary>
    /// Computer builder interface
    /// Defines the steps for building a computer
    /// </summary>
    public interface IComputerBuilder
    {
        /// <summary>
        /// Builds CPU component
        /// </summary>
        IComputerBuilder SetCPU(string cpu);
        
        /// <summary>
        /// Builds RAM component
        /// </summary>
        IComputerBuilder SetRAM(string ram);
        
        /// <summary>
        /// Builds storage component
        /// </summary>
        IComputerBuilder SetStorage(string storage);
        
        /// <summary>
        /// Builds graphics card component
        /// </summary>
        IComputerBuilder SetGraphicsCard(string graphicsCard);
        
        /// <summary>
        /// Builds motherboard component
        /// </summary>
        IComputerBuilder SetMotherboard(string motherboard);
        
        /// <summary>
        /// Builds power supply component
        /// </summary>
        IComputerBuilder SetPowerSupply(string powerSupply);
        
        /// <summary>
        /// Builds case component
        /// </summary>
        IComputerBuilder SetCase(string computerCase);
        
        /// <summary>
        /// Adds accessory component
        /// </summary>
        IComputerBuilder AddAccessory(string accessory);
        
        /// <summary>
        /// Gets the constructed computer
        /// </summary>
        Computer GetComputer();
    }
}