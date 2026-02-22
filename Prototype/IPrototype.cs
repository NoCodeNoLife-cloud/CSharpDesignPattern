namespace Prototype
{
    /// <summary>
    /// Abstract Prototype Interface
    /// Defines cloning methods
    /// </summary>
    public interface IPrototype<T>
    {
        /// <summary>
        /// Shallow clone method
        /// </summary>
        T Clone();
        
        /// <summary>
        /// Deep clone method
        /// </summary>
        T DeepClone();
    }
}
