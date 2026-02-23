using System.Text;

namespace Command.Receivers
{
    /// <summary>
    /// Text editor receiver
    /// Performs actual text editing operations
    /// </summary>
    public class TextEditor
    {
        private readonly StringBuilder _content = new StringBuilder();
        private readonly Stack<string> _history = new Stack<string>();

        public string Content => _content.ToString();

        public TextEditor()
        {
            SaveState(); // Save initial empty state
        }

        public TextEditor(string initialContent)
        {
            _content.Append(initialContent);
            SaveState();
        }

        public void InsertText(string text, int position)
        {
            SaveState();
            _content.Insert(position, text);
            Console.WriteLine($"[TextEditor] Inserted '{text}' at position {position}");
        }

        public void DeleteText(int startPosition, int length)
        {
            if (startPosition >= 0 && startPosition < _content.Length && length > 0)
            {
                SaveState();
                var deletedText = _content.ToString().Substring(startPosition, Math.Min(length, _content.Length - startPosition));
                _content.Remove(startPosition, Math.Min(length, _content.Length - startPosition));
                Console.WriteLine($"[TextEditor] Deleted '{deletedText}' from position {startPosition}");
            }
        }

        public void ReplaceText(string newText, int startPosition, int length)
        {
            SaveState();
            var oldText = _content.ToString().Substring(startPosition, Math.Min(length, _content.Length - startPosition));
            _content.Remove(startPosition, Math.Min(length, _content.Length - startPosition));
            _content.Insert(startPosition, newText);
            Console.WriteLine($"[TextEditor] Replaced '{oldText}' with '{newText}' at position {startPosition}");
        }

        public void Clear()
        {
            SaveState();
            _content.Clear();
            Console.WriteLine("[TextEditor] Content cleared");
        }

        public void UpperCase()
        {
            SaveState();
            var oldContent = _content.ToString();
            _content.Clear();
            _content.Append(oldContent.ToUpper());
            Console.WriteLine("[TextEditor] Converted to uppercase");
        }

        public void LowerCase()
        {
            SaveState();
            var oldContent = _content.ToString();
            _content.Clear();
            _content.Append(oldContent.ToLower());
            Console.WriteLine("[TextEditor] Converted to lowercase");
        }

        public bool CanUndo()
        {
            return _history.Count > 1; // Keep at least initial state
        }

        public void Undo()
        {
            if (CanUndo())
            {
                _history.Pop(); // Remove current state
                var previousState = _history.Peek();
                _content.Clear();
                _content.Append(previousState);
                Console.WriteLine("[TextEditor] Undo performed");
            }
        }

        private void SaveState()
        {
            _history.Push(_content.ToString());
            // Limit history size to prevent memory issues
            if (_history.Count > 50)
            {
                var tempStack = new Stack<string>();
                for (int i = 0; i < 25; i++)
                {
                    tempStack.Push(_history.Pop());
                }
                _history.Clear();
                while (tempStack.Count > 0)
                {
                    _history.Push(tempStack.Pop());
                }
            }
        }

        public int GetLength()
        {
            return _content.Length;
        }

        public override string ToString()
        {
            return _content.ToString();
        }
    }
}