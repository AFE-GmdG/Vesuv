using System.Collections.ObjectModel;

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

        public ObservableCollection<Project> LocalProjects { get; init; }

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
            LocalProjects = new ObservableCollection<Project>();

            if (IsDesignTime) {
                LocalProjects.Add(Project.CreateNewProject("Testproject #1"));
                LocalProjects.Add(Project.CreateNewProject("Testproject #2"));
                LocalProjects.Add(Project.CreateNewProject("Another Project"));
            }

            NewProjectCommand = new DelegateCommand(OnNewProject);
            ImportProjectCommand = new DelegateCommand(CanImportProject, OnImportProject);
            ScanForProjectsCommand = new DelegateCommand(OnScanForProjects);

            EditProjectCommand = new DelegateCommand(IsProjectSelected, OnEditProject);
            RunProjectCommand = new DelegateCommand(IsProjectSelected, OnRunProject);
            RenameProjectCommand = new DelegateCommand(IsProjectSelected, OnRenameProject);
            RemoveProjectCommand = new DelegateCommand(IsProjectSelected, OnRemoveProject);
            RemoveMissingProjectsCommand = new DelegateCommand(OnRemoveMissingProjects);
            AboutCommand = new DelegateCommand(OnAbout);
        }

        public async Task InitializeProjects()
        {
            var OpenProjectTasks = new List<Task<Project>>();
            foreach (var projectPath in GlobalConfig.Instance.MruProjects) {
                OpenProjectTasks.Add(Project.OpenProject(projectPath));
            }
            var projects = await Task.WhenAll(OpenProjectTasks);
            LocalProjects.Clear();
            foreach (var project in projects) {
                LocalProjects.Add(project);
            }
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

        private bool CanImportProject(object? _)
        {
            return false;
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
