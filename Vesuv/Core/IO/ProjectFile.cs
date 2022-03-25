using System.IO;

namespace Vesuv.Core.IO
{
    public class ProjectFile : File
    {

        private string? _projectName;

        public string ProjectName {
            get {
                if (_projectName == null) {
                    // Zuständigkeiten / LeseZeitpunkt / Asyncrones Property
                    // Die Eigenschaft (und weitere) könnten im ctor aus der physikalischen Datei gelesen werden.
                    // - Das ginge aber nur Syncron
                    // Ich könnte die Eigenschaft beim ersten Zugriff aus der physikalischen Datei lesen
                    // - Syncron oder dieses Property müsste asyncron werden.
                    // - Wird ein eigener FileStream für diese Property erzeugt oder kann er geshared werden?
                    // - Wer erzeugt den FileStream? Das FileSystem könnte auch ein RemoteFileSystem sein; diese Klasse soll davon nichts wissen.

                    // Ich glaube das Problem bezieht sich in Zukunft auf alle Dateien
                    throw new NotImplementedException();
                }
                return _projectName;
            }
            set {
                if (_projectName != value) {
                    _projectName = value;
                    FileState = FileState.Modified;
                }
            }
        }

        public override bool IsMissing => FileSystem.FileSystemState == FileSystemState.Missing;

        internal ProjectFile(BaseFileSystem fileSystem, DateTime modificationTime)
            : base(fileSystem, Scheme.Res, 0UL, "/", "project.vesuv", modificationTime, FileState.Saved)
        {
        }

        internal ProjectFile(MissingFileSystem fileSystem, string projectName)
            : base(fileSystem, Scheme.Res, 0UL, "/", "project.vesuv", DateTime.UnixEpoch, FileState.Saved)
        {
            _projectName = $"{projectName} (Missing Project)";
        }
    }
}
