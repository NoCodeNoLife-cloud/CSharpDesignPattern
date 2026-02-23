using Visitor.Elements;

namespace Visitor.Visitors
{
    /// <summary>
    /// Document visitor interface
    /// Defines visit methods for different document elements
    /// </summary>
    public interface IDocumentVisitor
    {
        /// <summary>
        /// Visits a document element
        /// </summary>
        void VisitDocument(Document document);
        
        /// <summary>
        /// Visits a text element
        /// </summary>
        void VisitTextElement(TextElement textElement);
        
        /// <summary>
        /// Visits an image element
        /// </summary>
        void VisitImageElement(ImageElement imageElement);
        
        /// <summary>
        /// Visits a table element
        /// </summary>
        void VisitTableElement(TableElement tableElement);
    }
}