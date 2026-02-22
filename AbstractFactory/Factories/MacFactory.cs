using AbstractFactory.Products;
using AbstractFactory.Products.Mac;

namespace AbstractFactory.Factories
{
    /// <summary>
    /// Concrete factory for Mac theme
    /// Implements abstract factory for Mac UI components
    /// </summary>
    public class MacFactory : IUIFactory
    {
        public string FactoryName => "Mac UI Factory";

        public IButton CreateButton()
        {
            return new MacButton("Apply");
        }

        public ITextBox CreateTextBox()
        {
            return new MacTextBox("Type something...");
        }

        public ICheckbox CreateCheckbox()
        {
            return new MacCheckbox("Remember me", true);
        }
    }
}