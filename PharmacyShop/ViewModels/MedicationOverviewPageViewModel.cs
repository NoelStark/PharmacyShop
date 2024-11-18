﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;
using PharmacyShop.Services;

namespace PharmacyShop.ViewModels
{
    public partial class MedicationOverviewPageViewModel : ObservableObject
    {
        private readonly MedicineService _medication;
        private readonly PersonService _personService;
        private int quantity = 1;

		private List<Medicine> medicationList;
        public ObservableCollection<Medicine> Medicine { get; set; } = new();

        [ObservableProperty]
        private string searchText;


        public MedicationOverviewPageViewModel(MedicineService medication, PersonService personService)
        {
            _medication = medication;
            _personService = personService;
            Task await = LoadMedicines();
			MessagingCenter.Subscribe<MedicationDetailsViewModel, string>(this, "SearchTextChanged", (sender, SearchText) =>
			{
                this.SearchText = SearchText;
				SearchProduct();
			});

            MessagingCenter.Subscribe<MedicationDetailsViewModel, Medicine>(this, "MedicineClicked", (sender, medicine) =>
            {
                _= InspectChosenMedicine(medicine);
            });

			MessagingCenter.Subscribe<MedicationDetailsViewModel, Dictionary<Medicine, int>> (this, "AddToCart", (sender, myDict) =>
			{
                var item = myDict.First();
                quantity = item.Value;
                _= BuyChosenMedicine(item.Key);
			});
		}

        [RelayCommand]
        public async Task LoadMedicines()
        {
            medicationList = await _medication.Medicines();

            if (Medicine.Any())
            {
                Medicine.Clear();
            }
            //string filtName = "";
            //string filtDose = "";
            //string filtDescription = "Oral";

            //string userInputMinValue = "40.00";
            //string userInputMaxValue = "50.00";
            //decimal minprice = decimal.MinValue;
            //decimal maxprice = decimal.MaxValue;

            //if (userInputMinValue.Any())
            //{
            //    try
            //    {
            //        decimal.TryParse(userInputMinValue, out decimal minValue);
            //        minprice = minValue;
            //    }
            //    catch
            //    {
            //        Console.WriteLine("Worng");
            //    }
            //}

            //if (userInputMaxValue.Any())
            //{
            //    try
            //    {
            //        decimal.TryParse(userInputMaxValue, out decimal maxValue);
            //        maxprice = maxValue;
            //    }
            //    catch
            //    {
            //        Console.WriteLine("Worng");
            //    }
            //}



            //var filter = medicationList.Where(a => a.Name.Contains(filtName) && a.Dose.Contains(filtDose) && a.Description.Contains(filtDescription)).Where(b => b.Price >= minprice && b.Price <= maxprice).ToList();
            //await Filter(filter);
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
        public async Task Filter(List<Medicine> filter)
        {
            Medicine.Clear();
            foreach (var medicine in filter)
            {
                Medicine.Add(medicine);
            }
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

                MessagingCenter.Send(this, "FilteredMedicineList", filterBySearchResult);
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
        public async Task GoToCheckout()
        {
            await Shell.Current.GoToAsync("//Checkout/CheckoutPage");
        }


        [RelayCommand]
        public async Task InspectChosenMedicine(Medicine InspectSelectedMedicine)
        {
            if(InspectSelectedMedicine != null)
            {
				_medication.CurrentMedicine = InspectSelectedMedicine;
                MessagingCenter.Send(this, "RefreshPage");
				await Shell.Current.GoToAsync("//MedicationDetailsPage");
            }
        }


        [RelayCommand]
        public async Task BuyChosenMedicine(Medicine BuySelectedMedicine)
        {
            if (BuySelectedMedicine != null)
            {
                Cart? cart = _personService.ItemsCart.FirstOrDefault(x => x.Medicine == BuySelectedMedicine);

				if (cart == null)
                {
					_personService.ItemsCart.Add(new Cart
					{
						Medicine = BuySelectedMedicine,
						Quantity = quantity,
						Information = BuySelectedMedicine.Information
					});
				}
                else
                {
                    _personService.ItemsCart.Find(a => a == cart).Quantity += quantity;
                }
                quantity = 1;
            }
        }
    }
}
