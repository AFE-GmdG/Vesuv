using System.IO;

using Vesuv.Editor;

namespace Vesuv.Core.Project
{
    public class InMemoryProject : IProject
    {
        private DirectoryInfo? _projectDirectory;
        bool _isModified;

        private string _name;
        private string? _description;
        private string? _author;
        private Version? _projectVersion;
        private Version _engineVersion;

        public bool IsReadonly { get; private set; }
        public bool IsMissing { get; private set; }
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

        private InMemoryProject(string name, Version engineVersion)
        {
            _name = name;
            _engineVersion = engineVersion;
        }

        public static InMemoryProject OpenProject(DirectoryInfo projectDirectory)
        {
            if (!projectDirectory.Exists) {
                return new InMemoryProject(projectDirectory.Name, new Version(0, 0, 0)) {
                    _projectDirectory = projectDirectory,
                    IsReadonly = true,
                    IsMissing = true,
                    _isModified = false,
                    _description = "Project directory not found."
                };
            }

            var projectFile = projectDirectory.GetFiles("project.vesuv", SearchOption.TopDirectoryOnly).FirstOrDefault();
            if (projectFile == null) {
                return new InMemoryProject(projectDirectory.Name, new Version(0, 0, 0)) {
                    _projectDirectory = projectDirectory,
                    IsReadonly = true,
                    IsMissing = true,
                    _isModified = false,
                    _description = "Project directory is not a Vesuv project."
                };
            }

            throw new NotImplementedException();
        }

        public static InMemoryProject CreateProject(string name)
        {
            return new InMemoryProject(name, Common.CurrentEngineVersion) {
                IsReadonly = false,
                IsMissing = false,
                IsModified = false,
                _author = GlobalConfig.Instance.Author
            };
        }
    }
}
