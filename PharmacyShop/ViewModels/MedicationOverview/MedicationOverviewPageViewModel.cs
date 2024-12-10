using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        //Initializes the Medicine with values from medicationList
        public void Initialize()
        {
			if(medicationList.Count > 0) //If there are items in the medicationList, add them to Medicine list
                Medicine = new ObservableCollection<Medicine>(medicationList);
		}
        
		public MedicationOverviewPageViewModel(MedicineService medication, PersonService personService)
        {
            //Creating dependanyc injection
            _medication = medication;
            _personService = personService;
            //Loads all medicines
            _ = LoadMedicines();
            
            /// <summary>
            /// The application uses WeakReferenceMessenger handle updates and actions based on specific messages. 
            /// It unregisters the current object from handling three types of messages, then registers new handelrs
            /// </summary>
            WeakReferenceMessenger.Default.Unregister<ValueChangedMessage<string>>(this);
			WeakReferenceMessenger.Default.Unregister<ValueChangedMessage<Medicine>>(this);
			WeakReferenceMessenger.Default.Unregister<ValueChangedMessage<Dictionary<Medicine, int>>>(this);

			WeakReferenceMessenger.Default.Register<ValueChangedMessage<string>>(this, (recipient, message) =>
			{
				if (message.Value != "RefreshPage")
				{
					this.SearchText = message.Value;
					SearchProduct();
				}
			});

			WeakReferenceMessenger.Default.Register<ValueChangedMessage<Medicine>>(this, (recipient, message) =>
			{
				SearchText = string.Empty;
				InspectChosenMedicine(message.Value);
			});
			WeakReferenceMessenger.Default.Register<ValueChangedMessage<Dictionary<Medicine, int>>>(this, (recipient, message) =>
			{
				var item = message.Value.First();
				quantity = item.Value;
				BuyChosenMedicine(item.Key);
			});
		}
      
        private async Task Filter(List<Medicine> filter) //This method is used to filter from the users inputs, takes a parameter
        {
			HashSet<Medicine> hashFilter = new HashSet<Medicine>(filter); //HashSet list with Medicine that gets values from the parameter
            for (int i = Medicine.Count - 1; i>= 0; i--) //iterates x times based on the total amount of items in Medicine list
            {
                if (!hashFilter.Contains(Medicine[i])) //Checks if items in the Medicine list does not contain items from the hashfilter
                {
                    Medicine.RemoveAt(i); //If there are, they will be removed
                }
            }
            foreach(Medicine item in filter)
            {
                if (!Medicine.Contains(item)) //if there are items that deos not exist in the Medicen list but in the filter list
                {
                    Medicine.Add(item); //if there is, add them to the Medicine list
                }
            }
			await Task.Run(()=>OnPropertyChanged(nameof(Medicine))); //On property change of Medicine with an await and task operation
        }


    }
}
