using System;

namespace IkeaMind.WPF.Commands.Base
{
    public class OneActionCommand : CommandBase
    {
        private readonly Action action;

        public OneActionCommand(Action action)
        {
            this.action = action;
        }

        public override void Execute(object parameter)
        {
            action();
        }
    }
}
