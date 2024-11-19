using CommunityToolkit.Mvvm.ComponentModel;
using PharmacyShop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels.Checkout.ConfirmationViewModels
{
	public partial class ConfirmationViewModel : ObservableObject
	{
		[ObservableProperty]
		private decimal totalCartCost = 0;

		[ObservableProperty]
		private decimal shippingCost = 0;

		[ObservableProperty]
		private string name = string.Empty;

		[ObservableProperty]
		private string orderNumber = string.Empty;

		[ObservableProperty]
		private string creditCard = string.Empty;

		[ObservableProperty]
		private string orderDate = DateTime.Now.ToString("yyyy-MM-dd");

		[ObservableProperty]
		private string expectedDelivery = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd");

		public ObservableCollection<Cart> ItemsCart { get; set; } = new();
	}
}
