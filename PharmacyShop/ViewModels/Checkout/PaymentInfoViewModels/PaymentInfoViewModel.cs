using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;
using PharmacyShop.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;
using static System.Net.Mime.MediaTypeNames;

namespace PharmacyShop.ViewModels.Checkout.PaymentInfoViewModels
{
	public partial class PaymentInfoViewModel : ObservableObject
	{
	
		private PersonService _personService;
		private bool isFormValid;
		public bool IsFormValid
		{
			get => isFormValid;
			private set
			{
				SetProperty(ref isFormValid, value);
				PayCommand.NotifyCanExecuteChanged();
			}

		}

		public PaymentInfoViewModel(PersonService personService)
        {
            _personService = personService;
			Reinitialize();
			TotalCartCost = _personService.TotalCartCost;
		}

		/// <summary>
		/// The method that checks whether the inputted CreditCard number is valid or not
		/// </summary>
		/// <param name="creditCardNumber">The number that the user input</param>
		/// <returns>Returns what kind of card that the credit card is</returns>
		private string GetCardNumber(string creditCardNumber)
		{
			string type = string.Empty;
			var formatString = creditCardNumber.Replace(" ", "");
			foreach(var pattern in cards)
			{
				//If the number is valid (A match to the dictionary's Regex filters)
				if (pattern.Value.IsMatch(formatString))
				{
					type = pattern.Key;
					break;
				}
			}
			
			return type;
		}
		/// <summary>
		/// Method that is run each time the View is being shown
		/// </summary>
		public void Reinitialize()
		{
			//TODO Remove this since it doesnt do anything?
			bool paymentSaved = Preferences.Get("ShouldSavePayment", false);
			CardImage = string.Empty;
			//Fills in the fields with values. If the user didnt save, these fields are empty
			if(_personService.PaymentInfo != null)
			{
				CreditCardNumber = _personService.PaymentInfo.CreditCardNumber;
				Name = _personService.PaymentInfo.CreditCardName;
				ExpireDate = _personService.PaymentInfo.ExpireDate;
				CVC = _personService.PaymentInfo.SecurityCode;
				SaveInformation = paymentSaved;
				AgreeToTerms = false;

			}
			
		}

		private void ValidateForm()
		{
			IsFormValid =	
				IsValidCard &&
				IsValidDate &&
				!string.IsNullOrEmpty(Name) &&
				IsValidSecurity &&
				AgreeToTerms;
		}

		public int MoveCursor(Entry entry)
		{
			if (entry != null && (entry.Text.Length == 3 || entry.Text.Length == 4) && entry.Text.Contains("/"))
			{
				return entry.Text.Length;
			}
			return 0; 
		}

	}
}
