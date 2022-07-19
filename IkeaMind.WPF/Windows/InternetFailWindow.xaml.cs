using IkeaMind.WPF.ViewModels.Windows;
using System;
using System.Windows;

namespace IkeaMind.WPF.Windows
{
    public partial class InternetFailWindow : Window
    {
        private readonly Action exitAction;
        public InternetFailWindow(Action exitAction)
        {
            InitializeComponent();
            this.exitAction = exitAction;
            DataContext = new InternetFailViewModel(RetryHandler, ExitHandler);
        }

        public void ExitHandler()
        {
            exitAction();
            Close();
        }

        public void RetryHandler()
        {
            Close();
        }
    }
}
