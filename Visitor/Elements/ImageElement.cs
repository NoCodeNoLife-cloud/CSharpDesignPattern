using Visitor.Visitors;

namespace Visitor.Elements
{
    /// <summary>
    /// Image element concrete implementation
    /// Represents image content in a document
    /// </summary>
    public class ImageElement : IDocumentElement
    {
        public string Source { get; set; }
        public string AltText { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public ImageFormat Format { get; set; }
        public double FileSize { get; set; } // in KB

        public ImageElement(string source, string altText)
        {
            Source = source;
            AltText = altText;
            Width = 400;
            Height = 300;
            Format = ImageFormat.JPEG;
            FileSize = 100.0;
        }

        public ImageElement(string source, string altText, int width, int height, ImageFormat format, double fileSize)
        {
            Source = source;
            AltText = altText;
            Width = width;
            Height = height;
            Format = format;
            FileSize = fileSize;
        }

        public void Accept(IDocumentVisitor visitor)
        {
            visitor.VisitImageElement(this);
        }

        public double GetAspectRatio()
        {
            return Height == 0 ? 0 : (double)Width / Height;
        }

        public string GetDimensions()
        {
            return $"{Width}×{Height} pixels";
        }

        public override string ToString()
        {
            return $"Image: {AltText} [{Source}, {GetDimensions()}, {Format}, {FileSize:F1}KB]";
        }
    }

    /// <summary>
    /// Image format enumeration
    /// </summary>
    public enum ImageFormat
    {
        JPEG,
        PNG,
        GIF,
        BMP,
        SVG,
        WEBP
    }
}