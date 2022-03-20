using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;

using Vesuv.Core;
using Vesuv.Editor.Commands;

namespace Vesuv.Editor.ViewModel
{
    public class ProjectManagerViewModel : BaseViewModel
    {

        public Version EngineVersion {
            get {
                var attribute = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>();
                if (attribute != null) {
                    return new Version(attribute.Version);
                }
                return new Version(0, 0, 0);
            }
        }

        private ObservableCollection<ProjectInfoViewModel> _mruProjects;
        public ObservableCollection<ProjectInfoViewModel> MruProjects {
            get => _mruProjects;
            private set {
                if (_mruProjects != value) {
                    _mruProjects = value;
                    RaisePropertyChanged();
                }
            }
        }

        public DelegateCommand NewProjectCommand { get; private set; }
        public DelegateCommand ImportProjectCommand { get; private set; }
        public DelegateCommand ScanForProjectsCommand { get; private set; }

        public DelegateCommand EditProjectCommand { get; private set; }
        public DelegateCommand RunProjectCommand { get; private set; }
        public DelegateCommand RenameProjectCommand { get; private set; }
        public DelegateCommand RemoveProjectCommand { get; private set; }
        public DelegateCommand RemoveMissingProjectsCommand { get; private set; }

        public ProjectManagerViewModel()
        {
            NewProjectCommand = new DelegateCommand(OnNewProject);
            ImportProjectCommand = new DelegateCommand(OnImportProject);
            ScanForProjectsCommand = new DelegateCommand(OnScanForProjects);

            EditProjectCommand = new DelegateCommand(IsProjectSelected, OnEditProject);
            RunProjectCommand = new DelegateCommand(IsProjectSelected, OnRunProject);
            RenameProjectCommand = new DelegateCommand(IsProjectSelected, OnRenameProject);
            RemoveProjectCommand = new DelegateCommand(IsProjectSelected, OnRemoveProject);
            RemoveMissingProjectsCommand = new DelegateCommand(OnRemoveMissingProjects);

            if (IsDesignTime) {
                _mruProjects = new ObservableCollection<ProjectInfoViewModel>(new ProjectInfoViewModel[] {
                    new ProjectInfoViewModel(new ProjectInfo("Dummy Project 1", EngineVersion)),
                    new ProjectInfoViewModel(new ProjectInfo("Dummy Project 2", new Version(0, 4, 1))),
                    new ProjectInfoViewModel(new ProjectInfo(new DirectoryInfo("S:\\Work\\Vesuv\\Tutorial (02)"), true))
                });
            } else {
                _mruProjects = InitMruProjects();
            }

            RaisePropertyChanged(nameof(EngineVersion));
            RaisePropertyChanged(nameof(MruProjects));
        }

        private bool IsProjectSelected(object? obj)
        {
            return true;
        }

        private void OnNewProject(object? _)
        {
            Debug.WriteLine("Create a new Project");
        }

        private void OnImportProject(object? _) => throw new NotImplementedException();

        private void OnScanForProjects(object? _) => throw new NotImplementedException();

        private void OnEditProject(object? obj)
        {
            if (obj == null || obj is not ProjectInfo projectInfo) {
                return;
            }

            Debug.WriteLine("Open Project: {0:s}", new Object[] { projectInfo.ProjectName });
        }

        private void OnRunProject(object? _) => throw new NotImplementedException();

        private void OnRenameProject(object? _) => throw new NotImplementedException();

        private void OnRemoveProject(object? _) => throw new NotImplementedException();

        private void OnRemoveMissingProjects(object? _) => throw new NotImplementedException();

        private static ObservableCollection<ProjectInfoViewModel> InitMruProjects()
        {
            throw new NotImplementedException();
            //var projectInfos = GlobalConfig.Instance.MruProjects.Select(projectPath => {
            //    var projectDirectoryInfo = new DirectoryInfo(projectPath);
            //    if (!projectDirectoryInfo.Exists) {
            //        return new ProjectInfoViewModel(new ProjectInfo(projectDirectoryInfo, true));
            //    }
            //    var projectFile = projectDirectoryInfo.GetFiles("project.vesuv", SearchOption.TopDirectoryOnly).FirstOrDefault();
            //    if (projectFile == null) {
            //        return new ProjectInfoViewModel(new ProjectInfo(projectDirectoryInfo, true));
            //    }
            //    return new ProjectInfoViewModel(new ProjectInfo(new ProjectFile(projectFile)));
            //});
            //return new ObservableCollection<ProjectInfoViewModel>(projectInfos);
        }

    }
}
