using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using PharmacyShop.Models;
using PharmacyShop.Views;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels.MedicationOverview
{
	public partial class MedicationOverviewPageViewModel : ObservableObject
	{

		[RelayCommand]
		public async Task LoadMedicines()
		{
			medicationList = _medication.Medicines();
			
			if (medicationList.Any())
			{		
				Medicine = new System.Collections.ObjectModel.ObservableCollection<Medicine>(medicationList);
				OnPropertyChanged(nameof(Medicine));
			}	
			FillSearch();
		}

	
		[RelayCommand]
        public void ShowFilter()
		{
			if (IsVisible == false)
			{ 
				IsVisible = true;
				SearchAndFilterIsVisible = false;
            }
			else
			{
                IsVisible = false;
                SearchAndFilterIsVisible = true;
            }
        }


        [RelayCommand]
		public void FilterMedicines()
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

			var filter = medicationList.Where(a => !active.Any() || active.Any(y => a.Description.ToLower().Contains(y)))
				 .Where(b => b.Information?.ItemPrice >= minprice && b.Information.ItemPrice <= maxprice).ToList();

            _= Filter(filter);
            IsVisible = false;
            SearchAndFilterIsVisible = true;
        }

		//private Dictionary<string, List<Medicine>> search = new();
		private ConcurrentDictionary<string, List<Medicine>> search = new();
		private void FillSearch()
		{	
			Parallel.ForEach(medicationList, item =>
			{
				AddToSearch(item.Name, item);
				AddToSearch(item.Dose, item);
				AddToSearch(item.Description, item);
			});		
		}

		private void AddToSearch(string text, Medicine medicine)
		{
			var list = search.GetOrAdd(text, _ => new List<Medicine>());
			lock (list)
			{
				list.Add(medicine);
			}
	
		}

		[RelayCommand]
		public void SearchProduct()
		{

			List<Medicine> foundMedicines = new List<Medicine>();
			if (SearchText == string.Empty)
			{
				_= Filter(medicationList);
			}
			else
			{
				bool foundMatch = false;
				HashSet<Medicine> result = new HashSet<Medicine>();
				foreach(string key in search.Keys)
				{
					if (key.ToLower().Contains(SearchText.ToLower()))
					{
						if (search.TryGetValue(key, out List<Medicine>? medicines))
						{
							foundMedicines.AddRange(medicines);
							foundMatch = true;
						}
					}
				}
				_= Filter(foundMatch ? foundMedicines : new List<Medicine>());
			}


			//if (SearchText != string.Empty)
			//{
			//	var filterBySearchResult = medicationList.Where(a => a.Name.ToLower().Contains(SearchText.ToLower()) || a.Dose.ToLower().Contains(SearchText.ToLower()) || a.Description.ToLower().Contains(SearchText.ToLower())).ToList();

			//	Filter(filterBySearchResult);

			//	WeakReferenceMessenger.Default.Send(new ValueChangedMessage<List<Medicine>>(filterBySearchResult));
			//}
			//else
			//{
			//	Filter(medicationList);
			//}
		}


		[RelayCommand]
		public void GoToCheckout()
		{
			Shell.Current.GoToAsync("//CheckoutPage");
		}


		[RelayCommand]
		public void InspectChosenMedicine(Medicine InspectSelectedMedicine)
		{
			if (InspectSelectedMedicine != null)
			{
				_medication.CurrentMedicine = InspectSelectedMedicine;
				WeakReferenceMessenger.Default.Send(new ValueChangedMessage<string>("RefreshPage"));
				Shell.Current.GoToAsync("//MedicationDetailsPage");
			}
		}


		[RelayCommand]
		public void BuyChosenMedicine(Medicine BuySelectedMedicine)
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
					Cart? item = _personService.ItemsCart.Find(a => a == cart);
					if(item != null)
						item.Quantity += quantity;
				}
				quantity = 1;
				PopupView? popup = new PopupView();
				if(Application.Current != null)
					if(Application.Current.MainPage != null)
						Application.Current.MainPage.ShowPopup(popup);
			}
		}
	}
}
