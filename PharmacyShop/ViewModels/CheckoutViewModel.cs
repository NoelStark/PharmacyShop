using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels
{
	public partial class CheckoutViewModel : ObservableObject
	{
		public ObservableCollection<Cart> MedicineList { get; set; }

		[ObservableProperty]
		private int totalPrice;
		[ObservableProperty]
		private int shippingCost = 29;
		public int TotalCartCost
		{
			get
			{
				int totalCost = 0;
				foreach(Cart cart in MedicineList)
				{
					totalCost += cart.TotalItemsPrice;
				}
				return totalCost + ShippingCost;
			}
		}

		public int TotalPriceWithoutShipping
		{
			get
			{
				int totalCost = 0;
				foreach (Cart cart in MedicineList)
				{
					totalCost += cart.TotalItemsPrice;
				}
				return totalCost;
			}
		}
		public CheckoutViewModel()
        {
			MedicineList = new ObservableCollection<Cart>
			{
				new Cart
				{
					Medicine =  new Medicine { Id = 1, Name = "Alvedon", Description = "500mg" },
					Quantity = 1,
					ItemPrice = 32,
					TotalItemsPrice = 32
				},
				new Cart
				{
					Medicine =  new Medicine { Id = 1, Name = "Ipren", Description = "200mg" },
					Quantity = 1,
					ItemPrice = 45,
					TotalItemsPrice = 45
				},
				new Cart
				{
					Medicine =  new Medicine { Id = 1, Name = "Lalala", Description = "100ml" },
					Quantity = 1,
					ItemPrice = 63,
					TotalItemsPrice = 63
				},
				new Cart
				{
					Medicine =  new Medicine { Id = 1, Name = "Lalala", Description = "100ml" },
					Quantity = 1,
					ItemPrice = 63,
					TotalItemsPrice = 63
				}
			};
			UpdateTotalPrice();


		}

		async void UpdateTotalPrice()
		{
			TotalPrice = 0;
			foreach(Cart cart in MedicineList)
			{
				cart.TotalItemsPrice = cart.Quantity * cart.ItemPrice;
				TotalPrice += cart.TotalItemsPrice;
			}
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
				UpdateTotalPrice();
			}


		}

	}
}
