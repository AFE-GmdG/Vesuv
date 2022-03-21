using System.IO;

namespace Vesuv.Core._Project
{
    public class PersistentProject : IProject
    {
        private bool _isModified;
        private FileInfo _projectFile;

        private string _name;
        private string? _description;
        private string? _author;
        private Version? _projectVersion;
        private Version _engineVersion;

        public bool IsReadonly { get; private set; }
        public bool IsMissing => false;
        public bool IsModified {
            get => _isModified;
            private set {
                if (_isModified == value) {
                    return;
                }
                _isModified = value;
                if (value) {
                    ProjectChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public DirectoryInfo? ProjectDirectory => _projectFile.Directory;

        public string Name {
            get => _name;
            set {
                if (_name != value) {
                    _name = value;
                    IsModified = true;
                }
            }
        }

        public string? Description {
            get => _description;
            set {
                if (_description != value) {
                    if (String.IsNullOrWhiteSpace(value)) {
                        _description = null;
                    } else {
                        _description = value;
                    }
                    IsModified = true;
                }
            }
        }

        public string? Author {
            get => _author;
            set {
                if (_author != value) {
                    if (String.IsNullOrWhiteSpace(value)) {
                        _author = null;
                    } else {
                        _author = value;
                    }
                    IsModified = true;
                }
            }
        }

        public Version? ProjectVersion {
            get => _projectVersion;
            set {
                if (_projectVersion != value) {
                    _projectVersion = value;
                    IsModified = true;
                }
            }
        }

        public Version EngineVersion => _engineVersion;

        public event EventHandler? ProjectChanged;
        public event EventHandler? ProjectSaved;
        public event EventHandler? ProjectReverted;

        private PersistentProject(DirectoryInfo projectDirectory, bool openReadonly)
        {
            if (!projectDirectory.Exists) {
                throw new DirectoryNotFoundException($"Directory {projectDirectory.FullName} not found.");
            }

            var projectFileInfo = projectDirectory.GetFiles("project.vesuv", SearchOption.TopDirectoryOnly).FirstOrDefault();
            if (projectFileInfo == null) {
                throw new FileNotFoundException("Project not found.", Path.Combine(projectDirectory.FullName, "project.vesuv"));
            }

            IsReadonly = openReadonly;
            _projectFile = projectFileInfo;

            _name = projectDirectory.Name;
            _engineVersion = new Version(0, 0, 0);
        }

        public static PersistentProject OpenProject(DirectoryInfo projectDirectory, bool openReadonly)
        {
            return new PersistentProject(projectDirectory, openReadonly);
        }

        public bool Equals(IProject? other)
        {
            if (other is not IProject otherProject) {
                return false;
            }
            if (ReferenceEquals(this, otherProject)) {
                return true;
            }

            if (otherProject is not PersistentProject otherPersistentProject) {
                return false;
            }

            return _projectFile.Equals(otherPersistentProject._projectFile);
        }
    }
}
