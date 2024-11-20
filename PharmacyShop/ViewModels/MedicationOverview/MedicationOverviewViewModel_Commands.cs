using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using PharmacyShop.Models;
using PharmacyShop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels.MedicationOverview
{
	public partial class MedicationOverviewPageViewModel : ObservableObject
	{
		[RelayCommand]
		public void LoadMedicines()
		{
			medicationList = _medication.Medicines();

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

			var filter = await Task.Run(() =>
			{
				return medicationList.Where(a => !active.Any() || active.Any(y => a.Description.ToLower().Contains(y)))
				 .Where(b => b.Information?.ItemPrice >= minprice && b.Information.ItemPrice <= maxprice).ToList();
			});

            Filter(filter);
            IsVisible = false;
            SearchAndFilterIsVisible = true;
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

				//MessagingCenter.Send(this, "FilteredMedicineList", filterBySearchResult);
				WeakReferenceMessenger.Default.Send(new ValueChangedMessage<List<Medicine>>(filterBySearchResult));
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
				WeakReferenceMessenger.Default.Send(this, "RefreshPage");
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
					var item = _personService.ItemsCart.Find(a => a == cart);
					if(item != null)
						item.Quantity += quantity;
				}
				quantity = 1;
				var popup = new PopupView();
				if(Application.Current != null)
					if(Application.Current.MainPage != null)
						Application.Current.MainPage.ShowPopup(popup);
			}
		}
	}
}
