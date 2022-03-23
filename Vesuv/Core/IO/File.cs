namespace Vesuv.Core.IO
{
    public class File : IFile
    {
        private ResourceType _resourceType;

        public IFileSystem FileSystem { get; init; }

        public Scheme Scheme { get; init; }

        public UInt64 RID { get; init; }

        public virtual string Path { get; private set; }

        public virtual string Name { get; private set; }

        public virtual DateTime ModificationTime { get; private set; }

        public virtual ResourceType ResourceType {
            get => _resourceType;
            set {
                if (_resourceType != value) {
                    _resourceType = value;
                }
            }
        }

        public FileState FileState { get; protected set; }

        internal File(IFileSystem fileSystem, Scheme scheme, UInt64 rid, string path, string name, DateTime modificationTime, FileState fileState)
        {
            FileSystem = fileSystem;
            Scheme = scheme;
            RID = rid;
            Path = path;
            Name = name;
            ModificationTime = modificationTime;
            FileState = fileState;
        }

        public Task<IFile> CopyAsync(string name)
        {
            if (RID == 0UL) {
                throw new InvalidOperationException("project.vesuv can't be copied");
            }
            throw new NotImplementedException();
        }

        public Task<IFile> CopyAsync(string path, string name)
        {
            if (RID == 0UL) {
                throw new InvalidOperationException("project.vesuv can't be copied");
            }
            throw new NotImplementedException();
        }

        public Task<IFile> MoveAsync(string name)
        {
            if (RID == 0UL) {
                throw new InvalidOperationException("project.vesuv can't be moved or renamed");
            }
            throw new NotImplementedException();
        }

        public Task<IFile> MoveAsync(string path, string name)
        {
            if (RID == 0UL) {
                throw new InvalidOperationException("project.vesuv can't be moved or renamed");
            }
            throw new NotImplementedException();
        }

        public Task<IFile> DeleteAsync()
        {
            if (RID == 0UL) {
                throw new InvalidOperationException("project.vesuv can't be deleted");
            }
            throw new NotImplementedException();
        }
    }
}
