namespace Composite.Components
{
    /// <summary>
    /// FileSystem component interface
    /// Defines common operations for both files and folders
    /// </summary>
    public interface IFileSystemComponent
    {
        /// <summary>
        /// Gets the name of the file system component
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets the size of the component in bytes
        /// </summary>
        long Size { get; }
        
        /// <summary>
        /// Gets the full path of the component
        /// </summary>
        string Path { get; }
        
        /// <summary>
        /// Displays the component information with specified indentation
        /// </summary>
        void Display(int depth = 0);
        
        /// <summary>
        /// Searches for components matching the specified criteria
        /// </summary>
        IEnumerable<IFileSystemComponent> Search(Func<IFileSystemComponent, bool> predicate);
        
        /// <summary>
        /// Gets the parent component (null for root)
        /// </summary>
        IFileSystemComponent? Parent { get; set; }
    }
}