using CommunityToolkit.Mvvm.ComponentModel;
using PharmacyShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace PharmacyShop.ViewModels.Checkout.PersonalInformation
{
	public partial class PersonalInfoViewModel : ObservableObject
	{
		private string _lastValidPhone = string.Empty;
		private string _lastValidPostalCode = string.Empty;
		private static readonly Regex NumbersOnly = new Regex("^[0-9]*$");
		private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

		[ObservableProperty]
		private string firstName = string.Empty;

		[ObservableProperty]
		private decimal totalCartCost = 0;



		[ObservableProperty]
		private int cursorPosition = 0;

		private bool isFormValid = false;

		partial void OnFirstNameChanged(string value)
		{
			ValidateForm();
		}

		[ObservableProperty]
		private string lastName = string.Empty;

		partial void OnLastNameChanged(string value)
		{
			ValidateForm();
		}

		[ObservableProperty]
		private string email = string.Empty;

		partial void OnEmailChanged(string value)
		{
			ValidateForm();
		}

		[ObservableProperty]
		private string phone = string.Empty;

		private bool isUpdatingPhone = false;
		partial void OnPhoneChanged(string value)
		{
			if (isUpdatingPhone) return;
			string numericValue = value.Replace(" ", "");
			if (NumbersOnly.IsMatch(value.Replace(" ", "")) && numericValue.Length <= 10)
			{
				isUpdatingPhone = true;
				if (numericValue.Length > _lastValidPhone.Replace(" ", "").Length)
				{
					if (numericValue.Length == 3 || numericValue.Length == 6 || numericValue.Length == 8)
					{
						Phone = value + " ";
					}
					else
					{
						Phone = value;
					}

				}
				else
					Phone = value;

				_lastValidPhone = Phone;
			}

			Phone = _lastValidPhone;
			isUpdatingPhone = false;


			ValidateForm();
		}

		[ObservableProperty]
		private string street = string.Empty;

		partial void OnStreetChanged(string value)
		{
			ValidateForm();
		}

		[ObservableProperty]
		private string city = string.Empty;

		partial void OnCityChanged(string value)
		{
			ValidateForm();
		}

		[ObservableProperty]
		private string postalCode = string.Empty;

		partial void OnPostalCodeChanged(string value)
		{
			if (NumbersOnly.IsMatch(value.Replace(" ", "")) && value.Replace(" ", "").Length <= 5)
			{
				if (value.Length == 3 && !value.Contains(" "))
				{
					PostalCode = value + " ";
				}
				else
				{
					PostalCode = value;
				}

				_lastValidPostalCode = PostalCode;
			}
			else
			{
				PostalCode = _lastValidPostalCode;
			}

			ValidateForm();
		}

	}
}
