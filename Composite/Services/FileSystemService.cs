using System.Text;
using Composite.Components;
using Composite.Components.Composite;
using Composite.Components.Leaf;

namespace Composite.Services
{
    /// <summary>
    /// FileSystem service
    /// Provides high-level operations for file system management
    /// </summary>
    public class FileSystemService
    {
        private readonly FolderComponent _root;

        public FileSystemService(FolderComponent root)
        {
            _root = root;
        }

        /// <summary>
        /// Displays the entire file system structure
        /// </summary>
        public void DisplayFileSystem()
        {
            Console.WriteLine("=== File System Structure ===");
            _root.Display();
            Console.WriteLine("=============================\n");
        }

        /// <summary>
        /// Searches for files by extension
        /// </summary>
        public IEnumerable<IFileSystemComponent> SearchByExtension(string extension)
        {
            return _root.Search(component =>
                component is FileComponent file &&
                file.Extension.Equals(extension, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Searches for files larger than specified size
        /// </summary>
        public IEnumerable<IFileSystemComponent> SearchByMinimumSize(long minSize)
        {
            return _root.Search(component => component.Size >= minSize);
        }

        /// <summary>
        /// Searches for files modified after specified date
        /// </summary>
        public IEnumerable<IFileSystemComponent> SearchByModifiedDate(DateTime date)
        {
            return _root.Search(component =>
            {
                if (component is FileComponent file)
                    return file.ModifiedDate >= date;
                if (component is FolderComponent folder)
                    return folder.ModifiedDate >= date;
                return false;
            });
        }

        /// <summary>
        /// Searches for components by name pattern
        /// </summary>
        public IEnumerable<IFileSystemComponent> SearchByName(string pattern)
        {
            return _root.Search(component =>
                component.Name.Contains(pattern, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets file system statistics
        /// </summary>
        public FileSystemStats GetStatistics()
        {
            var stats = _root.GetStats();
            return new FileSystemStats
            {
                TotalFolders = stats.FolderCount,
                TotalFiles = stats.FileCount,
                TotalSize = stats.TotalSize,
                RootFolderName = _root.Name
            };
        }

        /// <summary>
        /// Finds duplicate files by name
        /// </summary>
        public Dictionary<string, List<IFileSystemComponent>> FindDuplicateFiles()
        {
            var allFiles = _root.GetAllFiles();
            var duplicates = new Dictionary<string, List<IFileSystemComponent>>();

            foreach (var file in allFiles)
            {
                if (!duplicates.ContainsKey(file.Name))
                {
                    duplicates[file.Name] = new List<IFileSystemComponent>();
                }

                duplicates[file.Name].Add(file);
            }

            // Return only actual duplicates (more than one file with same name)
            return duplicates.Where(kvp => kvp.Value.Count > 1).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        /// <summary>
        /// Gets largest files in the system
        /// </summary>
        public IEnumerable<IFileSystemComponent> GetLargestFiles(int count = 10)
        {
            return _root.GetAllFiles()
                .OrderByDescending(f => f.Size)
                .Take(count);
        }

        /// <summary>
        /// Calculates folder size distribution
        /// </summary>
        public Dictionary<string, long> GetFolderSizeDistribution()
        {
            var distribution = new Dictionary<string, long>();

            void TraverseFolder(FolderComponent folder, string pathPrefix = "")
            {
                var currentPath = string.IsNullOrEmpty(pathPrefix) ? folder.Name : $"{pathPrefix}/{folder.Name}";
                distribution[currentPath] = folder.Size;

                foreach (var child in folder.GetChildren().OfType<FolderComponent>())
                {
                    TraverseFolder(child, currentPath);
                }
            }

            TraverseFolder(_root);
            return distribution;
        }

        /// <summary>
        /// Exports file system structure to text format
        /// </summary>
        public string ExportToFileSystemText()
        {
            var sb = new StringBuilder();
            ExportFolder(_root, sb, 0);
            return sb.ToString();
        }

        private void ExportFolder(FolderComponent folder, StringBuilder sb, int depth)
        {
            var indent = new string(' ', depth * 2);
            sb.AppendLine($"{indent}[Folder] {folder.Name} ({FormatSize(folder.Size)})");

            foreach (var child in folder.GetChildren())
            {
                if (child is FileComponent file)
                {
                    sb.AppendLine($"{indent}  [File] {file.Name} ({FormatSize(file.Size)})");
                }
                else if (child is FolderComponent subFolder)
                {
                    ExportFolder(subFolder, sb, depth + 1);
                }
            }
        }

        private static string FormatSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }
    }

    /// <summary>
    /// File system statistics data structure
    /// </summary>
    public class FileSystemStats
    {
        public int TotalFolders { get; set; }
        public int TotalFiles { get; set; }
        public long TotalSize { get; set; }
        public string RootFolderName { get; set; } = string.Empty;

        public string FormattedTotalSize => FormatSize(TotalSize);

        private static string FormatSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }
    }
}