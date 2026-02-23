namespace Iterator.Pattern
{
    /// <summary>
    /// Reverse iterator implementation
    /// Traverses collection from end to beginning
    /// </summary>
    /// <typeparam name="T">Type of elements to iterate</typeparam>
    public class ReverseIterator<T> : IIterator<T>
    {
        private readonly IAggregate<T> _aggregate;
        private int _currentIndex;

        public ReverseIterator(IAggregate<T> aggregate)
        {
            _aggregate = aggregate ?? throw new ArgumentNullException(nameof(aggregate));
            _currentIndex = _aggregate.Count; // Start after last element
        }

        public T Current
        {
            get
            {
                if (_currentIndex <= 0 || _currentIndex > _aggregate.Count)
                {
                    throw new InvalidOperationException("Iterator is not positioned on an element");
                }
                return _aggregate.GetElement(_currentIndex - 1);
            }
        }

        public bool MoveNext()
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _currentIndex = _aggregate.Count;
        }

        public bool HasNext()
        {
            return _currentIndex > 0;
        }

        public int Position => _currentIndex - 1;

        public bool IsFirst()
        {
            return _currentIndex == _aggregate.Count;
        }

        public bool IsLast()
        {
            return _currentIndex == 1;
        }

        public override string ToString()
        {
            return $"ReverseIterator[Position: {_currentIndex - 1}, Count: {_aggregate.Count}]";
        }

        /// <summary>
        /// Skips specified number of elements in reverse direction
        /// </summary>
        public bool Skip(int count)
        {
            if (count <= 0)
                return true;

            var newPosition = _currentIndex - count;
            if (newPosition > 0)
            {
                _currentIndex = newPosition;
                return true;
            }
            
            _currentIndex = 1;
            return false;
        }

        /// <summary>
        /// Moves to specific position (0-based index)
        /// </summary>
        public bool MoveTo(int position)
        {
            if (position >= 0 && position < _aggregate.Count)
            {
                _currentIndex = position + 1;
                return true;
            }
            return false;
        }
    }
}