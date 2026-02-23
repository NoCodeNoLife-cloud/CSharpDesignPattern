namespace Memento.Pattern
{
    /// <summary>
    /// Memento interface
    /// Defines the contract for storing and restoring object state
    /// </summary>
    public interface IMemento
    {
        /// <summary>
        /// Gets the saved state
        /// </summary>
        object GetState();
        
        /// <summary>
        /// Gets the timestamp when state was saved
        /// </summary>
        DateTime GetTimestamp();
        
        /// <summary>
        /// Gets the name/description of this memento
        /// </summary>
        string GetName();
        
        /// <summary>
        /// Gets state summary for display purposes
        /// </summary>
        string GetStateSummary();
    }
}