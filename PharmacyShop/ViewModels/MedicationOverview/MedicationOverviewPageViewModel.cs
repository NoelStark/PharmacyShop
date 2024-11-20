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
                if(message.Value != "RefreshPage")
                {
                    this.SearchText = message.Value;
				    SearchProduct();
                }
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
			
		}
        [RelayCommand]
        public async Task<ObservableCollection<Medicine>> ReturnMedicineToDetails()
        {
            return Medicine;
        }
        private void Filter(List<Medicine> filter)
        {
            
            var remove = Medicine.Except(filter).ToList();
            var add = filter.Except(Medicine).ToList();

            foreach (var item in remove)
            {
                Medicine.Remove(item);
            }
			foreach (var item in add)
			{
				Medicine.Add(item);
			}
		    
        }


    }
}
