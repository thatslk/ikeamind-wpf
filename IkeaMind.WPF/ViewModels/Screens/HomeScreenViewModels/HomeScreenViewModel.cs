using IkeaMind.WPF.ViewModels.Base;

namespace IkeaMind.WPF.ViewModels.Screens.HomeScreenViewModels
{
    public class HomeScreenViewModel : ViewModelBase
    {
        private static HomeScreenViewModel instance;
        private HomeScreenViewModel() { }

        public static HomeScreenViewModel GetInstance()
        {
            if (instance == null)
                instance = new HomeScreenViewModel();
            return instance;
        }
    }
}
