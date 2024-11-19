using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;
using PharmacyShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels
{
	public partial class PaymentInfoViewModel : ObservableObject
	{
		[ObservableProperty]
		private string name;

		[ObservableProperty]
		private long creditCardNumber;

		[ObservableProperty]
		private DateTime expireDate;

		[ObservableProperty]
		private int securityCode;

		[ObservableProperty]
		private bool saveInformation;

		[ObservableProperty]
		private bool agreeToTerms;

		partial void OnAgreeToTermsChanged(bool value)
		{
			if(value)
				PayCommand.NotifyCanExecuteChanged();
		}

		private PersonService _personService;

        public PaymentInfoViewModel(PersonService personService)
        {
            _personService = personService;
        }

        private void SaveCredentials()
		{
			PaymentInfo paymentInfo = new PaymentInfo() 
			{
				Person = _personService.CurrentPerson,
				CreditCardName = Name,
				CreditCardNumber = CreditCardNumber,
				ExpireDate = ExpireDate,
				SecurityCode = SecurityCode
			};

			_personService.PaymentInfo = paymentInfo;
		}


		[RelayCommand(CanExecute = nameof(AgreeToTerms))]
		async Task Pay()
		{
			if (SaveInformation)
			{
				SaveCredentials();
			}

            Console.WriteLine("ss");
		}
	}
}
