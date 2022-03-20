using System.IO;
using System.Windows;
using System.Windows.Navigation;

using Vesuv.Editor;
using Vesuv.Themes;

namespace Vesuv.Win32
{

    public partial class App
        : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Evaluate config parameter
            var defaultProjectPath = GlobalConfig.Instance.DefaultProjectPath;

            // Evaluate command line parameter
            if (e.Args.Length > 0) {
                var potentialProjectFile = e.Args[0]!;
                if (potentialProjectFile.EndsWith("project.vesuv", StringComparison.InvariantCultureIgnoreCase)) {
                    var projectFileInfo = new FileInfo(potentialProjectFile);
                    if (projectFileInfo.Exists && projectFileInfo.Name.Equals("project.vesuv", StringComparison.InvariantCultureIgnoreCase)) {
                        // Try open editor direct to this project...
                    }
                }
            }

            // Start with ProjectManagerWindow if necessary
            var projectManager = new ProjectManagerWindow();

            projectManager.Show();
        }

        //protected override void OnExit(ExitEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("OnExit");
        //    base.OnExit(e);
        //}

        //protected override void OnLoadCompleted(NavigationEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("OnLoadCompleted");
        //    base.OnLoadCompleted(e);
        //}

        //protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("OnSessionEnding");
        //    base.OnSessionEnding(e);
        //}
    }
}
