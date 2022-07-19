using IkeaMind.Core;
using IkeaMind.Infrastructure;
using IkeaMind.WPF.Commands;
using IkeaMind.WPF.Enums;
using IkeaMind.WPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace IkeaMind.WPF.ViewModels.Screens
{
    public abstract class AbstractGameModeViewModel<ProductType, HeaderType> : ViewModelBase
        where ProductType : AbstractProdcutViewModel
        where HeaderType: AbstractHeaderViewModel
    {
        protected bool isScreenAppearedBefore;
        public Random random = new Random();

        protected ObservableCollection<ProductType> products;
        protected ProductType selectedProduct;
        protected DecisionStatusEnum currentStatus;

        protected HeaderType header;
        protected IkeaProduct rightProduct;
        protected int totalScore = 0;
        protected int highScore = 0;

        public ApplySelectionCommand<ProductType> ApplySelectionCommand { get; set; }
        public ShowNextProductsCommand ShowNextProductsCommand { get; set; }


        public IEnumerable<ProductType> Products => products;
        public HeaderType Header => header;


        public int TotalScore
        {
            get { return totalScore; }
            set
            {
                if (value >= 0)
                    totalScore = value;

                HighScore = value;
                OnPropertyChanged(nameof(TotalScore));
                OnPropertyChanged(nameof(StatusColor));
                OnPropertyChanged(nameof(StatusText));
            }
        }

        public int HighScore
        {
            get => highScore;
            set
            {
                if (value > highScore)
                    highScore = value;

                OnPropertyChanged(nameof(HighScore));
            }
        }

        public int DebugQueueCount => IkeaFetcher.queue.Count();

        public ProductType SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        public DecisionStatusEnum CurrentStatus
        {
            set
            {
                currentStatus = value;
                OnPropertyChanged(nameof(CurrentStatus));
                OnPropertyChanged(nameof(TotalScore));
                OnPropertyChanged(nameof(StatusColor));
                OnPropertyChanged(nameof(StatusText));
            }
        }

        public string StatusColor => currentStatus switch
        {
            DecisionStatusEnum.Right => "#00AB26",
            DecisionStatusEnum.Wrong => "#EE0000",
            _ => "#00000000",
        };

        public string StatusText => currentStatus switch
        {
            DecisionStatusEnum.Right => "• Верно!",
            DecisionStatusEnum.Wrong => "• Неверно!",
            _ => "",
        };


        protected AbstractGameModeViewModel(Action<EventWaitHandle> onInternetFailed)
        {
            IkeaFetcher.InternetFailed = onInternetFailed;
            products = new ObservableCollection<ProductType>();

            IkeaFetcher.AppendProductsInQueue(9);
            OnPropertyChanged(nameof(DebugQueueCount));

            isScreenAppearedBefore = false;

            ShowNextProductsCommand = new ShowNextProductsCommand(ShowNextProducts);
            CurrentStatus = DecisionStatusEnum.Neutral;
        }

        public virtual void AppendInitialElementsToObservableCollection()
        {
            if (isScreenAppearedBefore == true)
                return;

            var randomRightPosition = random.Next(3);
            for (int i = 0; i < 3; i++)
            {
                var ikeaProduct = IkeaFetcher.GetNextProduct();
                OnPropertyChanged(nameof(DebugQueueCount));
                var productToAdd = (ProductType)Activator.CreateInstance(typeof(ProductType), new object[] { ikeaProduct, true });
                products.Add(productToAdd);
                if (randomRightPosition == i)
                    rightProduct = ikeaProduct;
            }

            
            header = (HeaderType)Activator.CreateInstance(typeof(HeaderType), new object[] { rightProduct });
            ApplySelectionCommand = new ApplySelectionCommand<ProductType>(rightProduct, RightProductDecisioned, WrongProductDecisioned);

            isScreenAppearedBefore = true;
        }

        public virtual void ShowNextProducts()
        {
            products.Clear();
            IkeaFetcher.AppendProductsInQueue(3);

            OnPropertyChanged(nameof(DebugQueueCount));

            var randomRightPosition = random.Next(3);
            for (int i = 0; i < 3; i++)
            {
                var ikeaProduct = IkeaFetcher.GetNextProduct();
                OnPropertyChanged(nameof(DebugQueueCount));
                var productToAdd = (ProductType)Activator.CreateInstance(typeof(ProductType), new object[] { ikeaProduct, true });
                products.Add(productToAdd);
                if (randomRightPosition == i)
                    rightProduct = ikeaProduct;
            }

            header.Refresh(rightProduct);
            ApplySelectionCommand.Refresh(rightProduct);
            CurrentStatus = DecisionStatusEnum.Neutral;
            ShowNextProductsCommand.SwitchExecutable();
        }

        public virtual void RightProductDecisioned()
        {
            foreach (var productVM in products)
            {
                productVM.IsHidden = false;
                if ((productVM.Id == rightProduct.Article) || (productVM.PeekedProductName == rightProduct.Name))
                    productVM.Status = DecisionStatusEnum.Right;
            }
            CurrentStatus = DecisionStatusEnum.Right;
            TotalScore++;
            ShowNextProductsCommand.SwitchExecutable();
        }

        public virtual void WrongProductDecisioned()
        {
            header.Status = DecisionStatusEnum.Wrong;
            foreach (var productVM in products)
            {
                productVM.IsHidden = false;
                if (productVM.Id == rightProduct.Article)
                    productVM.Status = DecisionStatusEnum.Right;
                if (productVM.Id == selectedProduct.Id)
                    productVM.Status = DecisionStatusEnum.Wrong;
            }
            CurrentStatus = DecisionStatusEnum.Wrong;
            TotalScore = 0;
            ShowNextProductsCommand.SwitchExecutable();
        }

    }
}
