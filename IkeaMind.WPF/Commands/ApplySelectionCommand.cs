using IkeaMind.Infrastructure;
using IkeaMind.WPF.Commands.Base;
using IkeaMind.WPF.ViewModels.Screens;
using System;

namespace IkeaMind.WPF.Commands
{
    public class ApplySelectionCommand<ProductType> : CommandBase
        where ProductType : AbstractProdcutViewModel
    {
        private IkeaProduct rightProduct;
        private readonly Action rightDecisioned;
        private readonly Action wrongDecisioned;
        private bool isDecisioned;
        public ApplySelectionCommand(IkeaProduct rightProduct, Action rightDecisioned, Action wrongDecisioned)
        {
            this.rightProduct = rightProduct;
            this.rightDecisioned = rightDecisioned;
            this.wrongDecisioned = wrongDecisioned;
            isDecisioned = false;
        }
        public override void Execute(object parameter)
        {
            var selectedProduct = parameter as ProductType;
            if (selectedProduct != null)
            {
                if ((selectedProduct.Id == rightProduct.Article) || (selectedProduct.PeekedProductName == rightProduct.Name))
                    rightDecisioned();
                else
                    wrongDecisioned();
                isDecisioned = true;
                OnCanExecuteChanged();
            }
        }

        public void Refresh(IkeaProduct rightProduct)
        {
            this.rightProduct = rightProduct;
            isDecisioned = false;
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object parameter)
        {
            return !isDecisioned;
        }
    }
}
