namespace Vesuv.Editor.ViewModel
{
    public class UCPlaygroundViewModel : BaseViewModel
    {

        private string? _name;
        public string? Name {
            get => _name;
            set {
                if (_name != value) {
                    _name = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _isToggled;
        public bool IsToggled {
            get => _isToggled;
            set {
                if (value != _isToggled) {
                    _isToggled = value;
                    RaisePropertyChanged();
                }
            }
        }

        public UCPlaygroundViewModel()
        {
            _name = "Andreas";
            _isToggled = false;
        }
    }
}
