using Command.Receivers;

namespace Command.Commands
{
    /// <summary>
    /// Upper case conversion command implementation
    /// Converts text to upper case
    /// </summary>
    public class UpperCaseCommand : ICommand
    {
        private readonly TextEditor _editor;
        private string _previousContent = string.Empty;

        public UpperCaseCommand(TextEditor editor)
        {
            _editor = editor;
        }

        public void Execute()
        {
            _previousContent = _editor.Content;
            _editor.UpperCase();
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
            return "Convert to uppercase";
        }
    }

    /// <summary>
    /// Lower case conversion command implementation
    /// Converts text to lower case
    /// </summary>
    public class LowerCaseCommand : ICommand
    {
        private readonly TextEditor _editor;
        private string _previousContent = string.Empty;

        public LowerCaseCommand(TextEditor editor)
        {
            _editor = editor;
        }

        public void Execute()
        {
            _previousContent = _editor.Content;
            _editor.LowerCase();
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
            return "Convert to lowercase";
        }
    }
}