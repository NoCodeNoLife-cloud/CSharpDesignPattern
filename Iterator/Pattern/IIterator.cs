namespace Iterator.Pattern
{
    /// <summary>
    /// Iterator interface
    /// Defines the contract for traversing collection elements
    /// </summary>
    /// <typeparam name="T">Type of elements to iterate</typeparam>
    public interface IIterator<T>
    {
        /// <summary>
        /// Gets the current element
        /// </summary>
        T Current { get; }
        
        /// <summary>
        /// Moves to the next element
        /// </summary>
        bool MoveNext();
        
        /// <summary>
        /// Resets the iterator to the beginning
        /// </summary>
        void Reset();
        
        /// <summary>
        /// Checks if there are more elements
        /// </summary>
        bool HasNext();
        
        /// <summary>
        /// Gets the current position
        /// </summary>
        int Position { get; }
        
        /// <summary>
        /// Checks if iterator is at the beginning
        /// </summary>
        bool IsFirst();
        
        /// <summary>
        /// Checks if iterator is at the end
        /// </summary>
        bool IsLast();
    }
}