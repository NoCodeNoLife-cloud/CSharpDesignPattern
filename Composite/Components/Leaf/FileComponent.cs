using System.Text;

namespace Composite.Components.Leaf
{
    /// <summary>
    /// File leaf component
    /// Represents individual files in the file system
    /// </summary>
    public class FileComponent : IFileSystemComponent
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public IFileSystemComponent? Parent { get; set; }
        
        public string Path => Parent == null ? Name : $"{Parent.Path}/{Name}";
        
        public string Extension { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public FileComponent(string name, long size, string extension = "")
        {
            Name = name;
            Size = size;
            Extension = extension;
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        public void Display(int depth = 0)
        {
            var indent = new string(' ', depth * 2);
            var icon = GetFileIcon();
            Console.WriteLine($"{indent}{icon} {Name} ({FormatSize(Size)}) - Modified: {ModifiedDate:yyyy-MM-dd HH:mm}");
        }

        public IEnumerable<IFileSystemComponent> Search(Func<IFileSystemComponent, bool> predicate)
        {
            return predicate(this) ? new[] { this } : Enumerable.Empty<IFileSystemComponent>();
        }

        private string GetFileIcon()
        {
            return Extension.ToLower() switch
            {
                ".txt" => "📄",
                ".pdf" => "📚",
                ".jpg" or ".png" or ".gif" => "🖼️",
                ".mp3" or ".wav" => "🎵",
                ".mp4" or ".avi" => "🎬",
                ".exe" => "⚙️",
                ".zip" or ".rar" => "📦",
                _ => "📝"
            };
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
}