using IkeaMind.WPF.ViewModels.Windows;
using System.ComponentModel;
using System.Windows;

namespace IkeaMind.WPF.Windows
{
    public partial class ExitWindow : Window
    {
        private static bool dialogResult = true;

        public ExitWindow()
        {
            InitializeComponent();
            Closing += CloseWindowHandler;
            DataContext = new ExitViewModel(ExitHandler, StayHandler);
        }

        public void ExitHandler()
        {
            dialogResult = false;
            Close();
        }

        public void StayHandler()
        {
            dialogResult = true;
            Close();
        }

        public void CloseWindowHandler(object sender, CancelEventArgs e)
        {
            if (dialogResult == false)
                DialogResult = false;
            else
            {
                e.Cancel = false;
                DialogResult = true;
            }
        }
    }
}
