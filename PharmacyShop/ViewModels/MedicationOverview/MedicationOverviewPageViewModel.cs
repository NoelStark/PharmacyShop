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

        public void Initialize()
        {
			if(medicationList.Count > 0)
				Medicine = new ObservableCollection<Medicine>(medicationList);

		}
		public MedicationOverviewPageViewModel(MedicineService medication, PersonService personService)
        {
            _medication = medication;
            _personService = personService;
			_= LoadMedicines();
            
			//Initialize();

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
      
        private async Task Filter(List<Medicine> filter)
        {

			var hashFilter = new HashSet<Medicine>(filter);
            for(int i = Medicine.Count - 1; i>= 0; i--)
            {
                if (!hashFilter.Contains(Medicine[i]))
                {
                    Medicine.RemoveAt(i);
                }
            }
            foreach(Medicine item in filter) 
            {
                if (!Medicine.Contains(item))
                {
                    Medicine.Add(item);
                }
            }
			//         var remove = Medicine.Except(filter).ToList();
			//         var add = filter.Except(Medicine).ToList();

			//         foreach (var item in remove)
			//         {
			//             Medicine.Remove(item);
			//         }
			//foreach (var item in add)
			//{
			//	Medicine.Add(item);
			//}
			//         sw.Stop();
			//         Console.WriteLine();
			await Task.Run(()=>OnPropertyChanged(nameof(Medicine)));
        }


    }
}
