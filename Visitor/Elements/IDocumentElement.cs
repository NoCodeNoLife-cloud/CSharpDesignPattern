using Visitor.Visitors;

namespace Visitor.Elements
{
    /// <summary>
    /// Document element interface
    /// Defines the accept method for visitor pattern
    /// </summary>
    public interface IDocumentElement
    {
        /// <summary>
        /// Accepts a visitor to perform operations
        /// </summary>
        void Accept(IDocumentVisitor visitor);
    }
}