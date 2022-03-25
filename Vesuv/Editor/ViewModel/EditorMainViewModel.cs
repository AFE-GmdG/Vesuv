using Vesuv.Core.IO;

namespace Vesuv.Editor.ViewModel
{
    public class EditorMainViewModel : BaseViewModel
    {

        private readonly Project _project;

        public EditorMainViewModel(Project project)
        {
            _project = project;
        }
    }
}
