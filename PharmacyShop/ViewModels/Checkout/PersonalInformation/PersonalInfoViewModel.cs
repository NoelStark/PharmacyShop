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
using System.Xml.Linq;

namespace PharmacyShop.ViewModels.Checkout.PersonalInformation
{

	[QueryProperty(nameof(TotalCartCost), "totalCartCost")]
	

	public partial class PersonalInfoViewModel : ObservableObject
	{
		private readonly PersonService _personService;

		//Property to check if all the fields are valid and if so, update the Command that has a CanExecute 
		public bool IsFormValid
		{
			get => isFormValid;
			private set
			{
				SetProperty(ref isFormValid, value);
				SavePersonCommand.NotifyCanExecuteChanged();
			}

		}
		public void Reinitialize()
		{
			//Fills in the fields with values. If the user didnt save, these fields are empty
			if (_personService.CurrentPerson != null)
			{
				FirstName = _personService.CurrentPerson.FirstName;
				LastName = _personService.CurrentPerson.LastName;
				Email = _personService.CurrentPerson.Email;
				Street = _personService.CurrentPerson.Street;
				PostalCode = _personService.CurrentPerson.PostalCode;
				City = _personService.CurrentPerson.City;
				Phone = _personService.CurrentPerson.Phone;
				ShouldSaveInfo = Preferences.Get("ShouldSaveInfo", false);
				ResetFieldsUI();
			}
		}

		private void ResetFieldsUI()
		{
			ShowErrorEmail = false;
			ShowErrorPhoneNumber = false;
			PostalCodeBorderColor = Grey;
			EmailBorderColor = Grey;
			PhoneNumberBorderColor = Grey;
		}
        public PersonalInfoViewModel(PersonService personService)
        {
            _personService = personService;
			Reinitialize();
			TotalCartCost = _personService.TotalCartCost;
        }
        /// <summary>
        /// The method that checks whether all fields are valid or not
        /// </summary>
        private void ValidateForm()
		{

			IsFormValid = !string.IsNullOrWhiteSpace(FirstName) &&
				!string.IsNullOrWhiteSpace(LastName) &&
				!string.IsNullOrWhiteSpace(Email) &&
				EmailRegex.IsMatch(Email) &&
				PostalCode.Replace(" ", "").Length == 5 &&
				!string.IsNullOrWhiteSpace(Phone) &&
				Phone.Replace(" ", "").Length == 10 &&
				!string.IsNullOrWhiteSpace(Street) &&
				!string.IsNullOrWhiteSpace(City) &&
				!string.IsNullOrWhiteSpace(PostalCode);
		} 
    }
}
