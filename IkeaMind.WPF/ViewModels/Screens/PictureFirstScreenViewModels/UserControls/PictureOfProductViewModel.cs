using IkeaMind.Infrastructure;
using IkeaMind.WPF.Enums;
using IkeaMind.WPF.ViewModels.Base;
using System;
using System.Windows.Media.Imaging;

namespace IkeaMind.WPF.ViewModels.Screens.PictureFirstScreenViewModels.UserControls
{
    public class PictureOfProductViewModel : ViewModelBase
    {
        private IkeaProduct ikeaProduct;
        private DecisionStatusEnum status;

        public DecisionStatusEnum Status
        {
            set
            {
                status = value;
                OnPropertyChanged(nameof(ProductColor));
            }
        }

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

        public string ProductColor
        {
            get
            {
                switch (status)
                {
                    case DecisionStatusEnum.Right:
                        return "#00AB26";
                    case DecisionStatusEnum.Wrong:
                        return "#EE0000";
                    default:
                        return "#000000";
                }
            }
        }

        public void Refresh(IkeaProduct newProduct)
        {
            ikeaProduct = newProduct;
            OnPropertyChanged(nameof(ProductImageURI));

            status = DecisionStatusEnum.Neutral;
            OnPropertyChanged(nameof(ProductColor));
        }

        public PictureOfProductViewModel
            (IkeaProduct ikeaProduct)
        {
            this.ikeaProduct = ikeaProduct;
            status = DecisionStatusEnum.Neutral;
        }
    }
}
