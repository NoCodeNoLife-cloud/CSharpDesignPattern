namespace Memento.Pattern
{
    /// <summary>
    /// Document editor originator
    /// Maintains document state and can save/restore checkpoints
    /// </summary>
    public class DocumentEditor : IOriginator
    {
        private string _content;
        private string _fileName;
        private DateTime _lastModified;
        private int _cursorPosition;
        private readonly List<string> _changeHistory;

        public DocumentEditor(string fileName = "Untitled")
        {
            _fileName = fileName;
            _content = string.Empty;
            _lastModified = DateTime.Now;
            _cursorPosition = 0;
            _changeHistory = new List<string>();
            Console.WriteLine($"[DocumentEditor] Created new document: {_fileName}");
        }

        public DocumentEditor(string fileName, string initialContent) : this(fileName)
        {
            _content = initialContent;
            _lastModified = DateTime.Now;
            _cursorPosition = initialContent.Length;
            _changeHistory.Add($"Document created with {initialContent.Length} characters");
        }

        public IMemento SaveState(string name = "")
        {
            var documentState = new DocumentState
            {
                Content = _content,
                FileName = _fileName,
                LastModified = _lastModified,
                CursorPosition = _cursorPosition,
                ChangeHistory = new List<string>(_changeHistory)
            };

            var mementoName = string.IsNullOrEmpty(name) ? $"Edit_{_changeHistory.Count}" : name;
            var memento = new Memento(documentState, mementoName);
            
            Console.WriteLine($"[DocumentEditor] State saved: {mementoName}");
            Console.WriteLine($"  Content length: {_content.Length} chars");
            Console.WriteLine($"  Changes: {_changeHistory.Count} operations");
            
            return memento;
        }

        public void RestoreState(IMemento memento)
        {
            if (memento.GetState() is not DocumentState state)
            {
                throw new ArgumentException("Invalid memento state type");
            }

            _content = state.Content;
            _fileName = state.FileName;
            _lastModified = state.LastModified;
            _cursorPosition = state.CursorPosition;
            _changeHistory.Clear();
            _changeHistory.AddRange(state.ChangeHistory);

            Console.WriteLine($"[DocumentEditor] State restored from: {memento.GetName()}");
            Console.WriteLine($"  Content: \"{TruncateContent(_content, 50)}\"");
            Console.WriteLine($"  File: {_fileName}");
            Console.WriteLine($"  Changes restored: {_changeHistory.Count} operations");
        }

        public string GetCurrentStateInfo()
        {
            return $"Document: {_fileName} | " +
                   $"Length: {_content.Length} chars | " +
                   $"Changes: {_changeHistory.Count} | " +
                   $"Last modified: {_lastModified:yyyy-MM-dd HH:mm:ss}";
        }

        // Document editing operations
        public void InsertText(string text, int position = -1)
        {
            if (position == -1)
                position = _cursorPosition;

            if (position < 0 || position > _content.Length)
                throw new ArgumentOutOfRangeException(nameof(position));

            _content = _content.Insert(position, text);
            _cursorPosition = position + text.Length;
            _lastModified = DateTime.Now;
            
            var changeMsg = $"Inserted '{TruncateContent(text, 20)}' at position {position}";
            _changeHistory.Add(changeMsg);
            
            Console.WriteLine($"[DocumentEditor] {changeMsg}");
        }

        public void DeleteText(int startPosition, int length)
        {
            if (startPosition < 0 || startPosition >= _content.Length)
                throw new ArgumentOutOfRangeException(nameof(startPosition));
                
            if (length <= 0 || startPosition + length > _content.Length)
                throw new ArgumentOutOfRangeException(nameof(length));

            var deletedText = _content.Substring(startPosition, length);
            _content = _content.Remove(startPosition, length);
            _cursorPosition = startPosition;
            _lastModified = DateTime.Now;
            
            var changeMsg = $"Deleted '{TruncateContent(deletedText, 20)}' ({length} chars)";
            _changeHistory.Add(changeMsg);
            
            Console.WriteLine($"[DocumentEditor] {changeMsg}");
        }

        public void ReplaceText(string newText, int startPosition, int length)
        {
            DeleteText(startPosition, length);
            InsertText(newText, startPosition);
        }

        public void SetFileName(string fileName)
        {
            var oldName = _fileName;
            _fileName = fileName;
            _lastModified = DateTime.Now;
            
            var changeMsg = $"Renamed from '{oldName}' to '{fileName}'";
            _changeHistory.Add(changeMsg);
            
            Console.WriteLine($"[DocumentEditor] {changeMsg}");
        }

        // Getters
        public string GetContent() => _content;
        public string GetFileName() => _fileName;
        public DateTime GetLastModified() => _lastModified;
        public int GetCursorPosition() => _cursorPosition;
        public List<string> GetChangeHistory() => new List<string>(_changeHistory);
        public int GetContentLength() => _content.Length;

        public void DisplayDocument()
        {
            Console.WriteLine($"\n=== Document: {_fileName} ===");
            Console.WriteLine($"Content ({_content.Length} chars):");
            Console.WriteLine(_content);
            Console.WriteLine($"\nLast modified: {_lastModified:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Cursor position: {_cursorPosition}");
            Console.WriteLine($"Change history ({_changeHistory.Count} operations):");
            foreach (var change in _changeHistory.Take(5)) // Show last 5 changes
            {
                Console.WriteLine($"  • {change}");
            }
            if (_changeHistory.Count > 5)
            {
                Console.WriteLine($"  ... and {_changeHistory.Count - 5} more changes");
            }
            Console.WriteLine(new string('=', 40));
        }

        private string TruncateContent(string content, int maxLength)
        {
            if (string.IsNullOrEmpty(content))
                return string.Empty;
                
            return content.Length <= maxLength ? content : content.Substring(0, maxLength) + "...";
        }

        // Document state class for serialization
        private class DocumentState
        {
            public string Content { get; set; } = string.Empty;
            public string FileName { get; set; } = string.Empty;
            public DateTime LastModified { get; set; }
            public int CursorPosition { get; set; }
            public List<string> ChangeHistory { get; set; } = new List<string>();

            public override string ToString()
            {
                return $"DocumentState[File: {FileName}, Length: {Content.Length}, Changes: {ChangeHistory.Count}]";
            }
        }
    }
}