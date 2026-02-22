namespace AbstractFactory.Products
{
    /// <summary>
    /// Button interface - Abstract product
    /// Defines common button behavior
    /// </summary>
    public interface IButton
    {
        /// <summary>
        /// Gets or sets button text
        /// </summary>
        string Text { get; set; }
        
        /// <summary>
        /// Gets button width
        /// </summary>
        int Width { get; }
        
        /// <summary>
        /// Gets button height
        /// </summary>
        int Height { get; }
        
        /// <summary>
        /// Renders the button
        /// </summary>
        void Render();
        
        /// <summary>
        /// Handles click event
        /// </summary>
        void Click();
    }
}