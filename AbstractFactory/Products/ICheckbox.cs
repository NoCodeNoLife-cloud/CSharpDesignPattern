namespace AbstractFactory.Products
{
    /// <summary>
    /// Checkbox interface - Abstract product
    /// Defines common checkbox behavior
    /// </summary>
    public interface ICheckbox
    {
        /// <summary>
        /// Gets or sets checkbox label
        /// </summary>
        string Label { get; set; }
        
        /// <summary>
        /// Gets or sets checked state
        /// </summary>
        bool IsChecked { get; set; }
        
        /// <summary>
        /// Gets checkbox width
        /// </summary>
        int Width { get; }
        
        /// <summary>
        /// Gets checkbox height
        /// </summary>
        int Height { get; }
        
        /// <summary>
        /// Renders the checkbox
        /// </summary>
        void Render();
        
        /// <summary>
        /// Toggles checkbox state
        /// </summary>
        void Toggle();
    }
}