using IkeaMind.WPF.Commands;
using IkeaMind.WPF.Enums;
using IkeaMind.WPF.ViewModels.Screens.PictureFirstScreenViewModels.UserControls;
using System;
using System.Threading;

namespace IkeaMind.WPF.ViewModels.Screens.PictureFirstScreenViewModels
{
    public class PictureFirstScreenViewModel : AbstractGameModeViewModel<ProductNameViewModel, HeaderViewModel>
    {
        private static PictureFirstScreenViewModel instance;
        private PictureOfProductViewModel productPicture;

        public PictureOfProductViewModel ProductPicture => productPicture;


        public static PictureFirstScreenViewModel CreateInstance(Action<EventWaitHandle> onInternetFailed)
        {
            if (instance == null)
                instance = new PictureFirstScreenViewModel(onInternetFailed);

            return instance;
        }

        public static PictureFirstScreenViewModel GetInstance()
        {
            return instance;
        }

        private PictureFirstScreenViewModel(Action<EventWaitHandle> onInternetFailed)
            : base(onInternetFailed) { }

        public override void AppendInitialElementsToObservableCollection()
        {
            base.AppendInitialElementsToObservableCollection();
            productPicture = new PictureOfProductViewModel(rightProduct);
        }

        public override void ShowNextProducts()
        {
            base.ShowNextProducts();
            productPicture.Refresh(rightProduct);
        }


        public override void RightProductDecisioned()
        {
            productPicture.Status = DecisionStatusEnum.Right;
            base.RightProductDecisioned();
        }

        public override void WrongProductDecisioned()
        {
            productPicture.Status = DecisionStatusEnum.Wrong;
            base.WrongProductDecisioned();
        }

    }
}
