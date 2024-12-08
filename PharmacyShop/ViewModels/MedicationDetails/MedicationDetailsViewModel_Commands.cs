using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using PharmacyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PharmacyShop.ViewModels.MedicationDetails
{
	public partial class MedicationDetailsViewModel : ObservableObject
	{
		private ObservableCollection<Medicine> _medicines; //En observablecollection som håller mediciner
		[RelayCommand]
		void GoBack() //Metod för om användaren trycker på tillbaka pilen
		{
			Shell.Current.GoToAsync($"//MedicationOverviewPage"); //Diregerar till MedicationOverviewPage

        }
		[RelayCommand]
		void MedicineClick(Medicine medicine) //Metod för om användaren söker på mediciner och trycker på en medicin
		{
			WeakReferenceMessenger.Default.Send(new ValueChangedMessage<Medicine>(medicine)); //Ett message som säger att en ny medicin ska visas
		}

		[RelayCommand]
		void IncreaseQuantity() //Metod om användaren trycker på att öka kvantiteten
		{
			Quantity++; //Ökar kvantiteten med 1
		}

		[RelayCommand]
		void DecreaseQuantity() //Metod om användaren trycker på att minska kvantiteten
        {
			if (Quantity > 1) //Så länge det inte är mindre än 0
			{
				Quantity--; //Minskar kvantiteten med 1
            }
		}
		[RelayCommand]
		void AddToCart() //Metod om användaren trycker på knappen att lägga till i kundvagn
		{
			Dictionary<Medicine, int> medicineQuantity = new Dictionary<Medicine, int> { { _medicineService.CurrentMedicine, Quantity } }; //Dictionary som fåt all inmatad information och 

			WeakReferenceMessenger.Default.Send(new ValueChangedMessage<Dictionary<Medicine, int>>(medicineQuantity)); //Message varan
		}

        [RelayCommand]
        public void GoToCheckout() //Metod för om användaren trycker på cart symbolen
        {
            Shell.Current.GoToAsync("//CheckoutPage"); //Diregerar till checkoutpage

        }
    }
}
