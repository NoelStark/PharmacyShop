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
using System.Diagnostics;
using PharmacyShop.Views;

namespace PharmacyShop.ViewModels.MedicationDetails
{
	public partial class MedicationDetailsViewModel : ObservableObject
	{
		private ObservableCollection<Medicine> _medicines;
		[RelayCommand]
		void GoBack() //Method that enables the user to direct back to the MedicationOverviewPage
		{
			Shell.Current.GoToAsync($"//MedicationOverviewPage");
        }
        //Method for when the user searches for medications and selects a specific medication. It sends a message of the selected medicine to show
        [RelayCommand]
		void MedicineClick(Medicine medicine)
        {
			WeakReferenceMessenger.Default.Send(new ValueChangedMessage<Medicine>(medicine));
		}

		[RelayCommand]
		void IncreaseQuantity() //Method for when the user clicks to increase the quantity, each click is +1
        {
			Quantity++;
		}

		[RelayCommand]
		void DecreaseQuantity() //Method for when the user clicks to decrease the quantity, each click is -1
        {
			if (Quantity > 1)
			{
				Quantity--; 
            }
		}
		[RelayCommand]
		void AddToCart() //Method that enables user to add items to cart. The method sends a message so the item gets added in the cartlist
		{
			Dictionary<Medicine, int> medicineQuantity = new Dictionary<Medicine, int> { { _medicineService.CurrentMedicine, Quantity } }; //A dictionary that receives all the input information

            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<Dictionary<Medicine, int>>(medicineQuantity));
		}

        [RelayCommand]
        public void GoToCheckout() //Method that enables to go to checkoutpage
        {
            Shell.Current.GoToAsync("//CheckoutPage"); 
        }
    }
}
