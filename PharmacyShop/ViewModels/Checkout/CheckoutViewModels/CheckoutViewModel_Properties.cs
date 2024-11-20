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
		//The CartList contains all the items in the Cart and is connected to the UI to display them
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
		/// <summary>
		/// A property that calculates the total cost, including shipping cost,
		/// to display to the user
		/// </summary>
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
		/// <summary>
		/// A property that calculates the total cost, NOT including shipping cost,
		/// to show the user what the price of the items adds up to
		/// </summary>
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
