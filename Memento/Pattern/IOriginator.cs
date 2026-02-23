namespace Memento.Pattern
{
    /// <summary>
    /// Originator interface
    /// Defines the contract for objects that can save and restore their state
    /// </summary>
    public interface IOriginator
    {
        /// <summary>
        /// Saves current state to a memento
        /// </summary>
        IMemento SaveState(string name = "");
        
        /// <summary>
        /// Restores state from a memento
        /// </summary>
        void RestoreState(IMemento memento);
        
        /// <summary>
        /// Gets current state information
        /// </summary>
        string GetCurrentStateInfo();
    }
}