using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using Vesuv.Core.IO;
using Vesuv.Editor.ViewModel;

namespace Vesuv.Editor
{
    public partial class ProjectManagerLocalProjects : UserControl
    {
        private static readonly DependencyPropertyKey IsBusyPropertyKey = DependencyProperty.RegisterReadOnly(
            "IsBusy",
            typeof(bool),
            typeof(ProjectManagerLocalProjects),
            new PropertyMetadata(false));
        public readonly DependencyProperty IsBusyProperty = IsBusyPropertyKey.DependencyProperty;
        public bool IsBusy => (bool)GetValue(IsBusyProperty);

        public ProjectManagerLocalProjects()
        {
            InitializeComponent();
        }

        private bool ProjectFilter(object obj)
        {
            if (obj is not Project project) {
                return false;
            }

            if (String.IsNullOrWhiteSpace(filterTextBox.Text)) {
                return true;
            }

            return project.ProjectFile.ProjectName.IndexOf(filterTextBox.Text, StringComparison.OrdinalIgnoreCase) > -1;
        }

        private void ProjectManagerLocalProjects_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is not ProjectManagerViewModel viewModel) {
                return;
            }

            var view = CollectionViewSource.GetDefaultView(projectList.ItemsSource);
            view.Filter = ProjectFilter;

            viewModel.NewProject += OnNewProject;

            SetValue(IsBusyPropertyKey, true);
            viewModel.InitializeProjects().GetAwaiter().OnCompleted(() => {
                SetValue(IsBusyPropertyKey, false);
            });
        }
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(projectList.ItemsSource).Refresh();
        }

        private void OnNewProject(object? sender, EventArgs e)
        {
            var pmNewProjectViewModel = new ProjectManagerNewProjectViewModel();
            var pmNewProjectDialog = new ProjectManagerNewProjectDialog {
                Owner = Window.GetWindow(this),
                DataContext = pmNewProjectViewModel,
            };
            System.Diagnostics.Debug.WriteLine(pmNewProjectDialog.ShowDialog());
        }

    }
}
