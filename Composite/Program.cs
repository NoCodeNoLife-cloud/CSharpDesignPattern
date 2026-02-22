// See https://aka.ms/new-console-template for more information
using Composite.Builders;
using Composite.Components.Composite;
using Composite.Services;

namespace Composite
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Composite Pattern Implementation Examples ===\n");

            try
            {
                // Demonstrate basic composite structure
                DemonstrateBasicStructure();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate file system builder
                DemonstrateFileSystemBuilder();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate file system service
                DemonstrateFileSystemService();
                
                Console.WriteLine(new string('=', 70));
                
                // Demonstrate advanced operations
                DemonstrateAdvancedOperations();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\n=== Composite Pattern Demo Completed ===");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates basic composite pattern structure
        /// </summary>
        private static void DemonstrateBasicStructure()
        {
            Console.WriteLine("1. Basic Composite Pattern Demo");
            Console.WriteLine("================================");
            
            // Create root folder
            var root = new FolderComponent("MyComputer");
            
            // Add some files directly to root
            root.Add(new Components.Leaf.FileComponent("readme.txt", 1024, ".txt"));
            root.Add(new Components.Leaf.FileComponent("config.json", 512, ".json"));
            
            // Create and add subfolders
            var documents = new FolderComponent("Documents");
            documents.Add(new Components.Leaf.FileComponent("report.pdf", 2048000, ".pdf"));
            documents.Add(new Components.Leaf.FileComponent("notes.docx", 102400, ".docx"));
            
            var images = new FolderComponent("Images");
            images.Add(new Components.Leaf.FileComponent("photo1.jpg", 3072000, ".jpg"));
            images.Add(new Components.Leaf.FileComponent("photo2.png", 2048000, ".png"));
            
            documents.Add(images);
            root.Add(documents);
            
            // Display the structure
            root.Display();
            
            // Show statistics
            var stats = root.GetStats();
            Console.WriteLine($"\nStatistics:");
            Console.WriteLine($"  Total Items: {stats.TotalItems}");
            Console.WriteLine($"  Files: {stats.FileCount}");
            Console.WriteLine($"  Folders: {stats.FolderCount}");
            Console.WriteLine($"  Total Size: {stats.FormattedSize}");
        }

        /// <summary>
        /// Demonstrates file system builder usage
        /// </summary>
        private static void DemonstrateFileSystemBuilder()
        {
            Console.WriteLine("\n2. File System Builder Demo");
            Console.WriteLine("============================");
            
            // Build sample project structure
            Console.WriteLine("\n--- Sample Project Structure ---");
            var projectStructure = FileSystemBuilder.BuildSampleProject();
            projectStructure.Display();
            
            // Build document library
            Console.WriteLine("\n--- Document Library Structure ---");
            var documentLibrary = FileSystemBuilder.BuildDocumentLibrary();
            documentLibrary.Display();
            
            // Build development environment
            Console.WriteLine("\n--- Development Environment ---");
            var devEnvironment = FileSystemBuilder.BuildDevEnvironment();
            devEnvironment.Display();
        }

        /// <summary>
        /// Demonstrates file system service operations
        /// </summary>
        private static void DemonstrateFileSystemService()
        {
            Console.WriteLine("\n3. File System Service Demo");
            Console.WriteLine("============================");
            
            var fileSystem = FileSystemBuilder.BuildSampleProject();
            var service = new FileSystemService(fileSystem);
            
            // Display complete file system
            service.DisplayFileSystem();
            
            // Show statistics
            var stats = service.GetStatistics();
            Console.WriteLine("File System Statistics:");
            Console.WriteLine($"  Root Folder: {stats.RootFolderName}");
            Console.WriteLine($"  Total Folders: {stats.TotalFolders}");
            Console.WriteLine($"  Total Files: {stats.TotalFiles}");
            Console.WriteLine($"  Total Size: {stats.FormattedTotalSize}");
            
            // Search operations
            Console.WriteLine("\nSearching for .cs files:");
            var csFiles = service.SearchByExtension(".cs");
            foreach (var file in csFiles)
            {
                file.Display(1);
            }
            
            Console.WriteLine("\nSearching for large files (> 2KB):");
            var largeFiles = service.SearchByMinimumSize(2048);
            foreach (var file in largeFiles)
            {
                Console.WriteLine($"  {file.Path} - {file.Size} bytes");
            }
        }

        /// <summary>
        /// Demonstrates advanced composite operations
        /// </summary>
        private static void DemonstrateAdvancedOperations()
        {
            Console.WriteLine("\n4. Advanced Operations Demo");
            Console.WriteLine("============================");
            
            var fileSystem = FileSystemBuilder.BuildDocumentLibrary();
            var service = new FileSystemService(fileSystem);
            
            // Find duplicates
            Console.WriteLine("Finding duplicate files:");
            var duplicates = service.FindDuplicateFiles();
            if (duplicates.Any())
            {
                foreach (var kvp in duplicates)
                {
                    Console.WriteLine($"  '{kvp.Key}' appears {kvp.Value.Count} times:");
                    foreach (var file in kvp.Value)
                    {
                        Console.WriteLine($"    - {file.Path}");
                    }
                }
            }
            else
            {
                Console.WriteLine("  No duplicate files found");
            }
            
            // Get largest files
            Console.WriteLine("\nLargest files:");
            var largestFiles = service.GetLargestFiles(3);
            foreach (var file in largestFiles)
            {
                Console.WriteLine($"  {file.Name}: {file.Size} bytes");
            }
            
            // Folder size distribution
            Console.WriteLine("\nFolder size distribution:");
            var distribution = service.GetFolderSizeDistribution();
            foreach (var kvp in distribution.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value} bytes");
            }
            
            // Export to text
            Console.WriteLine("\nExported file system structure:");
            var exported = service.ExportToFileSystemText();
            Console.WriteLine(exported);
            
            Console.WriteLine("\nComposite Pattern Benefits:");
            Console.WriteLine("• Treats individual objects and compositions uniformly");
            Console.WriteLine("• Simplifies client code by using polymorphism");
            Console.WriteLine("• Enables recursive operations on tree structures");
            Console.WriteLine("• Supports arbitrary nesting levels");
            Console.WriteLine("• Makes it easy to add new component types");
        }
    }
}