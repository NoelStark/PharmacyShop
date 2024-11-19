using CommunityToolkit.Mvvm.ComponentModel;
using PharmacyShop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels.Checkout.CheckoutViewModels
{
	public partial class CheckoutViewModel : ObservableObject
	{
		public ObservableCollection<Cart> CartList { get; set; } = new();
		private Task<List<Medicine>>? medicineList { get; set; }

		[ObservableProperty]
		private bool canExecute = false;

		[ObservableProperty]
		private int totalAmountOfItems = 0;

		[ObservableProperty]
		private decimal totalPrice = 0;
		[ObservableProperty]
		private int shippingCost = 0;
		public decimal TotalCartCost
		{
			get
			{
				decimal totalCost = 0;
				foreach (Cart cart in CartList)
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
	}
}
