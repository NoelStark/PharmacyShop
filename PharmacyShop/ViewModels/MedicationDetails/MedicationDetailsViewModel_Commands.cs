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

namespace PharmacyShop.ViewModels.MedicationDetails
{
	public partial class MedicationDetailsViewModel : ObservableObject
	{
		[RelayCommand]
		void MedicineClick(Medicine medicine)
		{
			WeakReferenceMessenger.Default.Send(new ValueChangedMessage<Medicine>(medicine));
		}

		[RelayCommand]
		void IncreaseQuantity()
		{
			Quantity++;
		}

		[RelayCommand]
		void DecreaseQuantity()
		{
			if (Quantity > 1)
			{
				Quantity--;
			}
		}
		[RelayCommand]
		void AddToCart()
		{
			var medicineQuantity = new Dictionary<Medicine, int> { { _medicineService.CurrentMedicine, Quantity } };

			WeakReferenceMessenger.Default.Send(new ValueChangedMessage<Dictionary<Medicine, int>>(medicineQuantity));
		}

        [RelayCommand]
        public void GoToCheckout()
        {
            Shell.Current.GoToAsync("//CheckoutPage");
        }
    }
}
