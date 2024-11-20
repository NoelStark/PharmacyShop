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
		/// <summary>
		/// First two fields contains values that are valid(numbers) for phone number and postal code
		/// The third and fourth field are the filters that check if the input is a number or not
		/// </summary>
		private string _lastValidPhone = string.Empty;
		private string _lastValidPostalCode = string.Empty;
		private static readonly Regex NumbersOnly = new Regex("^[0-9]*$");
		private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

		//Properties that contains values that are sent to the UI

		[ObservableProperty]
		private string firstName = string.Empty;

		[ObservableProperty]
		private decimal totalCartCost = 0;

		[ObservableProperty]
		private int cursorPosition = 0;

		private bool isFormValid = false;

		//When the First Name changes its value, a method is entered to check whether all fields are valid or not
		partial void OnFirstNameChanged(string value)
		{
			ValidateForm();
		}

		[ObservableProperty]
		private string lastName = string.Empty;

		//When the Last Name changes its value, a method is entered to check whether all fields are valid or not
		partial void OnLastNameChanged(string value)
		{
			ValidateForm();
		}

		[ObservableProperty]
		private string email = string.Empty;

		//When the Email changes its value, a method is entered to check whether all fields are valid or not
		partial void OnEmailChanged(string value)
		{
			ValidateForm();
		}

		[ObservableProperty]
		private string phone = string.Empty;

		private bool isUpdatingPhone = false;

		//When the Phone number changes its value, a method is entered to check whether all fields are valid or not
		partial void OnPhoneChanged(string value)
		{
			//If the phone number is currently being updated we exit the method
			if (isUpdatingPhone) return;
			//Since the phone numbers contains ' ' for readability, those are removed during the check
			string numericValue = value.Replace(" ", "");

			//If the value is a number and the length isnt 10 or more, the field is updated
			if (NumbersOnly.IsMatch(value.Replace(" ", "")) && numericValue.Length <= 10)
			{
				isUpdatingPhone = true;
				//If-statements that adds whitespace ' ' for readability if the length is X long
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
				//Updates the value since its a valid input (Number)
				_lastValidPhone = Phone;
			}
			//If a letter or other character is input, it goes back to previous state that is valid (only numbers)
			Phone = _lastValidPhone;
			isUpdatingPhone = false;

			//Likewise here there's a check if all fields now are valid
			ValidateForm();
		}

		[ObservableProperty]
		private string street = string.Empty;

		//When the Street changes its value, a method is entered to check whether all fields are valid or not
		partial void OnStreetChanged(string value)
		{
			ValidateForm();
		}

		[ObservableProperty]
		private string city = string.Empty;

		//When the City changes its value, a method is entered to check whether all fields are valid or not
		partial void OnCityChanged(string value)
		{
			ValidateForm();
		}

		[ObservableProperty]
		private string postalCode = string.Empty;

		//When the Postal Code changes its value, a method is entered to check whether all fields are valid or not
		partial void OnPostalCodeChanged(string value)
		{
			//If the value is a number and the length isnt 5 or more, the field is updated
			if (NumbersOnly.IsMatch(value.Replace(" ", "")) && value.Replace(" ", "").Length <= 5)
			{
				//Adds whitespace ' ' if the length is 3
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
			//If the value isnt a number, it goes back to previous valid value (only numbers)
			else
			{
				PostalCode = _lastValidPostalCode;
			}
			//Goes to check if all fields now are valid
			ValidateForm();
		}

	}
}
