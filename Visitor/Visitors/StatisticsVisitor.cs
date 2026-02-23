using Visitor.Elements;

namespace Visitor.Visitors
{
    /// <summary>
    /// Statistics visitor implementation
    /// Collects various statistics about document elements
    /// </summary>
    public class StatisticsVisitor : IDocumentVisitor
    {
        // Document statistics
        public int DocumentCount { get; private set; }
        public int TextElementCount { get; private set; }
        public int ImageElementCount { get; private set; }
        public int TableElementCount { get; private set; }
        
        // Text statistics
        public int TotalWords { get; private set; }
        public int TotalCharacters { get; private set; }
        
        // Image statistics
        public double TotalImageSize { get; private set; }
        public double AverageImageSize { get; private set; }
        
        // Table statistics
        public int TotalRows { get; private set; }
        public int TotalColumns { get; private set; }
        public int TotalCells { get; private set; }

        public void VisitDocument(Document document)
        {
            DocumentCount++;
            Console.WriteLine($"Analyzing document: {document.Title}");
        }

        public void VisitTextElement(TextElement textElement)
        {
            TextElementCount++;
            TotalWords += textElement.GetWordCount();
            TotalCharacters += textElement.GetCharacterCount();
            Console.WriteLine($"Text element analyzed: {textElement.GetWordCount()} words, {textElement.GetCharacterCount()} characters");
        }

        public void VisitImageElement(ImageElement imageElement)
        {
            ImageElementCount++;
            TotalImageSize += imageElement.FileSize;
            Console.WriteLine($"Image element analyzed: {imageElement.FileSize:F1}KB, {imageElement.GetDimensions()}");
        }

        public void VisitTableElement(TableElement tableElement)
        {
            TableElementCount++;
            TotalRows += tableElement.GetRowCount();
            TotalColumns += tableElement.GetColumnCount();
            TotalCells += tableElement.GetCellCount();
            Console.WriteLine($"Table element analyzed: {tableElement.GetRowCount()} rows, {tableElement.GetColumnCount()} columns");
        }

        public void Reset()
        {
            DocumentCount = 0;
            TextElementCount = 0;
            ImageElementCount = 0;
            TableElementCount = 0;
            TotalWords = 0;
            TotalCharacters = 0;
            TotalImageSize = 0;
            TotalRows = 0;
            TotalColumns = 0;
            TotalCells = 0;
        }

        public DocumentStatistics GetStatistics()
        {
            return new DocumentStatistics
            {
                DocumentCount = DocumentCount,
                TextElementCount = TextElementCount,
                ImageElementCount = ImageElementCount,
                TableElementCount = TableElementCount,
                TotalWords = TotalWords,
                TotalCharacters = TotalCharacters,
                TotalImageSize = TotalImageSize,
                AverageImageSize = ImageElementCount > 0 ? TotalImageSize / ImageElementCount : 0,
                TotalRows = TotalRows,
                TotalColumns = TotalColumns,
                TotalCells = TotalCells
            };
        }

        public void DisplayStatistics()
        {
            var stats = GetStatistics();
            Console.WriteLine("\n=== Document Statistics ===");
            Console.WriteLine($"Documents: {stats.DocumentCount}");
            Console.WriteLine($"Text Elements: {stats.TextElementCount}");
            Console.WriteLine($"Image Elements: {stats.ImageElementCount}");
            Console.WriteLine($"Table Elements: {stats.TableElementCount}");
            Console.WriteLine($"Total Words: {stats.TotalWords}");
            Console.WriteLine($"Total Characters: {stats.TotalCharacters}");
            Console.WriteLine($"Total Image Size: {stats.TotalImageSize:F1}KB");
            Console.WriteLine($"Average Image Size: {stats.AverageImageSize:F1}KB");
            Console.WriteLine($"Total Table Rows: {stats.TotalRows}");
            Console.WriteLine($"Total Table Columns: {stats.TotalColumns}");
            Console.WriteLine($"Total Table Cells: {stats.TotalCells}");
            Console.WriteLine("===========================\n");
        }
    }

    /// <summary>
    /// Document statistics data structure
    /// </summary>
    public class DocumentStatistics
    {
        public int DocumentCount { get; set; }
        public int TextElementCount { get; set; }
        public int ImageElementCount { get; set; }
        public int TableElementCount { get; set; }
        public int TotalWords { get; set; }
        public int TotalCharacters { get; set; }
        public double TotalImageSize { get; set; }
        public double AverageImageSize { get; set; }
        public int TotalRows { get; set; }
        public int TotalColumns { get; set; }
        public int TotalCells { get; set; }
    }
}