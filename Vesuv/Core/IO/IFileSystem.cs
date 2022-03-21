namespace Vesuv.Core.IO
{
    public interface IFileSystem
    {
        Dictionary<UInt64, IFile> Files { get; init; }

        event FileChangedEventHandler FileChanged;

        Task<IFile> CreateNewFileAsync(Scheme scheme, string path, string name, ResourceType resourceType);
    }
}
