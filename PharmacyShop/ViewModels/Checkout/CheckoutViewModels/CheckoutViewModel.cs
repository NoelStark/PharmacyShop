using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;
using PharmacyShop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels.Checkout.CheckoutViewModels
{
	public partial class CheckoutViewModel : ObservableObject
	{
		private readonly MedicineService _medicineService;
		private readonly PersonService _personService;

		private Cart? currentItem {  get; set; } = null;
		
		/// <summary>
		/// A method that is run every time the View is shown to update
		/// when new items and such are added
		/// </summary>
		public async void Reinitialize()
		{
			
			CartList = new ObservableCollection<Cart>(_personService.ItemsCart);
			OnPropertyChanged(nameof(CartList));
			//If the cart isnt empty, one can move on to the next step through the 'Next' button
			if (!CartList.Any())
				CanExecute = false;
			ContinueCommand.NotifyCanExecuteChanged();
			
			UpdateTotalPrice();
		}

        public void EmptyCart()
        {
            if (CartList.Any())
            {
				if(currentItem != null)
				{
                    CartList.Remove(currentItem);
                    _personService.ItemsCart.Remove(currentItem);
					currentItem = null;
                }
				else
				{
					CartList.Clear();
					_personService.ItemsCart.Clear();
				}
				UpdateTotalPrice();
            }
        }

        public CheckoutViewModel(MedicineService medicineService, PersonService personService)
        {
			_personService = personService;
			_medicineService = medicineService;
			Reinitialize();
		}
		/// <summary>
		/// The method that calculates the total price for all the items
		/// </summary>
		void UpdateTotalPrice()
		{
			TotalPrice = 0;
			int amountOfItems = 0;

			//For loop for each and every item in the cart
			foreach (Cart cart in CartList)
			{
				//If-statement that calculates total price and also how many items that exists to display to UI
				if(cart.Information != null)
				{
					cart.TotalItemsPrice = cart.Quantity * cart.Information.ItemPrice;
					TotalPrice += cart.TotalItemsPrice;
					amountOfItems += cart.Quantity;
				}
			}
			//Updates the UI and saves the shipping cost and total cost for all the items
			if(CartList.Any())
                ShippingCost = TotalPriceWithoutShipping > 500 ? 0 : 29;
			else
                ShippingCost = 0;
            


            TotalAmountOfItems = amountOfItems;

			//ShippingCost = TotalCartCost > 500 ? 0 : 29;

			OnPropertyChanged(nameof(TotalPriceWithoutShipping));
			OnPropertyChanged(nameof(TotalCartCost));
			_personService.ShippingCost = ShippingCost;
			_personService.TotalCartCost = TotalCartCost;
		}

	}
}
