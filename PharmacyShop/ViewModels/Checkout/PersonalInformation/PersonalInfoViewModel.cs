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

namespace PharmacyShop.ViewModels.Checkout.PersonalInformation
{

	[QueryProperty(nameof(TotalCartCost), "totalCartCost")]
	
	public partial class PersonalInfoViewModel : ObservableObject
	{
		private readonly PersonService _personService;


		public bool IsFormValid
		{
			get => isFormValid;
			private set
			{
				SetProperty(ref isFormValid, value);
				SavePersonCommand.NotifyCanExecuteChanged();
			}

		}
        public PersonalInfoViewModel(PersonService personService)
        {
            _personService = personService;
			TotalCartCost = _personService.TotalCartCost;
        }

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
