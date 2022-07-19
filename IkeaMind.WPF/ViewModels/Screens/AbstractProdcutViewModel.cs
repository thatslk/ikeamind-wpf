using IkeaMind.Infrastructure;
using IkeaMind.WPF.Enums;
using IkeaMind.WPF.ViewModels.Base;

namespace IkeaMind.WPF.ViewModels.Screens
{
    public abstract class AbstractProdcutViewModel : ViewModelBase
    {
        protected readonly IkeaProduct ikeaProduct;
        protected bool isHidden;
        protected DecisionStatusEnum status;

        public abstract string ProductName { get; }
        public abstract string ProductCategory { get; }
        public string Id => ikeaProduct.Article;
        public string PeekedProductName => ikeaProduct.Name;

        public bool IsHidden
        {
            set
            {
                isHidden = value;
                OnPropertyChanged(nameof(ProductCategory));
                OnPropertyChanged(nameof(ProductName));
            }
        }

        public DecisionStatusEnum Status
        {
            set
            {
                status = value;
                OnPropertyChanged(nameof(ProductColor));
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

        public AbstractProdcutViewModel
            (IkeaProduct ikeaProduct, bool isHidden)
        {
            this.ikeaProduct = ikeaProduct;
            this.isHidden = isHidden;
            status = DecisionStatusEnum.Neutral;
        }
    }
}
