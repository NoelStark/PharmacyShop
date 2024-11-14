﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;

namespace PharmacyShop.ViewModels
{
    public partial class MedicationOverviewPageViewModel : ObservableObject
    {
        private readonly MedicineConfiguration _medication;
        
        private List<Medicine> medicationList;
        public ObservableCollection<Medicine> Medicine { get; set; } = new();

        [ObservableProperty]
        private string searchText;


        public MedicationOverviewPageViewModel(MedicineConfiguration medication)
        {
            _medication = medication;
            Task await = LoadMedicines();
        }


        [RelayCommand]
        public async Task LoadMedicines()
        {
            medicationList = await _medication.Medicines();

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

        partial void OnSearchTextChanged(string value)
        {
            SearchProduct();
        }

        [RelayCommand]
        public void SearchProduct()
        {
            if (SearchText != string.Empty)
            {
                var filterBySearchResult = medicationList.Where(a => a.Name.ToLower().Contains(SearchText.ToLower()) || a.Dose.ToLower().Contains(SearchText.ToLower()) || a.Description.ToLower().Contains(SearchText.ToLower())).ToList();
                Medicine.Clear();
                foreach (var medicine in filterBySearchResult)
                {
                    Medicine.Add(medicine);
                }
            }
            else
            {
                Medicine.Clear();
                foreach (var medicine in medicationList)
                {
                    Medicine.Add(medicine);
                }
            }
        }

        [RelayCommand]
        public async Task InspectChosenMedicine(Medicine InspectSelectedMedicine)
        {
            if(InspectSelectedMedicine != null)
            {
                
            }
        }

        [RelayCommand]
        public async Task BuyChosenMedicine(Medicine BuySelectedMedicine)
        {
            if (BuySelectedMedicine != null)
            {
                
            }
        }
    }
}
