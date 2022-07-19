using IkeaMind.WPF.ViewModels.Base;

namespace IkeaMind.WPF.Stores
{
    public class NavigationStore
    {
        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;
            }
        }

    }
}
