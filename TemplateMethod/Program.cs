// See https://aka.ms/new-console-template for more information
using TemplateMethod.Framework;
using TemplateMethod.Processors;

namespace TemplateMethod
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Template Method Pattern Implementation Examples ===\n");

            try
            {
                // Demonstrate CSV data processing
                DemonstrateCsvProcessing();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate JSON data processing
                DemonstrateJsonProcessing();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate XML configuration processing
                DemonstrateXmlProcessing();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate batch processing
                DemonstrateBatchProcessing();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate template method pattern benefits
                DemonstratePatternBenefits();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\n=== Template Method Pattern Demo Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates CSV data processing using template method
        /// </summary>
        private static void DemonstrateCsvProcessing()
        {
            Console.WriteLine("1. CSV Data Processing Demo");
            Console.WriteLine("===========================");
            
            var csvProcessor = new CsvDataProcessor("employees.csv");
            csvProcessor.ProcessData();
            
            Console.WriteLine($"Processing time: {csvProcessor.GetProcessingDuration().TotalMilliseconds:F0} ms");
        }

        /// <summary>
        /// Demonstrates JSON data processing using template method
        /// </summary>
        private static void DemonstrateJsonProcessing()
        {
            Console.WriteLine("\n2. JSON Data Processing Demo");
            Console.WriteLine("=============================");
            
            var jsonProcessor = new JsonDataProcessor("https://api.example.com/products");
            jsonProcessor.ProcessData();
            
            Console.WriteLine($"Processing time: {jsonProcessor.GetProcessingDuration().TotalMilliseconds:F0} ms");
        }

        /// <summary>
        /// Demonstrates XML configuration processing using template method
        /// </summary>
        private static void DemonstrateXmlProcessing()
        {
            Console.WriteLine("\n3. XML Configuration Processing Demo");
            Console.WriteLine("=====================================");
            
            var xmlProcessor = new XmlDataProcessor("app.config.xml");
            xmlProcessor.ProcessData();
            
            Console.WriteLine($"Processing time: {xmlProcessor.GetProcessingDuration().TotalMilliseconds:F0} ms");
        }

        /// <summary>
        /// Demonstrates batch processing with multiple processors
        /// </summary>
        private static void DemonstrateBatchProcessing()
        {
            Console.WriteLine("\n4. Batch Processing Demo");
            Console.WriteLine("========================");
            
            var processors = new DataProcessor[]
            {
                new CsvDataProcessor("sales_data.csv"),
                new JsonDataProcessor("https://api.sales.com/data"),
                new XmlDataProcessor("integration_config.xml")
            };
            
            Console.WriteLine($"\nProcessing {processors.Length} different data sources:");
            
            var totalTime = TimeSpan.Zero;
            foreach (var processor in processors)
            {
                processor.ProcessData();
                totalTime += processor.GetProcessingDuration();
            }
            
            Console.WriteLine($"\n=== Batch Processing Summary ===");
            Console.WriteLine($"Total processors executed: {processors.Length}");
            Console.WriteLine($"Combined processing time: {totalTime.TotalMilliseconds:F0} ms");
            Console.WriteLine($"Average time per processor: {(totalTime.TotalMilliseconds / processors.Length):F0} ms");
        }

        /// <summary>
        /// Demonstrates template method pattern benefits and concepts
        /// </summary>
        private static void DemonstratePatternBenefits()
        {
            Console.WriteLine("\n5. Template Method Pattern Benefits Demo");
            Console.WriteLine("=========================================");
            
            Console.WriteLine("Template Method Pattern Key Benefits:");
            Console.WriteLine("• Defines algorithm skeleton in base class");
            Console.WriteLine("• Allows subclasses to override specific steps");
            Console.WriteLine("• Ensures consistent processing flow");
            Console.WriteLine("• Reduces code duplication");
            Console.WriteLine("• Provides extension points through hook methods");
            
            Console.WriteLine("\nCommon Use Cases:");
            Console.WriteLine("• Data processing pipelines");
            Console.WriteLine("• File format converters");
            Console.WriteLine("• Report generators");
            Console.WriteLine("• Build processes");
            Console.WriteLine("• Database migration scripts");
            Console.WriteLine("• Configuration management");
            
            Console.WriteLine("\nWhen to Use Template Method Pattern:");
            Console.WriteLine("• Algorithm structure is stable but steps vary");
            Console.WriteLine("• Want to enforce a common workflow");
            Console.WriteLine("• Need to avoid code duplication in similar processes");
            Console.WriteLine("• Want to provide customization points");
            Console.WriteLine("• Algorithm steps should be executed in specific order");
            
            Console.WriteLine("\nData Processing Pipeline Stages Demonstrated:");
            Console.WriteLine("1. Initialization and Setup");
            Console.WriteLine("2. Data Loading (abstract method)");
            Console.WriteLine("3. Data Validation (hook method)");
            Console.WriteLine("4. Data Transformation (hook method)");
            Console.WriteLine("5. Data Processing (hook method)");
            Console.WriteLine("6. Result Saving (abstract method)");
            Console.WriteLine("7. Report Generation (hook method)");
            Console.WriteLine("8. Error Handling and Cleanup");
        }
    }
}