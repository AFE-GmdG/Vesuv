using Vesuv.Core;
using Vesuv.Core._Project;

namespace Vesuv.Editor.ViewModel
{
    public class EditorMainViewModel : BaseViewModel
    {

        private readonly IProject _project;

        public EditorMainViewModel(IProject project)
        {
            _project = project;
        }
    }
}
