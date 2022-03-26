using System.IO;

namespace Vesuv.Core.IO
{
    public class Project : IEquatable<Project>
    {
        private readonly BaseFileSystem _fileSystem;

        public ProjectFile ProjectFile => _fileSystem.ProjectFile;

        private Project(BaseFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public static async Task<Project> OpenProject(string projectPath)
        {
            try {
                await Task.Delay(3000);
                var fileSystem = await NativeFileSystem.InitializeNativeFileSystem(projectPath);
                return new Project(fileSystem);
            } catch (DirectoryNotFoundException) {
                // The directory was not fount => Missing project.
                var fileSystem = new MissingFileSystem(projectPath);
                return new Project(fileSystem);
            } catch (FileNotFoundException) {
                // The directory was found but there is no project.vesuv file => Missing Project
                var fileSystem = new MissingFileSystem(projectPath);
                return new Project(fileSystem);
            }
        }

        public static Project CreateNewProject(string projectName)
        {
            var fileSystem = new InMemoryFileSystem(projectName);
            return new Project(fileSystem);
        }

        public bool Equals(Project? other)
        {
            if (other == null) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            return _fileSystem.Equals(other._fileSystem);
        }
    }
}
