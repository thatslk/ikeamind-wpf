using IkeaMind.Infrastructure;
using System;
using System.Windows.Media.Imaging;

namespace IkeaMind.WPF.ViewModels.Screens.NameFirstScreenViewModels.UserControls
{
    public class ProductPictureViewModel : AbstractProdcutViewModel
    {
        public override string ProductName => isHidden ? "???" : ikeaProduct.Name;
        public override string ProductCategory => ikeaProduct.Category;

        public BitmapImage ProductImageURI
        {
            get
            {
                if (ikeaProduct.Image == null)
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(ikeaProduct.ImageUrl);
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    return image;
                }
                else
                {
                    return ikeaProduct.Image;
                }
            }
        }


        public ProductPictureViewModel
            (IkeaProduct ikeaProduct, bool isHidden)
            : base(ikeaProduct, isHidden) { }
    }
}
