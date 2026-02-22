namespace AbstractFactory.Products
{
    /// <summary>
    /// TextBox interface - Abstract product
    /// Defines common textbox behavior
    /// </summary>
    public interface ITextBox
    {
        /// <summary>
        /// Gets or sets textbox content
        /// </summary>
        string Content { get; set; }
        
        /// <summary>
        /// Gets textbox width
        /// </summary>
        int Width { get; }
        
        /// <summary>
        /// Gets textbox height
        /// </summary>
        int Height { get; }
        
        /// <summary>
        /// Gets or sets placeholder text
        /// </summary>
        string Placeholder { get; set; }
        
        /// <summary>
        /// Renders the textbox
        /// </summary>
        void Render();
        
        /// <summary>
        /// Sets focus to textbox
        /// </summary>
        void Focus();
    }
}