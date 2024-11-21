using CommunityToolkit.Mvvm.ComponentModel;
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
		private bool shouldClose = false;
		[ObservableProperty]
		private bool shouldRemoveCart = false;
		[ObservableProperty]
		private bool shouldShowUndo = false;
		[ObservableProperty]
		private string text = string.Empty;
		[ObservableProperty]
		private int popupHeight = 50;
		private async void ClosePopup()
		{
			
			await Task.Delay(2000);
			if (!IsClosed)
			{
				ShouldClose = true;
			}

		}

		public PopupViewModel(object requestingModel, bool empty = false)
        {
			if (requestingModel is CheckoutViewModel && empty == true)
			{
				Text = "This action will Remove the item";

				PopupHeight = 100;
				shouldShowUndo = true;
			}
			else if (requestingModel is CheckoutViewModel)
			{
				Text = "This action will Remove ALL items";

				PopupHeight = 100;
				shouldRemoveCart = true;
			}
			else if(requestingModel is MedicationOverviewPageViewModel )
			{
				
				Text = "Item added to Cart!";
				ClosePopup();
			}
			else if (requestingModel is MedicationDetailsViewModel)
			{

				Text = "Item added to Cart!";
				ClosePopup();
			}
			


		}

    }
}
