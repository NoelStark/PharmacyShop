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

		/// <summary>
		/// Method to re-initialize everything when the View appears
		/// </summary>
		public void Reinitialize()
		{
			//Clears the cart and updates the items
			this.ItemsCart.Clear();
			foreach (Cart item in _personService.ItemsCart)
			{
				this.ItemsCart.Add(item);
			}

			//Makes sure to update all the fields in the UI to the new data
			Name = _personService.CurrentPerson.FirstName;
			TotalCartCost = _personService.TotalCartCost;
			ShippingCost = _personService.ShippingCost;
			if (_personService.PaymentInfo != null)
			{
				string creditCardNumber = _personService.PaymentInfo.CreditCardNumber;
				CreditCard = _personService.PaymentInfo.CreditCardType + " **** " + creditCardNumber.Substring(creditCardNumber.Length - 4);
			}

			Task supplierEmail = SupplierEmail();

        }

		/// <summary>
		/// Method to generate a random ordernumber being 12 characters long
		/// </summary>
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

		public async Task SupplierEmail()
		{
			string productsFromCart = $"Customer Name: {_personService.CurrentPerson.FullName}\nCustomer Email: {_personService.CurrentPerson.Email}\nCustomer Email: {_personService.CurrentPerson.Phone}\nOrdernumber{OrderNumber}\n\nProducts:";
			foreach (var item in ItemsCart)
			{
                productsFromCart += $"\nName: {item.Medicine.Name} {item.Medicine.Dose} {item.Medicine.Description}\nArticlenumber: {item.Medicine.ArticleNumber}, Quantity: {item.Quantity}\n";
            }
			productsFromCart += "\nBest regards\nEmployees at PharmacyShop";
			Console.WriteLine(productsFromCart);
            var email = new EmailMessage
			{
				Subject = $"Hello our dear pharmacy supplier, we just recieved an order from a customer.\n",
				Body = $"{productsFromCart}\n",
				To = new List<string> { "To: supplier@gmail.com", "From: pharmacyshop@gmail.com"},
			};
			await Email.ComposeAsync(email);
		}
	}
}
