namespace Vesuv.Core.IO
{
    public class File : IFile
    {
        private ResourceType _resourceType;

        public BaseFileSystem FileSystem { get; init; }

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

        // TODO: Ask the FileSystem, if a file is readonly => Async?
        public virtual bool IsReadonly => (FileSystem.FileSystemState & (FileSystemState.Offline | FileSystemState.Missing)) != 0UL;

        // TODO: Ask the FileSystem, if this file is missing => Async?
        public virtual bool IsMissing { get; protected set; }

        // TODO: The file itself should know, if it was modified but not yet saved
        public virtual bool IsModified {  get; protected set; }

        internal File(BaseFileSystem fileSystem, Scheme scheme, UInt64 rid, string path, string name, DateTime modificationTime, FileState fileState)
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
