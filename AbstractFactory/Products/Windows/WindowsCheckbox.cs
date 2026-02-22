namespace AbstractFactory.Products.Windows
{
    /// <summary>
    /// Windows checkbox implementation
    /// Concrete product for Windows theme
    /// </summary>
    public class WindowsCheckbox : ICheckbox
    {
        public string Label { get; set; } = string.Empty;
        public bool IsChecked { get; set; }
        public int Width { get; set; } = 150;
        public int Height { get; set; } = 20;

        public WindowsCheckbox()
        {
        }

        public WindowsCheckbox(string label, bool isChecked = false)
        {
            Label = label;
            IsChecked = isChecked;
        }

        public void Render()
        {
            var status = IsChecked ? "[✓]" : "[ ]";
            Console.WriteLine($"[Windows Checkbox] {status} {Label} | Size: {Width}x{Height}px | Style: Square box");
        }

        public void Toggle()
        {
            IsChecked = !IsChecked;
            Console.WriteLine($"[Windows] Checkbox '{Label}' toggled to {(IsChecked ? "checked" : "unchecked")}");
        }
    }
}