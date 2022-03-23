using System.IO;

namespace Vesuv.Core.IO
{
    public interface IFile
    {
        IFileSystem FileSystem { get; init; }

        Scheme Scheme { get; init; }
        UInt64 RID { get; init; }
        string Path { get; }
        string Name { get; }
        DateTime ModificationTime { get; }
        ResourceType ResourceType { get; set; }
        FileState FileState { get; }

        Task<IFile> CopyAsync(string name);
        Task<IFile> CopyAsync(string path, string name);

        Task<IFile> MoveAsync(string name);
        Task<IFile> MoveAsync(string path, string name);

        Task<IFile> DeleteAsync();
    }
}
