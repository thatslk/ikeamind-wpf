using IkeaMind.Infrastructure;

namespace IkeaMind.WPF.ViewModels.Screens.PictureFirstScreenViewModels.UserControls
{
    public class ProductNameViewModel : AbstractProdcutViewModel
    {

        public override string ProductName => ikeaProduct.Name;
        public override string ProductCategory => isHidden ? "???" : ikeaProduct.Category;

        public ProductNameViewModel
            (IkeaProduct ikeaProduct, bool isHidden)
            : base(ikeaProduct, isHidden) { }
    }
}
