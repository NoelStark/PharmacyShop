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
		partial void OnPhoneChanged(string value)
		{
			if (NumbersOnly.IsMatch(value) && Phone.Length != 11)
			{
				_lastValidPhone = value;
			}
			else
			{
				Phone = _lastValidPhone;
			}
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
			if (NumbersOnly.IsMatch(value) && PostalCode.Length != 6)
			{
				_lastValidPostalCode = value;
			}
			else
			{
				PostalCode = _lastValidPostalCode;
			}
			ValidateForm();
		}

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
				PostalCode.Length == 5 &&
				!string.IsNullOrWhiteSpace(Phone) &&
				Phone.Length == 10 &&
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
