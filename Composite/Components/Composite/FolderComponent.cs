using System.Text;
using Composite.Components.Leaf;

namespace Composite.Components.Composite
{
    /// <summary>
    /// Folder composite component
    /// Represents folders that can contain files and other folders
    /// </summary>
    public class FolderComponent : IFileSystemComponent
    {
        private readonly List<IFileSystemComponent> _children = new List<IFileSystemComponent>();

        public string Name { get; set; }
        public IFileSystemComponent? Parent { get; set; }

        public string Path => Parent == null ? Name : $"{Parent.Path}/{Name}";

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        // Calculated property - sum of all children sizes
        public long Size => _children.Sum(child => child.Size);

        public FolderComponent(string name)
        {
            Name = name;
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// Adds a child component to this folder
        /// </summary>
        public void Add(IFileSystemComponent component)
        {
            _children.Add(component);
            component.Parent = this;
            UpdateModifiedDate();
        }

        /// <summary>
        /// Removes a child component from this folder
        /// </summary>
        public void Remove(IFileSystemComponent component)
        {
            if (_children.Remove(component))
            {
                component.Parent = null;
                UpdateModifiedDate();
            }
        }

        /// <summary>
        /// Gets all child components
        /// </summary>
        public IEnumerable<IFileSystemComponent> GetChildren()
        {
            return _children.AsReadOnly();
        }

        /// <summary>
        /// Checks if this folder contains the specified component
        /// </summary>
        public bool Contains(IFileSystemComponent component)
        {
            return _children.Contains(component);
        }

        /// <summary>
        /// Gets the number of direct children
        /// </summary>
        public int ChildrenCount => _children.Count;

        public void Display(int depth = 0)
        {
            var indent = new string(' ', depth * 2);
            var childCount = _children.Count;
            var totalSize = FormatSize(Size);

            Console.WriteLine($"{indent}📁 {Name} [{childCount} items, {totalSize}] - Modified: {ModifiedDate:yyyy-MM-dd HH:mm}");

            // Display children with increased depth
            foreach (var child in _children)
            {
                child.Display(depth + 1);
            }
        }

        public IEnumerable<IFileSystemComponent> Search(Func<IFileSystemComponent, bool> predicate)
        {
            var results = new List<IFileSystemComponent>();

            // Check if this folder matches the criteria
            if (predicate(this))
            {
                results.Add(this);
            }

            // Search all children
            foreach (var child in _children)
            {
                results.AddRange(child.Search(predicate));
            }

            return results;
        }

        /// <summary>
        /// Finds a component by name (case-insensitive)
        /// </summary>
        public IFileSystemComponent? FindByName(string name)
        {
            return _children.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets all files in this folder and subfolders
        /// </summary>
        public IEnumerable<IFileSystemComponent> GetAllFiles()
        {
            var files = new List<IFileSystemComponent>();

            foreach (var child in _children)
            {
                if (child is FileComponent)
                {
                    files.Add(child);
                }
                else if (child is FolderComponent folder)
                {
                    files.AddRange(folder.GetAllFiles());
                }
            }

            return files;
        }

        /// <summary>
        /// Gets folder statistics
        /// </summary>
        public FolderStats GetStats()
        {
            var stats = new FolderStats
            {
                TotalItems = _children.Count,
                FileCount = _children.OfType<FileComponent>().Count(),
                FolderCount = _children.OfType<FolderComponent>().Count(),
                TotalSize = Size
            };

            foreach (var child in _children.OfType<FolderComponent>())
            {
                var childStats = child.GetStats();
                stats.FileCount += childStats.FileCount;
                stats.FolderCount += childStats.FolderCount;
                stats.TotalSize += childStats.TotalSize;
            }

            return stats;
        }

        private void UpdateModifiedDate()
        {
            ModifiedDate = DateTime.Now;
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
    /// Folder statistics data structure
    /// </summary>
    public class FolderStats
    {
        public int TotalItems { get; set; }
        public int FileCount { get; set; }
        public int FolderCount { get; set; }
        public long TotalSize { get; set; }

        public string FormattedSize => FormatSize(TotalSize);

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