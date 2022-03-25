using System.IO;

namespace Vesuv.Core.IO
{
    public class MissingFileSystem : BaseFileSystem
    {
        private DirectoryInfo _missedProjectRootDirectory;
        public override FileSystemState FileSystemState => FileSystemState.Missing;

        public MissingFileSystem(string projectPath)
        {
            _missedProjectRootDirectory = new DirectoryInfo(projectPath);

            var projectFile = new ProjectFile(this, _missedProjectRootDirectory.Name);
            Files.Add(projectFile.RID, projectFile);
        }

        public override Task<IFile> CreateNewFileAsync(Scheme scheme, string path, string name, ResourceType resourceType)
        {
            throw new InvalidOperationException("Can't create a file on this file system.");
        }

        public override bool Equals(BaseFileSystem? other)
        {
            if (other == null) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            if (other is not MissingFileSystem otherMissingFileSystem) {
                return false;
            }

            return _missedProjectRootDirectory.FullName.Equals(otherMissingFileSystem._missedProjectRootDirectory.FullName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
