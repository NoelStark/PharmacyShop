using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;
using PharmacyShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels
{
	public partial class PersonalInfoViewModel : ObservableObject
	{
		private static readonly Regex NumbersOnly = new Regex("^[0-9]*$");
		private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
		private string _lastValidPhone = string.Empty;
		private string _lastValidPostalCode = string.Empty;

		[ObservableProperty]
		private string firstName;

		partial void OnFirstNameChanged(string value)
		{
			ValidateForm();
		}

		[ObservableProperty]
		private string lastName;

		partial void OnLastNameChanged(string value)
		{
			ValidateForm();
		}

		[ObservableProperty]
		private string email;

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
		private string street;

		partial void OnStreetChanged(string value)
		{
			ValidateForm();
		}

		[ObservableProperty]
		private string city;

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


		[ObservableProperty]
		private int cursorPosition;

		private bool isFormValid;

		public bool IsFormValid
		{
			get => isFormValid;
			private set
			{
				SetProperty(ref isFormValid, value);
				SavePersonCommand.NotifyCanExecuteChanged();
			}
		}


		private void ValidateForm()
		{
			IsFormValid = !string.IsNullOrWhiteSpace(FirstName) &&
				!string.IsNullOrWhiteSpace(LastName) &&
				!string.IsNullOrWhiteSpace(Email) &&
				EmailRegex.IsMatch(Email) &&
				PostalCode.Replace(" ","").Length == 5 &&
				!string.IsNullOrWhiteSpace(Phone) &&
				Phone.Replace(" ","").Length == 10 &&
				!string.IsNullOrWhiteSpace(Street) &&
				!string.IsNullOrWhiteSpace(City) &&
				!string.IsNullOrWhiteSpace(PostalCode);
		}


		private readonly PersonService _personService;

        public PersonalInfoViewModel(PersonService personService)
        {
            _personService = personService;
        }


        [RelayCommand(CanExecute = nameof(IsFormValid))]
		async Task SavePerson()
		{
			Person person = new Person
			{
				FirstName = FirstName,
				LastName = LastName,
				Email = Email,
				Phone = Phone,
				Street = Street,
				City = City,
				PostalCode = PostalCode
			};

			if (person != null) {
				_personService.CurrentPerson = person;
				await Shell.Current.GoToAsync("///PaymentInfoPage");
			}
		}

   
    }
}
