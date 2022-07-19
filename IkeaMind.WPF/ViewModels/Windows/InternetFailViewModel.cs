using IkeaMind.WPF.Commands.Base;
using IkeaMind.WPF.ViewModels.Base;
using System;

namespace IkeaMind.WPF.ViewModels.Windows
{
    public class InternetFailViewModel : ViewModelBase
    {
        public OneActionCommand Retry { get; set; }
        public OneActionCommand Exit { get; set; }

        public InternetFailViewModel(Action retry, Action exit)
        {
            Retry = new OneActionCommand(retry);
            Exit = new OneActionCommand(exit);
        }
    }
}
