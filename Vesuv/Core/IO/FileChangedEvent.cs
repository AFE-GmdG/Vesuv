namespace Vesuv.Core.IO
{
    public class FileChangedEventArgs : EventArgs
    {
        public IFile File { get; init; }
        public FileChangedReason Reason { get; init; }

        public FileChangedEventArgs(IFile file, FileChangedReason reason)
        {
            File = file;
            Reason = reason;
        }
    }

    public delegate void FileChangedEventHandler(object? sender, FileChangedEventArgs args);
}
