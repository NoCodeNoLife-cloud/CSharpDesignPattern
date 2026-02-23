using System.Text;
using Visitor.Elements;

namespace Visitor.Visitors
{
    /// <summary>
    /// Export visitor implementation
    /// Exports document elements to different formats
    /// </summary>
    public class ExportVisitor : IDocumentVisitor
    {
        private readonly StringBuilder _output = new StringBuilder();
        private int _indentLevel = 0;

        public string ExportedContent => _output.ToString();

        public void VisitDocument(Document document)
        {
            _output.AppendLine($"# {document.Title}");
            _output.AppendLine($"*Author: {document.Author}*");
            _output.AppendLine($"*Created: {document.CreatedDate:yyyy-MM-dd}*");
            _output.AppendLine();
        }

        public void VisitTextElement(TextElement textElement)
        {
            var indent = new string(' ', _indentLevel * 2);
            var formatting = "";
            
            if (textElement.IsBold) formatting += "**";
            if (textElement.IsItalic) formatting += "*";
            
            _output.AppendLine($"{indent}{formatting}{textElement.Content}{formatting}");
        }

        public void VisitImageElement(ImageElement imageElement)
        {
            var indent = new string(' ', _indentLevel * 2);
            _output.AppendLine($"{indent}![{imageElement.AltText}]({imageElement.Source})");
            _output.AppendLine($"{indent}*Dimensions: {imageElement.GetDimensions()}, Size: {imageElement.FileSize:F1}KB*");
        }

        public void VisitTableElement(TableElement tableElement)
        {
            var indent = new string(' ', _indentLevel * 2);
            
            // Add caption if exists
            if (!string.IsNullOrEmpty(tableElement.Caption))
            {
                _output.AppendLine($"{indent}*{tableElement.Caption}*");
            }
            
            // Add headers
            if (tableElement.Headers.Any())
            {
                _output.Append(indent);
                foreach (var header in tableElement.Headers)
                {
                    _output.Append($"| {header} ");
                }
                _output.AppendLine("|");
                
                // Add separator
                _output.Append(indent);
                for (int i = 0; i < tableElement.Headers.Count; i++)
                {
                    _output.Append("| --- ");
                }
                _output.AppendLine("|");
            }
            
            // Add rows
            foreach (var row in tableElement.Rows)
            {
                _output.Append(indent);
                foreach (var cell in row)
                {
                    _output.Append($"| {cell} ");
                }
                _output.AppendLine("|");
            }
            
            _output.AppendLine();
        }

        public string ExportToMarkdown()
        {
            return _output.ToString();
        }

        public string ExportToHtml()
        {
            var html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("    <title>Document Export</title>");
            html.AppendLine("    <style>");
            html.AppendLine("        body { font-family: Arial, sans-serif; margin: 20px; }");
            html.AppendLine("        table { border-collapse: collapse; margin: 10px 0; }");
            html.AppendLine("        th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }");
            html.AppendLine("        th { background-color: #f2f2f2; }");
            html.AppendLine("        img { max-width: 100%; height: auto; }");
            html.AppendLine("    </style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            
            // Convert markdown-like content to HTML
            foreach (var line in _output.ToString().Split('\n'))
            {
                if (line.StartsWith("# "))
                {
                    html.AppendLine($"<h1>{line.Substring(2)}</h1>");
                }
                else if (line.StartsWith("*") && line.EndsWith("*"))
                {
                    html.AppendLine($"<p><em>{line.Substring(1, line.Length - 2)}</em></p>");
                }
                else if (line.StartsWith("**") && line.EndsWith("**"))
                {
                    html.AppendLine($"<p><strong>{line.Substring(2, line.Length - 4)}</strong></p>");
                }
                else if (line.StartsWith("!"))
                {
                    // Image
                    var match = System.Text.RegularExpressions.Regex.Match(line, @"!\[(.*?)\]\((.*?)\)");
                    if (match.Success)
                    {
                        html.AppendLine($"<img src=\"{match.Groups[2].Value}\" alt=\"{match.Groups[1].Value}\" />");
                    }
                }
                else if (line.Trim().StartsWith("|"))
                {
                    // Table row
                    if (line.Contains("---"))
                    {
                        // Skip separator line
                        continue;
                    }
                    else
                    {
                        var cells = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
                        if (cells.Length > 0)
                        {
                            html.AppendLine("<tr>");
                            foreach (var cell in cells)
                            {
                                html.AppendLine($"<td>{cell.Trim()}</td>");
                            }
                            html.AppendLine("</tr>");
                        }
                    }
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    html.AppendLine($"<p>{line}</p>");
                }
            }
            
            html.AppendLine("</body>");
            html.AppendLine("</html>");
            
            return html.ToString();
        }

        public void Reset()
        {
            _output.Clear();
            _indentLevel = 0;
        }

        public void IncreaseIndent()
        {
            _indentLevel++;
        }

        public void DecreaseIndent()
        {
            if (_indentLevel > 0)
                _indentLevel--;
        }
    }
}