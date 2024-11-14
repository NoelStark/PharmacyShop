using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;

namespace PharmacyShop.ViewModels
{
    public partial class MedicationOverviewPageViewModel
    {
        private readonly MedicineConfiguration _medication;
        public ObservableCollection<Medicine> Medicine { get; set; } = new();

        public ObservableCollection<string> Searchbar = new ObservableCollection<string>();

        public MedicationOverviewPageViewModel(MedicineConfiguration medication)
        {
            _medication = medication;
            Task await = GetMedicines();
        }

        [RelayCommand]
        public async Task GetMedicines()
        {
            List<Medicine> medicationList = await _medication.Medicines();

            if (Medicine.Any())
            {
                Medicine.Clear();
            }

            if (medicationList.Any())
            {
                foreach (var medicine in medicationList)
                {
                    Medicine.Add(medicine);
                }
            }

        }

        [RelayCommand]
        public async Task SearchProduct(string search)
        {
            Console.WriteLine();
        }

        [RelayCommand]
        public async Task GetMedicine(Medicine medicine)
        {
            if(medicine != null)
            {
                Console.WriteLine("");
            }
        }
    }
}
