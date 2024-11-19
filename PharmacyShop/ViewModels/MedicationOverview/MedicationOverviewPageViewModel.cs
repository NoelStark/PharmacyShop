using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using PharmacyShop.Models;
using PharmacyShop.Services;
using PharmacyShop.ViewModels.MedicationDetails;
using PharmacyShop.Views;

namespace PharmacyShop.ViewModels.MedicationOverview
{
    public partial class MedicationOverviewPageViewModel : ObservableObject
    {
        private readonly MedicineService _medication;
        private readonly PersonService _personService;


		public MedicationOverviewPageViewModel(MedicineService medication, PersonService personService)
        {
            _medication = medication;
            _personService = personService;
            LoadMedicines();
			WeakReferenceMessenger.Default.Register<ValueChangedMessage<string>>(this, (recipient, message) =>
			{
                this.SearchText = message.Value;
				SearchProduct();
			});

			WeakReferenceMessenger.Default.Register<ValueChangedMessage<Medicine>>(this, (recipient, message) =>
			{
                InspectChosenMedicine(message.Value);
            });
            WeakReferenceMessenger.Default.Register<ValueChangedMessage<Dictionary<Medicine, int>>>(this, (recipient, message) =>
            {
                var item = message.Value.First();
                quantity = item.Value;
                BuyChosenMedicine(item.Key);
            });
			//MessagingCenter.Subscribe<MedicationDetailsViewModel, Dictionary<Medicine, int>> (this, "AddToCart", (sender, myDict) =>
			//{
   //             var item = myDict.First();
   //             quantity = item.Value;
   //             BuyChosenMedicine(item.Key);
			//});
		}

     
        private void Filter(List<Medicine> filter)
        {
            Medicine.Clear();
            foreach (var medicine in filter)
            {
                Medicine.Add(medicine);
            }
        }


    }
}
