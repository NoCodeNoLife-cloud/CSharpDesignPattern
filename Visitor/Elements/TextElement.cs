using Visitor.Visitors;
using Visitor.Elements;

namespace Visitor.Elements
{
    /// <summary>
    /// Text element concrete implementation
    /// Represents text content in a document
    /// </summary>
    public class TextElement : IDocumentElement
    {
        public string Content { get; set; } = string.Empty;
        public string FontFamily { get; set; } = string.Empty;
        public int FontSize { get; set; }
        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }

        public TextElement(string content)
        {
            Content = content;
            FontFamily = "Arial";
            FontSize = 12;
        }

        public TextElement(string content, string fontFamily, int fontSize)
        {
            Content = content;
            FontFamily = fontFamily;
            FontSize = fontSize;
        }

        public void Accept(IDocumentVisitor visitor)
        {
            visitor.VisitTextElement(this);
        }

        public int GetWordCount()
        {
            return string.IsNullOrWhiteSpace(Content) ? 0 : Content.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public int GetCharacterCount()
        {
            return Content?.Length ?? 0;
        }

        public override string ToString()
        {
            var style = "";
            if (IsBold) style += "Bold ";
            if (IsItalic) style += "Italic ";
            
            return $"Text: \"{Content}\" [{FontFamily}, {FontSize}pt{style.TrimEnd()}]";
        }
    }
}