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

		[RelayCommand(CanExecute = nameof(AgreeToTerms))]
		async Task Pay()
		{
			string creditCardType = GetCardNumber(CreditCardNumber);
			if (creditCardType != string.Empty)
			{
				PaymentInfo paymentInfo = new PaymentInfo()
				{
					Person = _personService.CurrentPerson,
					CreditCardName = Name,
					CreditCardNumber = CreditCardNumber,
					CreditCardType = creditCardType,
					ExpireDate = ExpireDate,
					SecurityCode = SecurityCode
				};


				_personService.PaymentInfo = paymentInfo;
				Preferences.Set("ShouldSavePayment", SaveInformation);

				await Shell.Current.GoToAsync($"//ConfirmationPage");

			}
		}
	}
}
