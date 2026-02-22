using AbstractFactory.Products;
using AbstractFactory.Products.Windows;

namespace AbstractFactory.Factories
{
    /// <summary>
    /// Concrete factory for Windows theme
    /// Implements abstract factory for Windows UI components
    /// </summary>
    public class WindowsFactory : IUIFactory
    {
        public string FactoryName => "Windows UI Factory";

        public IButton CreateButton()
        {
            return new WindowsButton("OK");
        }

        public ITextBox CreateTextBox()
        {
            return new WindowsTextBox("Enter text here...");
        }

        public ICheckbox CreateCheckbox()
        {
            return new WindowsCheckbox("Accept terms", false);
        }
    }
}