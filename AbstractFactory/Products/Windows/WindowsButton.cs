namespace AbstractFactory.Products.Windows
{
    /// <summary>
    /// Windows button implementation
    /// Concrete product for Windows theme
    /// </summary>
    public class WindowsButton : IButton
    {
        public string Text { get; set; } = string.Empty;
        public int Width { get; set; } = 120;
        public int Height { get; set; } = 30;

        public WindowsButton()
        {
        }

        public WindowsButton(string text)
        {
            Text = text;
        }

        public void Render()
        {
            Console.WriteLine($"[Windows Button] '{Text}' | Size: {Width}x{Height}px | Style: Blue accent");
        }

        public void Click()
        {
            Console.WriteLine($"[Windows] Button '{Text}' clicked!");
        }
    }
}