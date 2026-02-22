using Composite.Components.Composite;
using Composite.Components.Leaf;

namespace Composite.Builders
{
    /// <summary>
    /// FileSystem builder
    /// Provides fluent API for constructing complex file system hierarchies
    /// </summary>
    public class FileSystemBuilder
    {
        private readonly FolderComponent _root;
        private FolderComponent _currentFolder;

        public FileSystemBuilder(string rootName = "Root")
        {
            _root = new FolderComponent(rootName);
            _currentFolder = _root;
        }

        /// <summary>
        /// Gets the constructed file system root
        /// </summary>
        public FolderComponent Root => _root;

        /// <summary>
        /// Creates a new folder in the current location
        /// </summary>
        public FileSystemBuilder CreateFolder(string name)
        {
            var folder = new FolderComponent(name);
            _currentFolder.Add(folder);
            return this;
        }

        /// <summary>
        /// Creates a file in the current location
        /// </summary>
        public FileSystemBuilder CreateFile(string name, long size, string extension = "")
        {
            var file = new FileComponent(name, size, extension);
            _currentFolder.Add(file);
            return this;
        }

        /// <summary>
        /// Navigates into a folder
        /// </summary>
        public FileSystemBuilder EnterFolder(string name)
        {
            var folder = _currentFolder.FindByName(name) as FolderComponent;
            if (folder != null)
            {
                _currentFolder = folder;
            }
            else
            {
                throw new InvalidOperationException($"Folder '{name}' not found in current directory");
            }
            return this;
        }

        /// <summary>
        /// Navigates back to parent folder
        /// </summary>
        public FileSystemBuilder GoBack()
        {
            if (_currentFolder.Parent is FolderComponent parent)
            {
                _currentFolder = parent;
            }
            return this;
        }

        /// <summary>
        /// Navigates to root folder
        /// </summary>
        public FileSystemBuilder GoToRoot()
        {
            _currentFolder = _root;
            return this;
        }

        /// <summary>
        /// Builds a complete project structure
        /// </summary>
        public static FolderComponent BuildSampleProject()
        {
            return new FileSystemBuilder("MyProject")
                .CreateFolder("src")
                    .EnterFolder("src")
                    .CreateFolder("Controllers")
                        .EnterFolder("Controllers")
                        .CreateFile("HomeController.cs", 2048, ".cs")
                        .CreateFile("UserController.cs", 4096, ".cs")
                        .GoBack()
                    .CreateFolder("Models")
                        .EnterFolder("Models")
                        .CreateFile("User.cs", 1024, ".cs")
                        .CreateFile("Product.cs", 1536, ".cs")
                        .GoBack()
                    .CreateFolder("Views")
                        .EnterFolder("Views")
                        .CreateFolder("Home")
                            .EnterFolder("Home")
                            .CreateFile("Index.cshtml", 3072, ".cshtml")
                            .CreateFile("About.cshtml", 2560, ".cshtml")
                            .GoBack()
                        .GoBack()
                    .GoBack()
                .CreateFolder("tests")
                    .EnterFolder("tests")
                    .CreateFile("HomeControllerTests.cs", 2048, ".cs")
                    .CreateFile("UserControllerTests.cs", 3072, ".cs")
                    .GoBack()
                .CreateFile("README.md", 1024, ".md")
                .CreateFile("appsettings.json", 512, ".json")
                .Root;
        }

        /// <summary>
        /// Builds a document library structure
        /// </summary>
        public static FolderComponent BuildDocumentLibrary()
        {
            return new FileSystemBuilder("Documents")
                .CreateFolder("Work")
                    .EnterFolder("Work")
                    .CreateFile("Project_Plan.pdf", 2048000, ".pdf")
                    .CreateFile("Meeting_Notes.docx", 102400, ".docx")
                    .CreateFile("Budget.xlsx", 51200, ".xlsx")
                    .GoBack()
                .CreateFolder("Personal")
                    .EnterFolder("Personal")
                    .CreateFolder("Photos")
                        .EnterFolder("Photos")
                        .CreateFile("Vacation.jpg", 4096000, ".jpg")
                        .CreateFile("Family.png", 2048000, ".png")
                        .GoBack()
                    .CreateFolder("Music")
                        .EnterFolder("Music")
                        .CreateFile("Favorite_Songs.mp3", 30720000, ".mp3")
                        .GoBack()
                    .GoBack()
                .CreateFolder("Archives")
                    .EnterFolder("Archives")
                    .CreateFile("Old_Project.zip", 10240000, ".zip")
                    .GoBack()
                .Root;
        }

        /// <summary>
        /// Builds a software development environment structure
        /// </summary>
        public static FolderComponent BuildDevEnvironment()
        {
            return new FileSystemBuilder("DevEnvironment")
                .CreateFolder("Projects")
                    .EnterFolder("Projects")
                    .CreateFolder("WebApp")
                        .EnterFolder("WebApp")
                        .CreateFolder("ClientApp")
                            .EnterFolder("ClientApp")
                            .CreateFile("package.json", 1024, ".json")
                            .CreateFile("webpack.config.js", 2048, ".js")
                            .GoBack()
                        .CreateFolder("Server")
                            .EnterFolder("Server")
                            .CreateFile("Program.cs", 2048, ".cs")
                            .CreateFile("Startup.cs", 4096, ".cs")
                            .CreateFolder("Controllers")
                                .EnterFolder("Controllers")
                                .CreateFile("ApiControlller.cs", 3072, ".cs")
                                .GoBack()
                            .GoBack()
                        .GoBack()
                    .GoBack()
                .CreateFolder("Tools")
                    .EnterFolder("Tools")
                    .CreateFile("Git.exe", 15728640, ".exe")
                    .CreateFile("Node.exe", 31457280, ".exe")
                    .GoBack()
                .CreateFolder("Config")
                    .EnterFolder("Config")
                    .CreateFile("environment.json", 512, ".json")
                    .CreateFile("database.conf", 1024, ".conf")
                    .GoBack()
                .Root;
        }
    }
}