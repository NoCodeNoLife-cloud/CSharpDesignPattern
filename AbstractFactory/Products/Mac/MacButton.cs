namespace AbstractFactory.Products.Mac
{
    /// <summary>
    /// Mac button implementation
    /// Concrete product for Mac theme
    /// </summary>
    public class MacButton : IButton
    {
        public string Text { get; set; } = string.Empty;
        public int Width { get; set; } = 100;
        public int Height { get; set; } = 28;

        public MacButton()
        {
        }

        public MacButton(string text)
        {
            Text = text;
        }

        public void Render()
        {
            Console.WriteLine($"[Mac Button] '{Text}' | Size: {Width}x{Height}px | Style: Rounded corners, gray background");
        }

        public void Click()
        {
            Console.WriteLine($"[Mac] Button '{Text}' clicked!");
        }
    }
}