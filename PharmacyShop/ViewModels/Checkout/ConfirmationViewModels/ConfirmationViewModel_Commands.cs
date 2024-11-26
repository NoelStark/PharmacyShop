using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels.Checkout.ConfirmationViewModels
{
	public partial class ConfirmationViewModel : ObservableObject
	{
		/// <summary>
		/// Method that handles what happens when the 'Confirm' button is pressed with the purpose
		/// of sending user back to the Overview page
		/// </summary>
		/// <returns></returns>
		[RelayCommand]
		async Task Confirm()
		{
			bool paymentSaved = Preferences.Get("ShouldSavePayment", false);
			bool personalInfoSaved = Preferences.Get("ShouldSaveInfo", false);

			//If statement that clears the payment information if user doesnt want to save it
			if (!paymentSaved)
				_personService.PaymentInfo = new PaymentInfo();
			if (!personalInfoSaved)
				_personService.CurrentPerson = new Person();
			
			_personService.ItemsCart.Clear();
			//Goes back to the overview page
			await Shell.Current.GoToAsync("//MedicationOverviewPage");
		}
	}
}
