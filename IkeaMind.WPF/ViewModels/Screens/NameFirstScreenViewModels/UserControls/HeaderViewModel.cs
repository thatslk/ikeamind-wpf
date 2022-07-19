using IkeaMind.Infrastructure;

namespace IkeaMind.WPF.ViewModels.Screens.NameFirstScreenViewModels.UserControls
{
    public class HeaderViewModel : AbstractHeaderViewModel
    {
        public HeaderViewModel(IkeaProduct ikeaProduct) : base (ikeaProduct) { }

        public override void Refresh(IkeaProduct ikeaProduct)
        {
            base.Refresh(ikeaProduct);
            OnPropertyChanged(nameof(ProductName));
        }

        public string ProductName =>
            ikeaProduct.Name + " - это... ";
    }
}
