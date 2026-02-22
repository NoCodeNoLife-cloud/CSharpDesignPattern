namespace AbstractFactory.Products.Mac
{
    /// <summary>
    /// Mac textbox implementation
    /// Concrete product for Mac theme
    /// </summary>
    public class MacTextBox : ITextBox
    {
        public string Content { get; set; } = string.Empty;
        public int Width { get; set; } = 180;
        public int Height { get; set; } = 22;
        public string Placeholder { get; set; } = string.Empty;

        public MacTextBox()
        {
        }

        public MacTextBox(string placeholder)
        {
            Placeholder = placeholder;
        }

        public void Render()
        {
            Console.WriteLine($"[Mac TextBox] '{Content}' | Placeholder: '{Placeholder}' | Size: {Width}x{Height}px | Style: Rounded borders, subtle shadow");
        }

        public void Focus()
        {
            Console.WriteLine("[Mac] TextBox focused - Blue ring activated");
        }
    }
}