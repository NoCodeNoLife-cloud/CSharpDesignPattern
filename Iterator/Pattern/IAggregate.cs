namespace Iterator.Pattern
{
    /// <summary>
    /// Aggregate interface
    /// Defines the contract for collections that can be iterated
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection</typeparam>
    public interface IAggregate<T>
    {
        /// <summary>
        /// Creates an iterator for this collection
        /// </summary>
        IIterator<T> CreateIterator();
        
        /// <summary>
        /// Gets the number of elements in the collection
        /// </summary>
        int Count { get; }
        
        /// <summary>
        /// Checks if the collection is empty
        /// </summary>
        bool IsEmpty();
        
        /// <summary>
        /// Gets element at specified index
        /// </summary>
        T GetElement(int index);
        
        /// <summary>
        /// Adds an element to the collection
        /// </summary>
        void Add(T item);
        
        /// <summary>
        /// Removes an element from the collection
        /// </summary>
        bool Remove(T item);
        
        /// <summary>
        /// Clears all elements from the collection
        /// </summary>
        void Clear();
    }
}