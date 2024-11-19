using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;
using PharmacyShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PharmacyShop.ViewModels.Checkout.PaymentInfoViewModels
{
	public partial class PaymentInfoViewModel : ObservableObject
	{
	


	

		private PersonService _personService;

        public PaymentInfoViewModel(PersonService personService)
        {
            _personService = personService;
			Reinitialize();
			TotalCartCost = _personService.TotalCartCost;
		}


		private string GetCardNumber(string creditCardNumber)
		{
			string type = string.Empty;
			foreach(var pattern in cards)
			{
				if (pattern.Value.IsMatch(creditCardNumber))
				{
					type = pattern.Key;
					break;
				}
			}
			return type;
		}

		public void Reinitialize()
		{
			bool paymentSaved = Preferences.Get("ShouldSavePayment", false);
			if(_personService.PaymentInfo != null)
			{
				CreditCardNumber = _personService.PaymentInfo.CreditCardNumber;
				Name = _personService.PaymentInfo.CreditCardName;
				ExpireDate = _personService.PaymentInfo.ExpireDate;
				SecurityCode = _personService.PaymentInfo.SecurityCode;
				SaveInformation = paymentSaved;
				AgreeToTerms = false;

			}
			//MessagingCenter.Subscribe<MedicationOverviewPageViewModel>(this, "RefreshPage", (sender) =>
			//{


			//});
		}

	}
}
