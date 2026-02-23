namespace Strategy.Strategies
{
    /// <summary>
    /// Sorting strategy interface
    /// Defines the contract for different sorting algorithms
    /// </summary>
    public interface ISortingStrategy<T> where T : IComparable<T>
    {
        /// <summary>
        /// Sorts the given collection
        /// </summary>
        List<T> Sort(List<T> data);
        
        /// <summary>
        /// Gets the strategy name
        /// </summary>
        string GetName();
        
        /// <summary>
        /// Gets the time complexity description
        /// </summary>
        string GetTimeComplexity();
    }
}