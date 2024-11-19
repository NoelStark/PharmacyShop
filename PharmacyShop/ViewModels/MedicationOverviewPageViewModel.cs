using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;
using PharmacyShop.Services;
using PharmacyShop.Views;

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

        [ObservableProperty]
        public bool filmdragerad;

        [ObservableProperty]
        public bool brustablett;

        [ObservableProperty]
        public bool flytande;

        [ObservableProperty]
        public bool oral;

        [ObservableProperty]
        public bool dragerad;

        [ObservableProperty]
        private string userInputMinValue;

        [ObservableProperty]
        private string userInputMaxValue;

        private readonly Dictionary<string, bool> filterDictionary = new()
        {
            {"Filmdragerad", false },
            {"Brustablett", false },
            {"Oral", false },
            {"Flytande", false },
            {"Dragerad", false },
		};



        //[ObservableProperty]
        //private string filtDose;

        //[ObservableProperty]
        //private string filtDescription;


        private string? filterFilmdragerad;
        private string? filterBrustablett;
        private string? filterFlyTande;
        private string? filterOral;
        private string? filterDragerad;


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

        partial void OnFilmdrageradChanged(bool checkedBox)
        {
            //if (checkedBox)
            //    filterFilmdragerad = "Filmdragerad";
            //else
            //    filterFilmdragerad = string.Empty;
            filterDictionary["Filmdragerad"] = checkedBox;
        }

        partial void OnBrustablettChanged(bool checkedBox)
        {
			//if (checkedBox)
			//    filterBrustablett = "Brustablett";
			//else
			//    filterBrustablett = string.Empty;
			filterDictionary["Brustablett"] = checkedBox;

		}

		partial void OnOralChanged(bool checkedBox)
        {
			//if (checkedBox)
			//    filterOral = "Oral";
			//else
			//    filterOral = string.Empty;
			filterDictionary["Oral"] = checkedBox;

		}

		partial void OnFlytandeChanged(bool checkedBox)
        {
			//    if (checkedBox)
			//        filterFlyTande = "Flytande";
			//    else
			//        filterFlyTande = string.Empty;
			filterDictionary["Flytande"] = checkedBox;

		}

		partial void OnDrageradChanged(bool checkedBox)
        {
			//if (checkedBox)
			//    filterDragerad = "Dragerad";
			//else
			//    filterDragerad = string.Empty;
			filterDictionary["Dragerad"] = checkedBox;

		}


		[RelayCommand]
        public async Task FilterMedicines()
        {
          
			decimal minprice = decimal.MinValue;
            decimal maxprice = decimal.MaxValue;

            if (!string.IsNullOrEmpty(UserInputMinValue))
            {
                try
                {
                    decimal.TryParse(UserInputMinValue, out decimal minValue);
                    minprice = minValue;
                }
                catch { }
            }

            if (!string.IsNullOrEmpty(UserInputMaxValue))
            {
                try
                {
                    decimal.TryParse(UserInputMaxValue, out decimal maxValue);
                    maxprice = maxValue;
                }
                catch { }
            }

            List<string> active = filterDictionary.Where(x => x.Value).Select(x => x.Key.ToLower()).ToList();

            var filter = medicationList.Where(a => active.Any(y => a.Description.ToLower().Contains(y)))
                 .Where(b => b.Information.ItemPrice >= minprice && b.Information.ItemPrice <= maxprice).ToList();


			//var filter = medicationList.Where(a => a.Description.ToLower().Contains(filterFilmdragerad.ToLower()) 
			//|| a.Description.ToLower().Contains(filterBrustablett.ToLower()) 
			//|| a.Description.ToLower().Contains(filterFlyTande.ToLower()) 
			//|| a.Description.ToLower().Contains(filterDragerad.ToLower()) 
			//|| a.Description.ToLower().Contains(filterOral.ToLower()))
			//    .Where(b => b.Information.ItemPrice >= minprice && b.Information.ItemPrice <= maxprice).ToList();
			_ = Filter(filter);
        }

        private async Task Filter(List<Medicine> filter)
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
            await Shell.Current.GoToAsync("//CheckoutPage");
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
                var popup = new PopupView();
                Application.Current.MainPage.ShowPopup(popup);
            }
        }
    }
}
