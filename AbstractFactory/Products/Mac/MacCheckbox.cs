namespace AbstractFactory.Products.Mac
{
    /// <summary>
    /// Mac checkbox implementation
    /// Concrete product for Mac theme
    /// </summary>
    public class MacCheckbox : ICheckbox
    {
        public string Label { get; set; } = string.Empty;
        public bool IsChecked { get; set; }
        public int Width { get; set; } = 140;
        public int Height { get; set; } = 18;

        public MacCheckbox()
        {
        }

        public MacCheckbox(string label, bool isChecked = false)
        {
            Label = label;
            IsChecked = isChecked;
        }

        public void Render()
        {
            var status = IsChecked ? "☑" : "☐";
            Console.WriteLine($"[Mac Checkbox] {status} {Label} | Size: {Width}x{Height}px | Style: Circular checkbox");
        }

        public void Toggle()
        {
            IsChecked = !IsChecked;
            Console.WriteLine($"[Mac] Checkbox '{Label}' toggled to {(IsChecked ? "checked" : "unchecked")}");
        }
    }
}