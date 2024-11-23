using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.ViewModels.Checkout.CheckoutViewModels;
using PharmacyShop.ViewModels.MedicationDetails;
using PharmacyShop.ViewModels.MedicationOverview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels
{
	public partial class PopupViewModel :ObservableObject
	{
		[ObservableProperty]
		private bool isClosed = false;
		[ObservableProperty]
		private bool removeCartButton = false;
		[ObservableProperty]
		private bool shouldClose = false;
		[ObservableProperty]
		private bool shouldRemoveCart = false;
		[ObservableProperty]
		private string text = string.Empty;
		[ObservableProperty]
		private int popupHeight = 50;
		private object clearCart;

        [RelayCommand]
		public async void RemoveCart()
		{
            RemoveCartButton = true;
			if (clearCart is CheckoutViewModel cvm)
			{
				cvm.EmptyCart();
				ClosePopup(true);
			}
        }




        private async void ClosePopup(bool saveButtonClicked = false)
		{
			if(!saveButtonClicked)
                await Task.Delay(2000);


            //await Task.Delay(2000);
			if (!IsClosed)
			{
				ShouldClose = true;
			}

		}

		public PopupViewModel(object requestingModel, bool empty = false)
        {
			if (requestingModel is CheckoutViewModel)
			{
				if (empty == true)
					Text = "This action will Remove the item";
				else
                    Text = "This action will Remove ALL items";

                shouldRemoveCart = true;
				PopupHeight = 100;
				clearCart = requestingModel;

            }
			else if(requestingModel is MedicationOverviewPageViewModel || requestingModel is MedicationDetailsViewModel)
			{
				
				Text = "Item added to Cart!";
				ClosePopup();
			}
		}
    }
}
