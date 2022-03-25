namespace Vesuv.Core.IO
{
    public class InMemoryFileSystem : BaseFileSystem
    {
        private FileSystemState _fileSystemState;
        public override FileSystemState FileSystemState => _fileSystemState;

        public InMemoryFileSystem(string projectName)
        {
            var projectFile = new ProjectFile(this, DateTime.Now);
            projectFile.ProjectName = projectName;
            Files.Add(projectFile.RID, projectFile);
        }

        private void SetFileSystemState(FileSystemState fileSystemState)
        {
            if (_fileSystemState != fileSystemState) {
                _fileSystemState = fileSystemState;
                RaisePropertyChanged(nameof(FileSystemState));
            }
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
            if (other is not InMemoryFileSystem otherInMemoryFileSystem) {
                return false;
            }

            return ProjectFile.ProjectName.Equals(otherInMemoryFileSystem.ProjectFile.ProjectName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
