namespace AbstractFactory.Products.Windows
{
    /// <summary>
    /// Windows textbox implementation
    /// Concrete product for Windows theme
    /// </summary>
    public class WindowsTextBox : ITextBox
    {
        public string Content { get; set; } = string.Empty;
        public int Width { get; set; } = 200;
        public int Height { get; set; } = 25;
        public string Placeholder { get; set; } = string.Empty;

        public WindowsTextBox()
        {
        }

        public WindowsTextBox(string placeholder)
        {
            Placeholder = placeholder;
        }

        public void Render()
        {
            Console.WriteLine($"[Windows TextBox] '{Content}' | Placeholder: '{Placeholder}' | Size: {Width}x{Height}px | Style: Light gray border");
        }

        public void Focus()
        {
            Console.WriteLine("[Windows] TextBox focused - Blue highlight activated");
        }
    }
}