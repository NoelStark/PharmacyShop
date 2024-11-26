using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace PharmacyShop.ViewModels.Checkout.PersonalInformation
{
	public partial class PersonalInfoViewModel : ObservableObject
	{
		/// <summary>
		/// Method that saves the person to be accessed throughout other
		/// parts of the program
		/// </summary>
		/// <returns></returns>
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
			//Saves the Person instance into the PersonService
			if (person != null)
			{
				_personService.CurrentPerson = person;
				Preferences.Set("ShouldSaveInfo", ShouldSaveInfo);
				await Shell.Current.GoToAsync("///PaymentInfoPage");
			}
		}

	}
}
