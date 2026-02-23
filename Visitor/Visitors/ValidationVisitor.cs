using Visitor.Elements;

namespace Visitor.Visitors
{
    /// <summary>
    /// Validation visitor implementation
    /// Validates document elements for quality and compliance
    /// </summary>
    public class ValidationVisitor : IDocumentVisitor
    {
        private readonly List<ValidationIssue> _issues = new List<ValidationIssue>();
        private int _elementCounter = 0;

        public List<ValidationIssue> Issues => _issues.ToList();
        public bool HasIssues => _issues.Any();
        public int IssueCount => _issues.Count;

        public void VisitDocument(Document document)
        {
            _elementCounter = 0;
            
            if (string.IsNullOrWhiteSpace(document.Title))
            {
                AddIssue(ValidationIssueType.Error, "Document", "Document title is required");
            }
            
            if (string.IsNullOrWhiteSpace(document.Author))
            {
                AddIssue(ValidationIssueType.Warning, "Document", "Document author is recommended");
            }
            
            if (document.Tags.Count == 0)
            {
                AddIssue(ValidationIssueType.Info, "Document", "Adding tags can improve document organization");
            }
        }

        public void VisitTextElement(TextElement textElement)
        {
            _elementCounter++;
            
            if (string.IsNullOrWhiteSpace(textElement.Content))
            {
                AddIssue(ValidationIssueType.Warning, $"Text Element #{_elementCounter}", "Empty text content found");
            }
            
            if (textElement.FontSize < 8 || textElement.FontSize > 72)
            {
                AddIssue(ValidationIssueType.Warning, $"Text Element #{_elementCounter}", 
                    $"Font size {textElement.FontSize}pt is outside recommended range (8-72pt)");
            }
            
            if (textElement.GetCharacterCount() > 10000)
            {
                AddIssue(ValidationIssueType.Info, $"Text Element #{_elementCounter}", 
                    "Long text block detected - consider breaking into smaller paragraphs");
            }
        }

        public void VisitImageElement(ImageElement imageElement)
        {
            _elementCounter++;
            
            if (string.IsNullOrWhiteSpace(imageElement.Source))
            {
                AddIssue(ValidationIssueType.Error, $"Image Element #{_elementCounter}", "Image source is required");
            }
            
            if (string.IsNullOrWhiteSpace(imageElement.AltText))
            {
                AddIssue(ValidationIssueType.Warning, $"Image Element #{_elementCounter}", "Missing alternative text for accessibility");
            }
            
            if (imageElement.Width <= 0 || imageElement.Height <= 0)
            {
                AddIssue(ValidationIssueType.Error, $"Image Element #{_elementCounter}", "Invalid image dimensions");
            }
            
            if (imageElement.FileSize > 2000) // 2MB limit
            {
                AddIssue(ValidationIssueType.Warning, $"Image Element #{_elementCounter}", 
                    $"Large image file ({imageElement.FileSize:F1}KB) - consider compression");
            }
            
            var aspectRatio = imageElement.GetAspectRatio();
            if (aspectRatio > 5 || aspectRatio < 0.2)
            {
                AddIssue(ValidationIssueType.Info, $"Image Element #{_elementCounter}", 
                    $"Unusual aspect ratio ({aspectRatio:F2}) - verify image proportions");
            }
        }

        public void VisitTableElement(TableElement tableElement)
        {
            _elementCounter++;
            
            if (tableElement.GetRowCount() == 0)
            {
                AddIssue(ValidationIssueType.Warning, $"Table Element #{_elementCounter}", "Empty table found");
            }
            
            if (tableElement.GetColumnCount() > 10)
            {
                AddIssue(ValidationIssueType.Info, $"Table Element #{_elementCounter}", 
                    "Wide table detected - may not display well on mobile devices");
            }
            
            if (tableElement.GetCellCount() > 1000)
            {
                AddIssue(ValidationIssueType.Warning, $"Table Element #{_elementCounter}", 
                    "Very large table detected - consider pagination or filtering");
            }
            
            // Check for inconsistent row lengths
            var rowLengths = tableElement.Rows.Select(row => row.Count).Distinct().ToList();
            if (rowLengths.Count > 1)
            {
                AddIssue(ValidationIssueType.Warning, $"Table Element #{_elementCounter}", 
                    "Inconsistent row lengths found in table");
            }
        }

        private void AddIssue(ValidationIssueType type, string element, string message)
        {
            _issues.Add(new ValidationIssue
            {
                Type = type,
                Element = element,
                Message = message,
                Timestamp = DateTime.Now
            });
        }

        public void Reset()
        {
            _issues.Clear();
            _elementCounter = 0;
        }

        public void DisplayValidationReport()
        {
            Console.WriteLine("\n=== Document Validation Report ===");
            
            if (!HasIssues)
            {
                Console.WriteLine("✓ No validation issues found!");
                Console.WriteLine("===============================\n");
                return;
            }
            
            var errorCount = _issues.Count(i => i.Type == ValidationIssueType.Error);
            var warningCount = _issues.Count(i => i.Type == ValidationIssueType.Warning);
            var infoCount = _issues.Count(i => i.Type == ValidationIssueType.Info);
            
            Console.WriteLine($"Issues Summary:");
            Console.WriteLine($"  Errors: {errorCount}");
            Console.WriteLine($"  Warnings: {warningCount}");
            Console.WriteLine($"  Info: {infoCount}");
            Console.WriteLine();
            
            foreach (var issue in _issues)
            {
                var symbol = issue.Type switch
                {
                    ValidationIssueType.Error => "✗",
                    ValidationIssueType.Warning => "⚠",
                    ValidationIssueType.Info => "ℹ",
                    _ => "•"
                };
                
                Console.WriteLine($"{symbol} [{issue.Type}] {issue.Element}: {issue.Message}");
            }
            
            Console.WriteLine("===============================\n");
        }

        public List<ValidationIssue> GetErrors()
        {
            return _issues.Where(i => i.Type == ValidationIssueType.Error).ToList();
        }

        public List<ValidationIssue> GetWarnings()
        {
            return _issues.Where(i => i.Type == ValidationIssueType.Warning).ToList();
        }

        public bool HasErrors()
        {
            return _issues.Any(i => i.Type == ValidationIssueType.Error);
        }

        public bool HasWarnings()
        {
            return _issues.Any(i => i.Type == ValidationIssueType.Warning);
        }
    }

    /// <summary>
    /// Validation issue data structure
    /// </summary>
    public class ValidationIssue
    {
        public ValidationIssueType Type { get; set; }
        public string Element { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Validation issue type enumeration
    /// </summary>
    public enum ValidationIssueType
    {
        Error,
        Warning,
        Info
    }
}