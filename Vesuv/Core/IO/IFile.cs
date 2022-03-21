using System.IO;

namespace Vesuv.Core.IO
{
    public interface IFile
    {
        Scheme Scheme { get; init; }
        UInt64 RID { get; init; }
        string Path { get; set; }
        string Name { get; set; }
        DateTime ModificationTime { get; set; }
        ResourceType ResourceType { get; set; }
        FileState FileState { get; set; }

        Task<IFile> CopyAsync(string name);
        Task<IFile> CopyAsync(string path, string name);

        Task<IFile> MoveAsync(string name);
        Task<IFile> MoveAsync(string path, string name);

        Task<IFile> DeleteAsync();
    }
}
