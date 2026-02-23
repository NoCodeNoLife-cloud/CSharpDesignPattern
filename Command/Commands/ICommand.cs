namespace Command.Commands
{
    /// <summary>
    /// Command interface
    /// Defines the contract for all command objects
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the command
        /// </summary>
        void Execute();
        
        /// <summary>
        /// Undoes the command execution
        /// </summary>
        void Undo();
        
        /// <summary>
        /// Gets the command description
        /// </summary>
        string GetDescription();
    }
}