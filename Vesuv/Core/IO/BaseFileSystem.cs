using System.IO;

using Vesuv.Editor.ViewModel;

namespace Vesuv.Core.IO
{
    public abstract class BaseFileSystem : BaseViewModel, IEquatable<BaseFileSystem>
    {
        public Dictionary<UInt64, IFile> Files { get; private init; }

        public abstract FileSystemState FileSystemState { get; }

        public ProjectFile ProjectFile {
            get {
                if (!Files.TryGetValue(0UL, out var file) || (file is not ProjectFile projectFile)) {
                    throw new FileNotFoundException("Project file not found", "vesuv.project");
                }
                return projectFile;
            }
        } 

        public event EventHandler<FileChangedEventArgs>? FileChanged;

        public BaseFileSystem()
        {
            Files = new Dictionary<UInt64, IFile>();
        }

        public abstract Task<IFile> CreateNewFileAsync(Scheme scheme, string path, string name, ResourceType resourceType);
        public abstract bool Equals(BaseFileSystem? other);

        protected void RaiseFileChanged(UInt64 rid, FileChangedEventArgs args)
        {
            if (!Files.TryGetValue(0UL, out var file)) {
                throw new FileNotFoundException($"File with RID {rid} not found.");
            }
            FileChanged?.Invoke(this, args);
        }
    }
}
