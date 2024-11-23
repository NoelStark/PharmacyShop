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
		string _lastValidSecurityCode = string.Empty;
		string _lastValidCreditNumber = string.Empty;
		string _lastValidName = string.Empty;
		string _lastValidDate = string.Empty;

		//Properties that is connected to fields in the UI
		[ObservableProperty]
		private string name = string.Empty;

		[ObservableProperty]
		private string creditCardNumber = string.Empty;

	
		[ObservableProperty]
		private string expireDate = string.Empty;

		[ObservableProperty]
		private string cVC = string.Empty;
		

		[ObservableProperty]
		private bool saveInformation = false;

		[ObservableProperty]
		private bool agreeToTerms = false;

		[ObservableProperty]
		private decimal totalCartCost = 0;

		[ObservableProperty]
		private bool isValidCard = false;
		[ObservableProperty]

		private bool showErrorMessage = false;
		[ObservableProperty]
		private string cardImage = string.Empty;

		[ObservableProperty]
		private bool isValidDate = false;

		[ObservableProperty]
		private bool showDateError = false;

		[ObservableProperty]
		private bool showCVCError = false;
		public bool IsValidSecurity { get; private set; } = false;

		private Color Red = Color.FromArgb("D22B2B");
		private Color Grey = Color.FromArgb("C8C8C8");

		[ObservableProperty]
		private Color creditBorderColor = Color.FromArgb("C8C8C8");

		[ObservableProperty]
		private Color dateBorderColor = Color.FromArgb("C8C8C8");

		[ObservableProperty]
		private Color securityBorderColor = Color.FromArgb("C8C8C8");


	
		partial void OnCVCChanged(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (securiyCodeRegex.IsMatch(value))
				{
					if(value.Length == 3)
					{
						IsValidSecurity = true;
						SecurityBorderColor = Grey;
						ShowCVCError = false;
					}
					else
					{
						IsValidSecurity = false;
						SecurityBorderColor = Red;
						ShowCVCError = true;
					}
					CVC = value;
					_lastValidSecurityCode = value;
				}
				else
				{
					CVC = _lastValidSecurityCode;
				}
			}
		}

		partial void OnCreditCardNumberChanged(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				var trimValue = value.Replace(" ", "");
				if (creditCardRegex.IsMatch(trimValue))
				{
					CreditCardNumber = Regex.Replace(trimValue, ".{4}", "$0 ").Trim();

					_lastValidCreditNumber = CreditCardNumber;

				}
				else
				{
					CreditCardNumber = _lastValidCreditNumber;
				}
				string type = GetCardNumber(CreditCardNumber);
				if (string.IsNullOrEmpty(type))
				{
					IsValidCard = false;
					ShowErrorMessage = true;
					CreditBorderColor = Red;
				}
				else
				{
					if (type == "Maestro")
						CardImage = "maestro.png";
					else if (type == "Mastercard")
						CardImage = "mastercard.png";
					else
						CardImage = "visa.png";
					IsValidCard = true;
					ShowErrorMessage = false;
					CreditBorderColor = Grey;
				}
			}
		}

		partial void OnNameChanged(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (nameRegex.IsMatch(value))
				{
					Name = value;
					_lastValidName = value;
				}
				else
				{
					Name = _lastValidName;
				}
			}
		}
		partial void OnExpireDateChanged(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				
				string dateToday = DateTime.Now.ToString("MM/yy");
				int index = 2;
				DateTime parsedDate;
				string parsedValue = value.Replace("/", "");
				if (expireRegex.IsMatch(parsedValue))
				{
					if(value.Length < _lastValidDate.Length && value.EndsWith("/"))
					{
						value = value.Substring(0, value.Length - 1);
					}
					else if(value.Length > _lastValidDate.Length && !value.Contains("/"))
					{
						if(value.Length == 2)
							value += "/";
						else if(value.Length == 3)
						{
							value = value.Substring(0, index) + "/" + value.Substring(index);
						}

					}
				
					if (DateTime.TryParseExact(value, "MM/yy", null, System.Globalization.DateTimeStyles.None, out parsedDate) && value.Length == 5)
					{
						// Parse the current date for comparison
						DateTime currentDate = DateTime.ParseExact(dateToday, "MM/yy", null);

						if (parsedDate >= currentDate && parsedDate <= currentDate.AddYears(10))
						{
							IsValidDate = true;
							DateBorderColor = Grey;
							ShowDateError = false;
						}
						else
						{
							IsValidDate = false;
							DateBorderColor = Red;
							ShowDateError = true;
						}
					}
					else
					{
						IsValidDate = false;
						DateBorderColor = Red;
						ShowDateError = true;
					}
					
					ExpireDate = value;
					_lastValidDate = value;
					
				}
				else
				{
					ExpireDate = _lastValidDate;
				}
			}
		}



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

		private Regex securiyCodeRegex = new Regex(@"^([0-9])\d{0,2}$");
		private Regex creditCardRegex = new Regex(@"^([0-9])\d{0,15}$");
		private Regex nameRegex = new Regex(@"^[a-z\s]{0,20}$", RegexOptions.IgnoreCase);
		private Regex expireRegex = new Regex(@"^[0-9]\d{0,3}$");
	}
}
