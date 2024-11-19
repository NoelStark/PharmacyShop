using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;
using PharmacyShop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels.Checkout.ConfirmationViewModels
{
	public partial class ConfirmationViewModel : ObservableObject
	{
       
        private readonly PersonService _personService;


		public ConfirmationViewModel(PersonService personService)
        {
            _personService = personService;
			Reinitialize();
			GenerateOrderNumber();
		}

		public void Reinitialize()
		{
			this.ItemsCart.Clear();
			foreach (Cart item in _personService.ItemsCart)
			{
				this.ItemsCart.Add(item);
			}

			Name = _personService.CurrentPerson.FirstName;
			TotalCartCost = _personService.TotalCartCost;
			ShippingCost = _personService.ShippingCost;
			if(_personService.PaymentInfo != null)
			{
				string creditCardNumber = _personService.PaymentInfo.CreditCardNumber;
				CreditCard = _personService.PaymentInfo.CreditCardType + " **** " + creditCardNumber.Substring(creditCardNumber.Length - 4);
			}
		}

		private void GenerateOrderNumber()
		{
			Random random = new Random();
			string ordernumber = string.Empty;
			string source = "0123456789ABCDEFGHIJKLMNOPQRSTUV";

			for (int i = 1; i <= 12; i++)
			{
				int randomIndex = random.Next(source.Length);
				ordernumber += source[randomIndex];
			}
			OrderNumber = ordernumber;
		}	
	
    }
}
