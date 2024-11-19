using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels.Checkout.CheckoutViewModels
{
	public partial class CheckoutViewModel : ObservableObject
	{
		[RelayCommand]
		void IncreaseQuantity(Cart cartItem)
		{
			cartItem.Quantity++;
			UpdateTotalPrice();
		}

		[RelayCommand]
		void DecreaseQuantity(Cart cartItem)
		{
			if (cartItem.Quantity > 0)
			{
				cartItem.Quantity--;
				if (cartItem.Quantity == 0)
				{
					CartList.Remove(cartItem);
					_personService.ItemsCart.Remove(cartItem);
					if (!CartList.Any())
					{
						CanExecute = false;

					}
					else
					{
						CanExecute = true;
						ContinueCommand.NotifyCanExecuteChanged();
					}
				}
				UpdateTotalPrice();
			}
		}

		[RelayCommand(CanExecute = nameof(CanExecute))]
		void Continue()
		{
			Shell.Current.GoToAsync($"//PersonalInfoPage?totalCartCost={TotalCartCost}");
		}
	}
}
