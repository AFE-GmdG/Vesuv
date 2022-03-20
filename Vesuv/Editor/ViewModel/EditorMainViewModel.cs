using Vesuv.Core;

namespace Vesuv.Editor.ViewModel
{
    public class EditorMainViewModel : BaseViewModel
    {

        private readonly ProjectFile _projectFile;

        private ProjectInfoViewModel _projectInfo;
        public ProjectInfoViewModel ProjectInfo {
            get => _projectInfo;
            set {
                if (_projectInfo != value) {
                    _projectInfo = value;
                    RaisePropertyChanged();
                }
            }
        }

        public EditorMainViewModel(ProjectFile projectFile)
        {
            _projectFile = projectFile;
            var projectInfo = new ProjectInfo(projectFile);
            _projectInfo = new ProjectInfoViewModel(projectInfo);
        }
    }
}
