using IkeaMind.WPF.Stores;
using IkeaMind.WPF.ViewModels;
using IkeaMind.WPF.ViewModels.Screens.HomeScreenViewModels;
using IkeaMind.WPF.Windows;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace IkeaMind.WPF
{
    public partial class App : Application
    {
        private readonly NavigationStore navigationStore;
        private bool isInternetFailWindowOpened = false;
        private bool isClosed = false;
        private InternetFailWindow internetFailWindow;

        public App()
        {
            navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            navigationStore.CurrentViewModel = HomeScreenViewModel.GetInstance();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore, InternetFailed)
            };
            MainWindow.Closing += Closing;
            MainWindow.Show();

            base.OnStartup(e);
        }

        private void Closing(object sender, CancelEventArgs e)
        {
            ExitWindow ew = new ExitWindow();
            ew.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var res = ew.ShowDialog().Value;

            if (res == false && internetFailWindow != null)
            {
                isClosed = true;
                internetFailWindow.Close();
            }

            e.Cancel = res;
        }

        private void InternetFailed(EventWaitHandle ewh)
        {
            Current.Dispatcher.Invoke(() =>
            {
                if (!isInternetFailWindowOpened && !isClosed && MainWindow != null)
                {
                    internetFailWindow = new InternetFailWindow(MainWindow.Close);
                    internetFailWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    isInternetFailWindowOpened = true;
                    internetFailWindow.ShowDialog();
                    if (isClosed)
                        return;
                    isInternetFailWindowOpened = false;
                    ewh.Set();
                }
            });
        }
    }
}
