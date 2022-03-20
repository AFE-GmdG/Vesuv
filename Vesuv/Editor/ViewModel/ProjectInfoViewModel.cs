using Vesuv.Core;

namespace Vesuv.Editor.ViewModel
{
    public class ProjectInfoViewModel : BaseViewModel
    {

        private readonly ProjectInfo _projectInfo;

        public bool IsMissing {
            get => _projectInfo.IsMissing;
            set {
                if (_projectInfo.IsMissing != value) {
                    _projectInfo.IsMissing = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string ProjectName {
            get => _projectInfo.ProjectName;
            set {
                if (_projectInfo.ProjectName != value) {
                    _projectInfo.ProjectName = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string? ProjectDescription {
            get => _projectInfo.ProjectDescription;
            set {
                if (_projectInfo.ProjectDescription != value) {
                    _projectInfo.ProjectDescription = value;
                    RaisePropertyChanged();
                }
            }
        }

        public Version EngineVersion {
            get => _projectInfo.EngineVersion;
            set {
                if (_projectInfo.EngineVersion != value) {
                    _projectInfo.EngineVersion = value;
                    RaisePropertyChanged();
                }
            }
        }

        public DateTime ModifikationTime {
            get => _projectInfo.ModifikationTime;
            set {
                if (_projectInfo.ModifikationTime != value) {
                    _projectInfo.ModifikationTime = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ProjectInfoViewModel(ProjectInfo projectInfo)
        {
            _projectInfo = projectInfo;
        }
    }
}
