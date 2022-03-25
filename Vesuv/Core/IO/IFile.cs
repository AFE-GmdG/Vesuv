using System.IO;

namespace Vesuv.Core.IO
{
    public interface IFile
    {
        BaseFileSystem FileSystem { get; init; }

        Scheme Scheme { get; init; }
        UInt64 RID { get; init; }
        string Path { get; }
        string Name { get; }
        DateTime ModificationTime { get; }
        ResourceType ResourceType { get; set; }
        FileState FileState { get; }

        bool IsReadonly { get; }
        bool IsMissing { get; }
        bool IsModified { get; }

        Task<IFile> CopyAsync(string name);
        Task<IFile> CopyAsync(string path, string name);

        Task<IFile> MoveAsync(string name);
        Task<IFile> MoveAsync(string path, string name);

        Task<IFile> DeleteAsync();
    }
}
