using Command.Receivers;

namespace Command.Commands
{
    /// <summary>
    /// Delete text command implementation
    /// Deletes text from specified position
    /// </summary>
    public class DeleteTextCommand : ICommand
    {
        private readonly TextEditor _editor;
        private readonly int _startPosition;
        private readonly int _length;
        private string _deletedText = string.Empty;

        public DeleteTextCommand(TextEditor editor, int startPosition, int length)
        {
            _editor = editor;
            _startPosition = startPosition;
            _length = length;
        }

        public void Execute()
        {
            // Store the text that will be deleted for undo operation
            if (_startPosition >= 0 && _startPosition < _editor.GetLength())
            {
                var actualLength = Math.Min(_length, _editor.GetLength() - _startPosition);
                _deletedText = _editor.Content.Substring(_startPosition, actualLength);
            }
            _editor.DeleteText(_startPosition, _length);
        }

        public void Undo()
        {
            if (!string.IsNullOrEmpty(_deletedText))
            {
                _editor.InsertText(_deletedText, _startPosition);
            }
        }

        public string GetDescription()
        {
            return $"Delete {Math.Min(_length, _editor.GetLength() - _startPosition)} characters from position {_startPosition}";
        }
    }
}