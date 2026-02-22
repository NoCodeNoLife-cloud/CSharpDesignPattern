using AbstractFactory.Products;

namespace AbstractFactory.Factories
{
    /// <summary>
    /// Abstract factory interface
    /// Declares methods for creating abstract products
    /// </summary>
    public interface IUIFactory
    {
        /// <summary>
        /// Creates button product
        /// </summary>
        IButton CreateButton();
        
        /// <summary>
        /// Creates textbox product
        /// </summary>
        ITextBox CreateTextBox();
        
        /// <summary>
        /// Creates checkbox product
        /// </summary>
        ICheckbox CreateCheckbox();
        
        /// <summary>
        /// Gets factory name
        /// </summary>
        string FactoryName { get; }
    }
}