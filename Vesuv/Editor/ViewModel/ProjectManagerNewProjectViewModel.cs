using System.IO;

using Vesuv.Editor.Commands;

namespace Vesuv.Editor.ViewModel
{
    public class ProjectManagerNewProjectViewModel : BaseViewModel
    {
        private string _projectName;
        private string _projectPath;

        public DelegateCommand CreateFolderCommand { get; private init; }
        public DelegateCommand BrowseCommand { get; private init; }
        public DelegateCommand CancelCommand { get; private init; }
        public DelegateCommand CreateNewProjectCommand { get; private init; }

        public string ProjectName {
            get => _projectName;
            set {
                if (_projectName != value) {
                    _projectName = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string ProjectPath {
            get => _projectPath;
            set {
                if (_projectPath != value) {
                    _projectPath = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(IsProjectPathEmpty));
                }
            }
        }

        public bool IsProjectPathEmpty {
            get {
                if (String.IsNullOrWhiteSpace(_projectPath)) {
                    return false;
                }
                try {
                    var directoryInfo = new DirectoryInfo(_projectPath);
                    if (!directoryInfo.Exists) {
                        return false;
                    }
                    return directoryInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly).Length == 0;
                } catch {
                    return false;
                }
            }
        }

        public ProjectManagerNewProjectViewModel()
        {
            CreateFolderCommand = new DelegateCommand(CanCreateFolder, OnCreateFolder);
            BrowseCommand = new DelegateCommand(OnBrowseCommand);
            CancelCommand = new DelegateCommand(OnCancel);
            CreateNewProjectCommand = new DelegateCommand(CanCreateNewProject, OnCreateNewProject);

            _projectName = "";
            _projectPath = GlobalConfig.Instance.DefaultProjectPath ?? "";

            RaisePropertyChanged(nameof(ProjectName));
            RaisePropertyChanged(nameof(ProjectPath));
            RaisePropertyChanged(nameof(IsProjectPathEmpty));
        }

        private bool CanCreateFolder(object? _)
        {
            return false;
        }

        private void OnCreateFolder(object? _)
        {
        }

        private void OnBrowseCommand(object? _)
        {
        }

        private void OnCancel(object? _)
        {
        }

        private bool CanCreateNewProject(object? _)
        {
            return false;
        }

        private void OnCreateNewProject(object? _)
        {
        }
    }
}
