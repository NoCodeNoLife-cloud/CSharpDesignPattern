// See https://aka.ms/new-console-template for more information
using Visitor.Elements;
using Visitor.Visitors;

namespace Visitor
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Visitor Pattern Implementation Examples ===\n");

            try
            {
                // Create sample document
                var document = CreateSampleDocument();
                
                // Demonstrate statistics visitor
                DemonstrateStatisticsVisitor(document);
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate export visitor
                DemonstrateExportVisitor(document);
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate validation visitor
                DemonstrateValidationVisitor(document);
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate visitor pattern benefits
                DemonstrateVisitorBenefits();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\n=== Visitor Pattern Demo Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Creates a sample document for demonstration
        /// </summary>
        private static Document CreateSampleDocument()
        {
            var document = new Document("Software Architecture Guide", "John Architect");
            document.Tags.AddRange(new[] { "Architecture", "Design Patterns", "Software Engineering" });
            
            // Add introduction text
            var intro = new TextElement("This guide provides comprehensive coverage of software architecture patterns and best practices.");
            intro.IsBold = true;
            document.AddElement(intro);
            
            // Add chapter heading
            var chapter = new TextElement("Chapter 1: Design Patterns Overview");
            chapter.FontSize = 18;
            chapter.IsBold = true;
            document.AddElement(chapter);
            
            // Add content paragraph
            var content = new TextElement("Design patterns are reusable solutions to common software design problems. They provide proven approaches to structuring code and solving architectural challenges.");
            document.AddElement(content);
            
            // Add image
            var diagram = new ImageElement("architecture-diagram.png", "Software Architecture Diagram");
            diagram.Width = 800;
            diagram.Height = 600;
            diagram.FileSize = 150.5;
            document.AddElement(diagram);
            
            // Add comparison table
            var headers = new List<string> { "Pattern", "Purpose", "Complexity", "Use Case" };
            var rows = new List<List<string>>
            {
                new List<string> { "Singleton", "Ensure single instance", "Low", "Configuration managers" },
                new List<string> { "Factory", "Object creation", "Medium", "UI components" },
                new List<string> { "Observer", "Event handling", "Medium", "MVC applications" },
                new List<string> { "Strategy", "Algorithm selection", "High", "Payment processing" }
            };
            
            var table = new TableElement(headers, rows);
            table.Caption = "Common Design Patterns Comparison";
            document.AddElement(table);
            
            // Add conclusion
            var conclusion = new TextElement("Understanding these patterns will help you design more maintainable and scalable software systems.");
            conclusion.IsItalic = true;
            document.AddElement(conclusion);
            
            return document;
        }

        /// <summary>
        /// Demonstrates statistics visitor functionality
        /// </summary>
        private static void DemonstrateStatisticsVisitor(Document document)
        {
            Console.WriteLine("1. Statistics Visitor Demo");
            Console.WriteLine("==========================");
            
            var statsVisitor = new StatisticsVisitor();
            document.Accept(statsVisitor);
            statsVisitor.DisplayStatistics();
            
            Console.WriteLine($"Document elements breakdown:");
            Console.WriteLine($"  Text elements: {document.GetElementCountByType<TextElement>()}");
            Console.WriteLine($"  Image elements: {document.GetElementCountByType<ImageElement>()}");
            Console.WriteLine($"  Table elements: {document.GetElementCountByType<TableElement>()}");
        }

        /// <summary>
        /// Demonstrates export visitor functionality
        /// </summary>
        private static void DemonstrateExportVisitor(Document document)
        {
            Console.WriteLine("\n2. Export Visitor Demo");
            Console.WriteLine("=======================");
            
            var exportVisitor = new ExportVisitor();
            document.Accept(exportVisitor);
            
            Console.WriteLine("Markdown Export:");
            Console.WriteLine(new string('-', 30));
            Console.WriteLine(exportVisitor.ExportedContent);
            Console.WriteLine(new string('-', 30));
            
            Console.WriteLine("\nHTML Export:");
            Console.WriteLine(new string('-', 30));
            var html = exportVisitor.ExportToHtml();
            Console.WriteLine(html.Substring(0, Math.Min(500, html.Length)) + "...");
            Console.WriteLine(new string('-', 30));
        }

        /// <summary>
        /// Demonstrates validation visitor functionality
        /// </summary>
        private static void DemonstrateValidationVisitor(Document document)
        {
            Console.WriteLine("\n3. Validation Visitor Demo");
            Console.WriteLine("===========================");
            
            var validationVisitor = new ValidationVisitor();
            document.Accept(validationVisitor);
            validationVisitor.DisplayValidationReport();
            
            if (validationVisitor.HasErrors())
            {
                Console.WriteLine("Critical errors found - document needs corrections!");
            }
            else if (validationVisitor.HasWarnings())
            {
                Console.WriteLine("Document has warnings - review suggested improvements");
            }
            else
            {
                Console.WriteLine("Document passed all validation checks!");
            }
        }

        /// <summary>
        /// Demonstrates visitor pattern benefits
        /// </summary>
        private static void DemonstrateVisitorBenefits()
        {
            Console.WriteLine("\n4. Visitor Pattern Benefits Demo");
            Console.WriteLine("=================================");
            
            Console.WriteLine("Visitor Pattern Key Benefits:");
            Console.WriteLine("• Separates algorithms from object structure");
            Console.WriteLine("• Allows adding new operations without modifying existing classes");
            Console.WriteLine("• Enables double dispatch mechanism");
            Console.WriteLine("• Supports multiple algorithms for same object structure");
            Console.WriteLine("• Maintains open/closed principle");
            
            Console.WriteLine("\nUse Cases Demonstrated:");
            Console.WriteLine("• Statistics Collection - Counting and measuring elements");
            Console.WriteLine("• Data Export - Converting to different formats");
            Console.WriteLine("• Validation - Quality checking and compliance");
            
            Console.WriteLine("\nReal-world Applications:");
            Console.WriteLine("• Compiler design (AST traversal)");
            Console.WriteLine("• Document processing systems");
            Console.WriteLine("• XML/JSON processing");
            Console.WriteLine("• Game object processing");
            Console.WriteLine("• Financial calculation engines");
            
            Console.WriteLine("\nWhen to Use Visitor Pattern:");
            Console.WriteLine("• Object structure is stable but operations change frequently");
            Console.WriteLine("• Need to perform multiple unrelated operations");
            Console.WriteLine("• Want to avoid type checking and casting");
            Console.WriteLine("• Require clean separation of concerns");
        }
    }
}