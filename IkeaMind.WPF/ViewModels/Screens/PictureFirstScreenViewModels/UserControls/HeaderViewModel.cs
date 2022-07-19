using IkeaMind.Infrastructure;

namespace IkeaMind.WPF.ViewModels.Screens.PictureFirstScreenViewModels.UserControls
{
    public class HeaderViewModel : AbstractHeaderViewModel
    {
        public HeaderViewModel(IkeaProduct ikeaProduct) : base(ikeaProduct) { }

        public override void Refresh(IkeaProduct ikeaProduct)
        {
            base.Refresh(ikeaProduct);
            OnPropertyChanged(nameof(CategoryName));
        }

        public string CategoryName =>
            ikeaProduct.Category;
    }
}