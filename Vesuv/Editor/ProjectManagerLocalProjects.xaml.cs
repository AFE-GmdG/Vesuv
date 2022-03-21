using System.Windows;
using System.Windows.Controls;

using Vesuv.Editor.ViewModel;

namespace Vesuv.Editor
{
    public partial class ProjectManagerLocalProjects : UserControl
    {
        public ProjectManagerLocalProjects()
        {
            InitializeComponent();
        }

        private void ProjectManagerLocalProjects_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is not ProjectManagerViewModel viewModel) {
                return;
            }

            viewModel.NewProject += OnNewProject;
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
