using IkeaMind.Infrastructure;
using IkeaMind.WPF.Enums;
using IkeaMind.WPF.ViewModels.Base;

namespace IkeaMind.WPF.ViewModels.Screens
{
    public class AbstractHeaderViewModel : ViewModelBase
    {
        protected IkeaProduct ikeaProduct;
        protected DecisionStatusEnum status;

        public AbstractHeaderViewModel(IkeaProduct ikeaProduct)
        {
            Refresh(ikeaProduct);
        }

        public virtual void Refresh(IkeaProduct ikeaProduct)
        {
            this.ikeaProduct = ikeaProduct;
            status = DecisionStatusEnum.Neutral;
            OnPropertyChanged(nameof(GuyImagePath));
        }


        public DecisionStatusEnum Status
        {
            set
            {
                status = value;
                OnPropertyChanged(nameof(GuyImagePath));
            }
        }

        public string GuyImagePath =>
            status == DecisionStatusEnum.Neutral || status == DecisionStatusEnum.Right
            ? "./../../../../Images/GuyDefault.png"
            : "./../../../../Images/GuyWrong.png";
    }

    
}
