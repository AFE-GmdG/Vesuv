using System.IO;

namespace Vesuv.Core.IO
{
    public class Project
    {
        private IFileSystem _fileSystem;

        private Project(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public static async Task<Project?> OpenProject(string projectPath)
        {
            try {
                var fileSystem = await NativeFileSystem.InitializeNativeFileSystem(projectPath);
                return new Project(fileSystem);
            } catch (DirectoryNotFoundException) {
                // Use Missing File System
                return null;
            } catch (FileNotFoundException) {
                // Use Missing File System
                return null;
            }
        }
    }
}
