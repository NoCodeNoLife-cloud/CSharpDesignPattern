using AbstractFactory.Factories;
using AbstractFactory.Products;

namespace AbstractFactory.Application
{
    /// <summary>
    /// Client application class
    /// Uses abstract factory to create consistent UI components
    /// </summary>
    public class UIApplication
    {
        private readonly IUIFactory _factory;
        private IButton _button;
        private ITextBox _textBox;
        private ICheckbox _checkbox;

        public UIApplication(IUIFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Creates all UI components using the factory
        /// </summary>
        public void CreateUI()
        {
            Console.WriteLine($"\n--- Creating UI with {_factory.FactoryName} ---");
            
            _button = _factory.CreateButton();
            _textBox = _factory.CreateTextBox();
            _checkbox = _factory.CreateCheckbox();
            
            Console.WriteLine("UI components created successfully!");
        }

        /// <summary>
        /// Renders all UI components
        /// </summary>
        public void RenderUI()
        {
            Console.WriteLine($"\n--- Rendering UI ({_factory.FactoryName}) ---");
            
            _button.Render();
            _textBox.Render();
            _checkbox.Render();
        }

        /// <summary>
        /// Simulates user interactions
        /// </summary>
        public void SimulateUserActions()
        {
            Console.WriteLine($"\n--- Simulating User Actions ({_factory.FactoryName}) ---");
            
            _button.Click();
            _textBox.Focus();
            _checkbox.Toggle();
            _checkbox.Toggle();
        }

        /// <summary>
        /// Customizes button text
        /// </summary>
        public void SetButtonText(string text)
        {
            if (_button != null)
            {
                _button.Text = text;
                Console.WriteLine($"Button text changed to: '{text}'");
            }
        }

        /// <summary>
        /// Gets the current factory name
        /// </summary>
        public string GetFactoryName() => _factory.FactoryName;
    }
}