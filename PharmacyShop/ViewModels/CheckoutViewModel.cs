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

namespace PharmacyShop.ViewModels
{
	public partial class CheckoutViewModel : ObservableObject
	{
		public ObservableCollection<Cart> CartList { get; set; } = new();
		private Task<List<Medicine>> medicineList { get; set; }
		private readonly MedicineService _medicineService;
		private readonly PersonService _personService;

		[ObservableProperty]
		private bool canExecute;
		

		[ObservableProperty]
		private decimal totalPrice;
		[ObservableProperty]
		private int shippingCost = 0;
		public decimal TotalCartCost
		{
			get
			{
				decimal totalCost = 0;
				foreach(Cart cart in CartList)
				{
					totalCost += cart.TotalItemsPrice;
				}
				return totalCost + ShippingCost;
			}
		}

		public decimal TotalPriceWithoutShipping
		{
			get
			{
				decimal totalCost = 0;
				foreach (Cart cart in CartList)
				{
					totalCost += cart.TotalItemsPrice;
				}
				return totalCost;
			}
		}

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

		async void UpdateTotalPrice()
		{
			TotalPrice = 0;
			foreach(Cart cart in CartList)
			{
				cart.TotalItemsPrice = cart.Quantity * cart.Information.ItemPrice;
				TotalPrice += cart.TotalItemsPrice;
			}
			ShippingCost = TotalPrice > 0 ? 29 : 0;

			OnPropertyChanged(nameof(TotalPriceWithoutShipping));
			OnPropertyChanged(nameof(TotalCartCost));
		}

		[RelayCommand]
		async Task IncreaseQuantity(Cart cartItem)
		{
			cartItem.Quantity++;
			UpdateTotalPrice();
		}

		[RelayCommand]
		async Task DecreaseQuantity(Cart cartItem)
		{
			if(cartItem.Quantity > 0)
			{
				cartItem.Quantity--;
				if(cartItem.Quantity == 0)
				{
					CartList.Remove(cartItem);
					_personService.ItemsCart.Remove(cartItem);
					if(!CartList.Any())
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
		async Task Continue()
		{
			_= Shell.Current.GoToAsync("//PersonalInfoPage");
		}

	}
}
