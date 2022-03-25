using System.IO;

namespace Vesuv.Core.IO
{

    /// <summary>
    /// The NativeFileSystem class implements the IFileSystem interface,
    /// mostly used while creating a game on the local disk.
    /// Vesuv use a sandboxed file system, composed of 2 parts:
    /// - A Res scheme, which is used to store the game contents.
    ///   It is only writeable while the editor is running. In a exported game, this part is readonly.
    ///   The root directory must contain the "project.vesuv" file which contains project relevant
    ///   configuration base settings.
    /// - A User scheme, which is used to store savegames and game specific config files.
    ///   This scheme is writeable even in a exported game. Its usually located below %LOCALAPPDATA%
    ///   folder.
    ///   Files with the same path and name but with scheme User have precedence over Res scheme.
    /// </summary>
    public class NativeFileSystem : BaseFileSystem
    {
        private readonly DirectoryInfo _resRoot;
        private readonly DirectoryInfo _userRoot;
        private FileSystemState _fileSystemState;
        public override FileSystemState FileSystemState => _fileSystemState;

        private NativeFileSystem(DirectoryInfo resRoot, DirectoryInfo userRoot, FileInfo projectFileInfo)
        {
            _resRoot = resRoot;
            _userRoot = userRoot;
            _fileSystemState = FileSystemState.Created;

            var projectFile = new ProjectFile(this, projectFileInfo.LastWriteTime);
            Files.Add(projectFile.RID, projectFile);
        }

        public static async Task<NativeFileSystem> InitializeNativeFileSystem(string resRootPath)
        {
            if (String.IsNullOrWhiteSpace(resRootPath)) {
                throw new ArgumentException("resRootPath is empty", nameof(resRootPath));
            }

            var resRoot = new DirectoryInfo(resRootPath);
            if (!resRoot.Exists) {
                throw new DirectoryNotFoundException($"Directory {resRootPath} not found.");
            }

            var projectName = resRoot.Name;

            // project.vesuv suchen
            var projectFileInfo = resRoot.GetFiles("project.vesuv", SearchOption.TopDirectoryOnly).FirstOrDefault();
            if (projectFileInfo == null) {
                throw new FileNotFoundException("Project not found.", Path.Combine(resRoot.FullName, "project.vesuv"));
            }

            // Info über Projekt incl. userRootPath ermitteln
            DirectoryInfo userRoot;
            using (var fs = projectFileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Write)) {
                var userRootPath = await fs.ReadIni("Application", "UserDirName");
                if (userRootPath == null) {
                    // use $(GlobalConfigPath)/Applications/$(ProjectName)
                    projectName = await fs.ReadIni("Application", "ProjectName") ?? projectName;
                    userRootPath = Path.Combine(Common.GlobalConfigPath, "Applications", projectName);
                }
                userRoot = new DirectoryInfo(userRootPath);
            }

            // ctor aufrufen
            var nativeFileSystem = new NativeFileSystem(resRoot, userRoot, projectFileInfo);

            return nativeFileSystem;
        }

        public override Task<IFile> CreateNewFileAsync(Scheme scheme, string path, string name, ResourceType resourceType)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(BaseFileSystem? other)
        {
            if (other == null) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            if (other is not NativeFileSystem otherNativeFileSystem) {
                return false;
            }

            return _resRoot.FullName.Equals(otherNativeFileSystem._resRoot.FullName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
