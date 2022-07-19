using IkeaMind.WPF.Commands.Base;
using IkeaMind.WPF.ViewModels.Base;
using System;

namespace IkeaMind.WPF.ViewModels.Windows
{
    public class ExitViewModel : ViewModelBase
    {
        public OneActionCommand ExitCommand { get; set; }
        public OneActionCommand StayCommand { get; set; }

        private readonly Random random;

        public string ExitImage => "./../Images/Exit" + random.Next(1, 3) + ".png";

        public bool Result { get; set; }

        public ExitViewModel(Action exit, Action stay)
        {
            ExitCommand = new OneActionCommand(exit);
            StayCommand = new OneActionCommand(stay);

            random = new Random();
        }
    }
}
