using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels.Checkout.ConfirmationViewModels
{
	public partial class ConfirmationViewModel : ObservableObject
	{
		/// <summary>
		/// Method that handles what happens when the 'Confirm' button is pressed with the purpose
		/// of sending user back to the Overview page
		/// </summary>
		/// <returns></returns>
		[RelayCommand]
		async Task Confirm()
		{
			bool paymentSaved = Preferences.Get("ShouldSavePayment", false);
			
			//If statement that clears the payment information if user doesnt want to save it
			if (!paymentSaved && _personService.PaymentInfo != null)
			{
				_personService.PaymentInfo.CreditCardName = string.Empty;
				_personService.PaymentInfo.CreditCardNumber = string.Empty;
				_personService.PaymentInfo.ExpireDate = string.Empty;
				_personService.PaymentInfo.SecurityCode = string.Empty;
				_personService.PaymentInfo.CreditCardType = string.Empty;
				_personService.PaymentInfo.Person = new Person();
			}
			//Goes back to the overview page
			await Shell.Current.GoToAsync("//MedicationOverviewPage");
		}
	}
}
