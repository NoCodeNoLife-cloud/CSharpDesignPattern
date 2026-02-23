namespace Iterator.Pattern
{
    /// <summary>
    /// Book aggregate implementation
    /// Represents a collection of books that can be iterated
    /// </summary>
    public class BookCollection : IAggregate<Book>
    {
        private readonly List<Book> _books;

        public BookCollection()
        {
            _books = new List<Book>();
        }

        public BookCollection(IEnumerable<Book> books)
        {
            _books = new List<Book>(books);
        }

        public IIterator<Book> CreateIterator()
        {
            return new ForwardIterator<Book>(this);
        }

        public IIterator<Book> CreateReverseIterator()
        {
            return new ReverseIterator<Book>(this);
        }

        public int Count => _books.Count;

        public bool IsEmpty() => _books.Count == 0;

        public Book GetElement(int index)
        {
            if (index < 0 || index >= _books.Count)
            {
                throw new IndexOutOfRangeException($"Index {index} is out of range [0, {_books.Count - 1}]");
            }
            return _books[index];
        }

        public void Add(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
                
            _books.Add(book);
            Console.WriteLine($"[BookCollection] Added book: {book.Title} by {book.Author}");
        }

        public bool Remove(Book book)
        {
            if (book == null)
                return false;
                
            var removed = _books.Remove(book);
            if (removed)
            {
                Console.WriteLine($"[BookCollection] Removed book: {book.Title} by {book.Author}");
            }
            return removed;
        }

        public void Clear()
        {
            var count = _books.Count;
            _books.Clear();
            Console.WriteLine($"[BookCollection] Cleared {count} books");
        }

        public List<Book> GetAllBooks()
        {
            return new List<Book>(_books);
        }

        public void SortByTitle()
        {
            _books.Sort((a, b) => string.Compare(a.Title, b.Title, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine("[BookCollection] Sorted books by title");
        }

        public void SortByAuthor()
        {
            _books.Sort((a, b) => string.Compare(a.Author, b.Author, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine("[BookCollection] Sorted books by author");
        }

        public void SortByPublicationYear()
        {
            _books.Sort((a, b) => a.PublicationYear.CompareTo(b.PublicationYear));
            Console.WriteLine("[BookCollection] Sorted books by publication year");
        }

        public List<Book> FindBooksByAuthor(string author)
        {
            return _books.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Book> FindBooksByYear(int year)
        {
            return _books.Where(b => b.PublicationYear == year).ToList();
        }

        public override string ToString()
        {
            return $"BookCollection[Count: {Count}]";
        }

        public void DisplayCollection()
        {
            Console.WriteLine($"\n=== Book Collection ({Count} books) ===");
            if (IsEmpty())
            {
                Console.WriteLine("  No books in collection");
            }
            else
            {
                var iterator = CreateIterator();
                while (iterator.MoveNext())
                {
                    var book = iterator.Current;
                    Console.WriteLine($"  [{iterator.Position + 1}] {book.Title} by {book.Author} ({book.PublicationYear})");
                }
            }
            Console.WriteLine(new string('=', 40));
        }
    }

    /// <summary>
    /// Book entity
    /// </summary>
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }

        public Book(string title, string author, int publicationYear, string isbn = "", string genre = "")
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            PublicationYear = publicationYear;
            ISBN = isbn;
            Genre = genre;
        }

        public override string ToString()
        {
            return $"{Title} by {Author} ({PublicationYear})";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Book other)
            {
                return ISBN.Equals(other.ISBN, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ISBN.GetHashCode(StringComparison.OrdinalIgnoreCase);
        }
    }
}