using Visitor.Visitors;

namespace Visitor.Elements
{
    /// <summary>
    /// Document composite element
    /// Represents a collection of document elements
    /// </summary>
    public class Document : IDocumentElement
    {
        private readonly List<IDocumentElement> _elements = new List<IDocumentElement>();

        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<string> Tags { get; set; }

        public Document()
        {
            Title = "Untitled Document";
            Author = "Anonymous";
            CreatedDate = DateTime.Now;
            Tags = new List<string>();
        }

        public Document(string title, string author)
        {
            Title = title;
            Author = author;
            CreatedDate = DateTime.Now;
            Tags = new List<string>();
        }

        public void Accept(IDocumentVisitor visitor)
        {
            visitor.VisitDocument(this);
            
            // Visit all child elements
            foreach (var element in _elements)
            {
                element.Accept(visitor);
            }
        }

        public void AddElement(IDocumentElement element)
        {
            _elements.Add(element);
        }

        public void RemoveElement(IDocumentElement element)
        {
            _elements.Remove(element);
        }

        public List<IDocumentElement> GetElements()
        {
            return _elements.ToList();
        }

        public int ElementCount => _elements.Count;

        public List<T> GetElementsByType<T>() where T : IDocumentElement
        {
            return _elements.OfType<T>().ToList();
        }

        public int GetElementCountByType<T>() where T : IDocumentElement
        {
            return _elements.OfType<T>().Count();
        }

        public void Clear()
        {
            _elements.Clear();
        }

        public override string ToString()
        {
            return $"Document: {Title} by {Author} [{ElementCount} elements, Created: {CreatedDate:yyyy-MM-dd}]";
        }
    }
}