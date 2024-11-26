using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels.Checkout.PaymentInfoViewModels
{
	public partial class PaymentInfoViewModel : ObservableObject
	{
		/// <summary>
		/// This method handles what happens when the 'Pay' button is pressed
		/// </summary>
		/// <returns></returns>
		[RelayCommand(CanExecute = nameof(IsFormValid))]
		async Task Pay()
		{
			string creditCardType = GetCardNumber(CreditCardNumber);
			//If-statement that is run if the creditCardtyp is OK.
			if (creditCardType != string.Empty)
			{
				//Creates a new instance of the PaymentInfo class and fill in the fields with what the user put in
				PaymentInfo paymentInfo = new PaymentInfo()
				{
					Person = _personService.CurrentPerson,
					CreditCardName = Name,
					CreditCardNumber = CreditCardNumber,
					CreditCardType = creditCardType,
					ExpireDate = ExpireDate,
					SecurityCode = CVC
				};


				_personService.PaymentInfo = paymentInfo;
				//Preference (Type of Dictionary) that is global and saves whether the user wants to save info or not
				Preferences.Set("ShouldSavePayment", SaveInformation);

				await Shell.Current.GoToAsync($"//ConfirmationPage");

			}
		}

		[RelayCommand]
		async Task GoBack()
		{
			await Shell.Current.GoToAsync($"//PersonalInfoPage");
		}
	}
}
