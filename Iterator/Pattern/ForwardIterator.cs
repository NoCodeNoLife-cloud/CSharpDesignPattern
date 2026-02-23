namespace Iterator.Pattern
{
    /// <summary>
    /// Forward iterator implementation
    /// Traverses collection from beginning to end
    /// </summary>
    /// <typeparam name="T">Type of elements to iterate</typeparam>
    public class ForwardIterator<T> : IIterator<T>
    {
        private readonly IAggregate<T> _aggregate;
        private int _currentIndex;

        public ForwardIterator(IAggregate<T> aggregate)
        {
            _aggregate = aggregate ?? throw new ArgumentNullException(nameof(aggregate));
            _currentIndex = -1; // Start before first element
        }

        public T Current
        {
            get
            {
                if (_currentIndex < 0 || _currentIndex >= _aggregate.Count)
                {
                    throw new InvalidOperationException("Iterator is not positioned on an element");
                }
                return _aggregate.GetElement(_currentIndex);
            }
        }

        public bool MoveNext()
        {
            if (_currentIndex < _aggregate.Count - 1)
            {
                _currentIndex++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _currentIndex = -1;
        }

        public bool HasNext()
        {
            return _currentIndex < _aggregate.Count - 1;
        }

        public int Position => _currentIndex;

        public bool IsFirst()
        {
            return _currentIndex == 0;
        }

        public bool IsLast()
        {
            return _currentIndex == _aggregate.Count - 1;
        }

        public override string ToString()
        {
            return $"ForwardIterator[Position: {_currentIndex}, Count: {_aggregate.Count}]";
        }

        /// <summary>
        /// Skips specified number of elements
        /// </summary>
        public bool Skip(int count)
        {
            if (count <= 0)
                return true;

            var newPosition = _currentIndex + count;
            if (newPosition < _aggregate.Count)
            {
                _currentIndex = newPosition;
                return true;
            }
            
            _currentIndex = _aggregate.Count - 1;
            return false;
        }

        /// <summary>
        /// Moves to specific position
        /// </summary>
        public bool MoveTo(int position)
        {
            if (position >= 0 && position < _aggregate.Count)
            {
                _currentIndex = position;
                return true;
            }
            return false;
        }
    }
}