using Command.Receivers;

namespace Command.Commands
{
    /// <summary>
    /// Replace text command implementation
    /// Replaces text at specified position
    /// </summary>
    public class ReplaceTextCommand : ICommand
    {
        private readonly TextEditor _editor;
        private readonly string _newText;
        private readonly int _startPosition;
        private readonly int _length;
        private string _oldText = string.Empty;

        public ReplaceTextCommand(TextEditor editor, string newText, int startPosition, int length)
        {
            _editor = editor;
            _newText = newText;
            _startPosition = startPosition;
            _length = length;
        }

        public void Execute()
        {
            // Store the text that will be replaced for undo operation
            if (_startPosition >= 0 && _startPosition < _editor.GetLength())
            {
                var actualLength = Math.Min(_length, _editor.GetLength() - _startPosition);
                _oldText = _editor.Content.Substring(_startPosition, actualLength);
            }
            _editor.ReplaceText(_newText, _startPosition, _length);
        }

        public void Undo()
        {
            if (!string.IsNullOrEmpty(_oldText))
            {
                _editor.ReplaceText(_oldText, _startPosition, _newText.Length);
            }
        }

        public string GetDescription()
        {
            return $"Replace text at position {_startPosition} with '{_newText}'";
        }
    }
}