using CommunityToolkit.Mvvm.ComponentModel;
using PharmacyShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
		private string _lastValidFirstName = string.Empty;
        private string _lastValidLastName = string.Empty;
        private string _lastValidCity = string.Empty;
        private string _lastValidStreet = string.Empty;
        private static readonly Regex NumbersOnly = new Regex("^[0-9]*$");
        private static readonly Regex PostalnumbersOnly = new Regex("^[0-9]{0,5}$");
        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        private static readonly Regex firstNameRegex = new Regex(@"^[a-z\s\-]{0,15}$", RegexOptions.IgnoreCase);
        private static readonly Regex lastNameRegex = new Regex(@"^[a-z\s]{0,15}$", RegexOptions.IgnoreCase);

        //Properties that contains values that are sent to the UI

        [ObservableProperty]
		private string firstName = string.Empty;

		[ObservableProperty]
		private decimal totalCartCost = 0;

		[ObservableProperty]
		private int cursorPosition = 0;

		private bool isFormValid = false;

        private bool isUpdatingStreet = false;
        private bool isUpdatingPostalCode = false;

        //When the First Name changes its value, a method is entered to check whether all fields are valid or not

        [ObservableProperty]
		private string lastName = string.Empty;

		//When the Last Name changes its value, a method is entered to check whether all fields are valid or not

		[ObservableProperty]
		private string email = string.Empty;

		//When the Email changes its value, a method is entered to check whether all fields are valid or not
		[ObservableProperty]
		private string phone = string.Empty;

		[ObservableProperty]
		private string postalCode = string.Empty;

		[ObservableProperty]
		private string street = string.Empty;

		[ObservableProperty]
		private string city = string.Empty;

		private bool isUpdatingPhone = false;

        private Color Red = Color.FromArgb("D22B2B");
        private Color Grey = Color.FromArgb("C8C8C8");

        [ObservableProperty]
        private Color emailBorderColor = Color.FromArgb("C8C8C8");

        [ObservableProperty]
        private Color phoneNumberBorderColor = Color.FromArgb("C8C8C8");

        [ObservableProperty]
        private Color streetBorderColor = Color.FromArgb("C8C8C8");

        [ObservableProperty]
        private Color postalCodeBorderColor = Color.FromArgb("C8C8C8");

        [ObservableProperty]
        private Color cityBorderColor = Color.FromArgb("C8C8C8");

		[ObservableProperty]
		private bool showErrorEmail = false;

        [ObservableProperty]
        private bool showErrorPhoneNumber = false;

        partial void OnFirstNameChanged(string value)
		{
            string lettersValue = value.Replace(" ", "");
            if (firstNameRegex.IsMatch(value.Replace(" ", "")))
			{
				if (!value.EndsWith(" ") && !_lastValidFirstName.EndsWith(" "))
				{
                    _lastValidFirstName = value;
                }
            }

            FirstName = _lastValidFirstName;

            ValidateForm();
        }

		partial void OnLastNameChanged(string value)
		{
            string lettersValue = value.Replace(" ", "");
            if (lastNameRegex.IsMatch(value.Replace(" ", "")))
			{
                if (!value.EndsWith(" ") && !_lastValidLastName.EndsWith(" "))
                {
                    _lastValidLastName = value;
                }
            }

            LastName = _lastValidLastName;

            ValidateForm();
        }

		partial void OnEmailChanged(string value)
		{
			if (EmailRegex.IsMatch(value))
			{
                EmailBorderColor = Grey;
				ShowErrorEmail = false;
            }
			else
			{
                EmailBorderColor = Red;
				ShowErrorEmail = true;
            }
            ValidateForm();
		}


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
				if (numericValue.Length < 10)
				{
					PhoneNumberBorderColor = Red;
					ShowErrorPhoneNumber = true;
				}
                else
				{
                    ShowErrorPhoneNumber = false;
                    PhoneNumberBorderColor = Grey;
				}
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
			else
                PhoneNumberBorderColor = Red;
			//If a letter or other character is input, it goes back to previous state that is valid (only numbers)
			Phone = _lastValidPhone;
			isUpdatingPhone = false;

			//Likewise here there's a check if all fields now are valid
			ValidateForm();
		}

		

		//When the Street changes its value, a method is entered to check whether all fields are valid or not
		partial void OnStreetChanged(string value)
		{
			
			string empty = string.Empty;
			if (isUpdatingStreet)
				return;

			empty = value;
			if (!string.IsNullOrWhiteSpace(empty) && empty.Any(char.IsDigit))
			{
				isUpdatingStreet = true;
				var index = value.IndexOf(value.First(char.IsDigit));
				value = value.Substring(0, index) + " " + value.Substring(index);
				
				Street = value;

			}

			isUpdatingStreet = false;

			ValidateForm();
		}

	
		//When the City changes its value, a method is entered to check whether all fields are valid or not
		partial void OnCityChanged(string value)
		{
            string lettersValue = value.Replace(" ", "");
            if (lastNameRegex.IsMatch(value.Replace(" ", "")))
			{
				if (!value.EndsWith(" ") && !_lastValidCity.EndsWith(" "))
				{
                    _lastValidCity = value;
                }
			}

            City = _lastValidCity;
            ValidateForm();
		}


        //When the Postal Code changes its value, a method is entered to check whether all fields are valid or not
        partial void OnPostalCodeChanged(string value)
        {
            if (isUpdatingPostalCode)
                return;

                isUpdatingPostalCode = true;

            if (!string.IsNullOrEmpty(value))
            {
				//If the value is a number and the length isnt 5 or more, the field is updated
                if (PostalnumbersOnly.IsMatch(value.Replace(" ", "")))
                {
                    //Adds whitespace ' ' if the length is 3
                    if (value.Length == 3 && !value.Contains(" "))
                    {
                        PostalCode = value + " ";
                    }
                    else if (value.Length < _lastValidPostalCode.Length && value.EndsWith(" "))
                    {
                        value = value.Substring(0, value.Length - 1);
                        PostalCode = value;
                    }

					if (value.Replace(" ", "").Length == 5)
                        PostalCodeBorderColor = Grey;
					else
                        PostalCodeBorderColor = Red;

                    _lastValidPostalCode = PostalCode;
                }
                //If the value isnt a number, it goes back to previous valid value (only numbers)
                else
                {
                    PostalCode = _lastValidPostalCode;
                }
            }

            isUpdatingPostalCode = false;

            //Goes to check if all fields now are valid
            ValidateForm();
        }

    }
}
