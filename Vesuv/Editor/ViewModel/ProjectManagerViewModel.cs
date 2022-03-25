using Vesuv.Core.IO;
using Vesuv.Editor.Commands;

namespace Vesuv.Editor.ViewModel
{
    public class ProjectManagerViewModel : BaseViewModel
    {
        private Project? _selectedProject;
        public Project? SelectedProject {
            get => _selectedProject;
            set {
                if (_selectedProject != value) {
                    _selectedProject = value;
                    RaisePropertyChanged();
                }
            }
        }

        public DelegateCommand NewProjectCommand { get; private init; }
        public DelegateCommand ImportProjectCommand { get; private init; }
        public DelegateCommand ScanForProjectsCommand { get; private init; }

        public DelegateCommand EditProjectCommand { get; private init; }
        public DelegateCommand RunProjectCommand { get; private init; }
        public DelegateCommand RenameProjectCommand { get; private init; }
        public DelegateCommand RemoveProjectCommand { get; private init; }
        public DelegateCommand RemoveMissingProjectsCommand { get; private init; }
        public DelegateCommand AboutCommand { get; private init; }

        public event EventHandler? NewProject;

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
            AboutCommand = new DelegateCommand(OnAbout);
        }

        private bool IsProjectSelected(object? obj)
        {
            return true;
        }

        private void OnNewProject(object? _)
        {
            SelectedProject = null;
            NewProject?.Invoke(this, new EventArgs());
        }

        private void OnImportProject(object? _) => throw new NotImplementedException();

        private void OnScanForProjects(object? _) => throw new NotImplementedException();

        private void OnEditProject(object? _) => throw new NotImplementedException();

        private void OnRunProject(object? _) => throw new NotImplementedException();

        private void OnRenameProject(object? _) => throw new NotImplementedException();

        private void OnRemoveProject(object? _) => throw new NotImplementedException();

        private void OnRemoveMissingProjects(object? _) => throw new NotImplementedException();

        private void OnAbout(object? _) => throw new NotImplementedException();
    }
}
