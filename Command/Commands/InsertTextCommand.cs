using Command.Receivers;

namespace Command.Commands
{
    /// <summary>
    /// Insert text command implementation
    /// Inserts text at specified position
    /// </summary>
    public class InsertTextCommand : ICommand
    {
        private readonly TextEditor _editor;
        private readonly string _text;
        private readonly int _position;

        public InsertTextCommand(TextEditor editor, string text, int position)
        {
            _editor = editor;
            _text = text;
            _position = position;
        }

        public void Execute()
        {
            _editor.InsertText(_text, _position);
        }

        public void Undo()
        {
            _editor.DeleteText(_position, _text.Length);
        }

        public string GetDescription()
        {
            return $"Insert '{_text}' at position {_position}";
        }
    }
}