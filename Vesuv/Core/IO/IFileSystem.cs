namespace Vesuv.Core.IO
{
    public interface IFileSystem
    {
        Dictionary<UInt64, IFile> Files { get; init; }
        FileSystemState FileSystemState { get; }

        ProjectFile ProjectFile { get; } 

        event EventHandler<FileChangedEventArgs>? FileChanged;

        Task<IFile> CreateNewFileAsync(Scheme scheme, string path, string name, ResourceType resourceType);
    }
}
