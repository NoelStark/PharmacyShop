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
		

		public void Reinitialize()
		{

			CartList = new ObservableCollection<Cart>(_personService.ItemsCart);
			OnPropertyChanged(nameof(CartList));
			if (CartList.Any())
			{
				CanExecute = true;
			}
			UpdateTotalPrice();
		}

		public CheckoutViewModel(MedicineService medicineService, PersonService personService)
        {
			_personService = personService;
			_medicineService = medicineService;
			Reinitialize();
		}

		void UpdateTotalPrice()
		{
			TotalPrice = 0;
			int amountOfItems = 0;

			foreach (Cart cart in CartList)
			{
				if(cart.Information != null)
				{
					cart.TotalItemsPrice = cart.Quantity * cart.Information.ItemPrice;
					TotalPrice += cart.TotalItemsPrice;
					amountOfItems += cart.Quantity;
				}
			}
			TotalAmountOfItems = amountOfItems;

			ShippingCost = TotalCartCost > 500 ? 0 : 29;

			OnPropertyChanged(nameof(TotalPriceWithoutShipping));
			OnPropertyChanged(nameof(TotalCartCost));
			_personService.ShippingCost = ShippingCost;
			_personService.TotalCartCost = TotalCartCost;
		}

	}
}
