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

namespace PharmacyShop.ViewModels
{
	public partial class ConfirmationViewModel : ObservableObject
	{
        [ObservableProperty]
        private decimal totalCartCost;

		[ObservableProperty]
		private decimal shippingCost;

		[ObservableProperty]
		private string name;

		[ObservableProperty]
		private string orderNumber;

		[ObservableProperty]
		private string creditCard;

		[ObservableProperty]
		private string orderDate = DateTime.Now.ToString("yyyy-MM-dd");

		[ObservableProperty]
		private string expectedDelivery = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd");

		public ObservableCollection<Cart> ItemsCart { get; set; } = new();
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
			string creditCardNumber = _personService.PaymentInfo.CreditCardNumber;
			CreditCard = _personService.PaymentInfo.CreditCardType + " **** " + creditCardNumber.Substring(creditCardNumber.Length - 4);
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

		

		[RelayCommand]
		async Task Confirm()
		{
			bool paymentSaved = Preferences.Get("ShouldSavePayment", false);

			if (!paymentSaved)
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
