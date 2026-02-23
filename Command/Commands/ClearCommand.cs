using Command.Receivers;

namespace Command.Commands
{
    /// <summary>
    /// Clear editor command implementation
    /// Clears all content from the editor
    /// </summary>
    public class ClearCommand : ICommand
    {
        private readonly TextEditor _editor;
        private string _previousContent = string.Empty;

        public ClearCommand(TextEditor editor)
        {
            _editor = editor;
        }

        public void Execute()
        {
            _previousContent = _editor.Content;
            _editor.Clear();
        }

        public void Undo()
        {
            if (!string.IsNullOrEmpty(_previousContent))
            {
                _editor.Clear();
                _editor.InsertText(_previousContent, 0);
            }
        }

        public string GetDescription()
        {
            return "Clear all content";
        }
    }
}