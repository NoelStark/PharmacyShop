﻿using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;
using PharmacyShop.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels.Checkout.CheckoutViewModels
{
	public partial class CheckoutViewModel : ObservableObject
	{
		/// <summary>
		/// This method Increases the Quantity
		/// of a specific cartItem
		/// </summary>
		/// <param name="cartItem">Parameter that takes the Cart item that is selected/pressed</param>
		[RelayCommand]
		void IncreaseQuantity(Cart cartItem)
		{
			cartItem.Quantity++;
			//Whenever something is Added/Removed, the totalprice is updated
			UpdateTotalPrice();
		}


		/// <summary>
		/// This method Decreases the Quantity
		/// of a specific cartItem
		/// </summary>
		/// <param name="cartItem">Parameter that takes the Cart item that is selected/pressed</param>
		[RelayCommand]
		void DecreaseQuantity(Cart cartItem)
		{
			if (cartItem.Quantity > 0)
			{
				cartItem.Quantity--;
				//If the quantity reaches 0, the item is removed from the cart
				if (cartItem.Quantity == 0)
				{
					CartList.Remove(cartItem);
					_personService.ItemsCart.Remove(cartItem);
					//If the cart is empty, one shouldnt be able to press the 'Next' button
					if (!CartList.Any())
					{
						PopupView? popup = new PopupView(this);
						Application.Current?.MainPage?.ShowPopup(popup);
						CanExecute = false;
					}
					else
					{
						CanExecute = true;
					}
					ContinueCommand.NotifyCanExecuteChanged();
				}
				//Whenever something is Added/Removed, the totalprice is updated
				UpdateTotalPrice();
			}
		}
		[RelayCommand]
		public void ShowPopup()
		{
            PopupView? popup = new PopupView(this);
			Application.Current?.MainPage?.ShowPopup(popup);
        }

        [RelayCommand]
        public void RemoveItemInCart(Cart cartItem)
		{
			if(CartList.Any())
			{
				currentItem = cartItem;
				PopupView? popup = new PopupView(this, true);
				Application.Current?.MainPage?.ShowPopup(popup);
			}
		}

		[RelayCommand]
		static async Task GoBack()
		{
			await Shell.Current.GoToAsync($"//MedicationOverviewPage");

		}

		/// <summary>
		/// The method that represents what happens when
		/// the 'Next' button is pressed. Can only be run when CanExecute is 'true'
		/// </summary>
		[RelayCommand(CanExecute = nameof(CanExecute))]
		void Continue()
		{
			Shell.Current.GoToAsync($"//PersonalInfoPage?totalCartCost={TotalCartCost}");
		}
	}
}
