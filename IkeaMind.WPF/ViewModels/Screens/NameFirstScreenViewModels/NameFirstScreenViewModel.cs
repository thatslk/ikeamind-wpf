using IkeaMind.WPF.Commands;
using IkeaMind.WPF.Enums;
using IkeaMind.WPF.ViewModels.Screens.NameFirstScreenViewModels.UserControls;
using System;
using System.Threading;

namespace IkeaMind.WPF.ViewModels.Screens.NameFirstScreenViewModels
{
    public class NameFirstScreenViewModel : AbstractGameModeViewModel<ProductPictureViewModel, HeaderViewModel>
    {
        private static NameFirstScreenViewModel instance;

        public static NameFirstScreenViewModel CreateInstance(Action<EventWaitHandle> onInternetFailed)
        {
            if (instance == null)
                instance = new NameFirstScreenViewModel(onInternetFailed);

            return instance;
        }

        public static NameFirstScreenViewModel GetInstance()
        {
            return instance;
        }

        private NameFirstScreenViewModel(Action<EventWaitHandle> onInternetFailed)
            : base(onInternetFailed) { }

        public override void AppendInitialElementsToObservableCollection()
        {
            base.AppendInitialElementsToObservableCollection();
        }

        public override void ShowNextProducts()
        {
            base.ShowNextProducts();
        }


        public override void RightProductDecisioned()
        {
            base.RightProductDecisioned();
        }

        public override void WrongProductDecisioned()
        {
            base.WrongProductDecisioned();
        }

    }
}
