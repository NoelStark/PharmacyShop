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
		[RelayCommand]
		async Task Confirm()
		{
			bool paymentSaved = Preferences.Get("ShouldSavePayment", false);

			if (!paymentSaved && _personService.PaymentInfo != null)
			{
				_personService.PaymentInfo.CreditCardName = string.Empty;
				_personService.PaymentInfo.CreditCardNumber = string.Empty;
				_personService.PaymentInfo.ExpireDate = string.Empty;
				_personService.PaymentInfo.SecurityCode = string.Empty;
				_personService.PaymentInfo.CreditCardType = string.Empty;
				_personService.PaymentInfo.Person = new Person();
			}

			await Shell.Current.GoToAsync("//MedicationOverviewPage");
		}
	}
}
