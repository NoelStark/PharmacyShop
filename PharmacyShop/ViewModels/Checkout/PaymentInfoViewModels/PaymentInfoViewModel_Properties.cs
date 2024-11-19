using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels.Checkout.PaymentInfoViewModels
{
	/// <summary>
	/// Partial class containing all the properties/fields
	/// </summary>
	public partial class PaymentInfoViewModel : ObservableObject
	{
		[ObservableProperty]
		private string name = string.Empty;

		[ObservableProperty]
		private string creditCardNumber = string.Empty;

		[ObservableProperty]
		private string expireDate = string.Empty;

		[ObservableProperty]
		private string securityCode = string.Empty;

		[ObservableProperty]
		private bool saveInformation = false;

		[ObservableProperty]
		private bool agreeToTerms = false;

		[ObservableProperty]
		private decimal totalCartCost = 0;

		partial void OnAgreeToTermsChanged(bool value)
		{
			AgreeToTerms = value;
			PayCommand.NotifyCanExecuteChanged();
		}

		private Dictionary<string, Regex> cards = new Dictionary<string, Regex>
		{
			{"Maestro", new Regex(@"^(50|5[6-9]|6[0-9])\d{10,17}$") },
			{"Mastercard", new Regex(@"^(5[1-5])\d{14}$") },
			{"Visa", new Regex(@"^(4)\d{11,17}$") },
		};
	}
}
