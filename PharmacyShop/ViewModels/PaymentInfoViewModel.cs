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

namespace PharmacyShop.ViewModels
{
	public partial class PaymentInfoViewModel : ObservableObject
	{
		[ObservableProperty]
		private string name;

		[ObservableProperty]
		private string creditCardNumber;

		[ObservableProperty]
		private string expireDate;

		[ObservableProperty]
		private string securityCode;

		[ObservableProperty]
		private bool saveInformation;

		[ObservableProperty]
		private bool agreeToTerms;

		private Dictionary<string, Regex> cards = new Dictionary<string, Regex>
		{
			{"Maestro", new Regex(@"^(50|5[6-9]|6[0-9])\d{10,17}$") },
			{"Mastercard", new Regex(@"^(5[1-5])\d{14}$") },
			{"Visa", new Regex(@"^(4)\d{11,17}$") },
		};


		partial void OnAgreeToTermsChanged(bool value)
		{
			AgreeToTerms = value;
			PayCommand.NotifyCanExecuteChanged();
		}

		private PersonService _personService;

        public PaymentInfoViewModel(PersonService personService)
        {
            _personService = personService;
			Reinitialize();
		}



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

		private string GetCardNumber(string creditCardNumber)
		{

			//56-69
			//var cardNumber = _personService.PaymentInfo.CreditCardNumber;
			bool foundMatch = false;
			string type = string.Empty;
			foreach(var pattern in cards)
			{
				if (pattern.Value.IsMatch(creditCardNumber))
				{
					foundMatch = true;
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
