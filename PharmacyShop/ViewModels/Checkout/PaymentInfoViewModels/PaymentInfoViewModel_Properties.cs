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
		//Properties that is connected to fields in the UI
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
		/// <summary>
		/// Whenever the user clicks on the Checkbox, the 'Next' button is enabled/disabled
		/// </summary>
		/// <param name="value">Parameter that contains true/false depending on whether box is checked or not</param>
		partial void OnAgreeToTermsChanged(bool value)
		{
			AgreeToTerms = value;
			//Makes sure to send a message to the Pay method to check whether AgreeToTerms is true or not
			PayCommand.NotifyCanExecuteChanged();
		}
		//Dictionary that contains requirements for different card types
		private Dictionary<string, Regex> cards = new Dictionary<string, Regex>
		{
			{"Maestro", new Regex(@"^(50|5[6-9]|6[0-9])\d{10,17}$") },
			{"Mastercard", new Regex(@"^(5[1-5])\d{14}$") },
			{"Visa", new Regex(@"^(4)\d{11,17}$") },
		};
	}
}
