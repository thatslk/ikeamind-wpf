using IkeaMind.WPF.Commands.Base;
using System;

namespace IkeaMind.WPF.Commands
{
    public class ShowNextProductsCommand : CommandBase
    {
        private readonly Action refresh;
        private bool isEnabled;

        public ShowNextProductsCommand(Action refresh)
        {
            this.refresh = refresh;
            isEnabled = false;
        }

        public override void Execute(object parameter)
        {
            refresh();
        }

        public void SwitchExecutable()
        {
            isEnabled = !isEnabled;
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object parameter)
        {
            return isEnabled;
        }
    }
}
