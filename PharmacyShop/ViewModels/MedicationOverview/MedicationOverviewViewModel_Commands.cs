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
		public async Task LoadMedicines() //Method to load all medicines
		{
			medicationList = _medication.Medicines(); //Loads medicines to medication list from medicationservice
			
			if (medicationList.Any()) //if there are medicines
			{		
				Medicine = new System.Collections.ObjectModel.ObservableCollection<Medicine>(medicationList); //Add them to the observablecollection Medicine list
				OnPropertyChanged(nameof(Medicine));
			}	
			FillSearch();
		}

	
		[RelayCommand]
        public void ShowFilter() //if user clicks on filter icon
		{
			if (IsVisible == false)
			{ 
				IsVisible = true; //Show filter
				SearchAndFilterIsVisible = false; //Close everyting else except filter
            }
			else
			{
                IsVisible = false; //Do not show filter
                SearchAndFilterIsVisible = true; //Show everyting else except filter
            }
        }


        [RelayCommand]
		public void FilterMedicines() //If user wants to filter medicines with the filter function
		{
			//initializes decimals with max and min values so there cant be numbers higher or lower
            decimal minprice = decimal.MinValue;
			decimal maxprice = decimal.MaxValue;

			if (!string.IsNullOrEmpty(UserInputMinValue)) //Checks if user have typed in min value
			{
				try
				{
					decimal.TryParse(UserInputMinValue, out decimal minValue); //Converting the user input to decimal
					minprice = minValue; //replacing the minprice with users min value
				}
				catch { }
			}

			if (!string.IsNullOrEmpty(UserInputMaxValue)) //Checks if user have typed in max value
            {
				try
				{
					decimal.TryParse(UserInputMaxValue, out decimal maxValue); //Converting the user input to decimal
                    maxprice = maxValue; //replacing the maxprice with users min value
                }
				catch { }
			}
            //Lamda expression to filter if user checked some of the checkboxes
            List<string> active = filterDictionary.Where(x => x.Value).Select(x => x.Key.ToLower()).ToList();

			//Lamda expression that filters by checking checkboxes and users min or max values
			var filter = medicationList.Where(a => !active.Any() || active.Any(y => a.Description.ToLower().Contains(y)))
				 .Where(b => b.Information?.ItemPrice >= minprice && b.Information.ItemPrice <= maxprice).ToList();

            _= Filter(filter); //Call method filter with filter as a parameter
            IsVisible = false;
            SearchAndFilterIsVisible = true;
        }
		private ConcurrentDictionary<string, List<Medicine>> search = new();

        //Metod that fills search. Foor the fastest and most efficient performance, Parallel is used to populate the search index concurrently with data from a list of medications
        private void FillSearch() 
        {   
            Parallel.ForEach(medicationList, item =>
			{
				AddToSearch(item.Name, item);
				AddToSearch(item.Dose, item);
				AddToSearch(item.Description, item);
			});		
		}

        //Methods to add medicines to a list based on the users input in the searchbar. Retrieves all search indexes and key-value pairs, which are then added to a list
        private void AddToSearch(string text, Medicine medicine)
		{
			List<Medicine> list = search.GetOrAdd(text, _ => new List<Medicine>());
			lock (list)
            {
				list.Add(medicine);
			}
	
		}

		[RelayCommand]
		public void SearchProduct() //Method that handles search terms from the user input in the search field
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
					if (key.ToLower().Contains(SearchText.ToLower())) //If key is found based on the users search
					{
						if (search.TryGetValue(key, out List<Medicine>? medicines)) //Retrieves the key value
						{
							foundMedicines.AddRange(medicines);
							foundMatch = true;
						}
					}
				}
				_= Filter(foundMatch ? foundMedicines : new List<Medicine>()); //Calls the method for filtering, with list as parameter
			}
		}


		[RelayCommand]
		public void GoToCheckout() //If user pressed on the checkout icon, direct to CheckoutPage
		{
			Shell.Current.GoToAsync("//CheckoutPage");
        }


		[RelayCommand]
		public void InspectChosenMedicine(Medicine InspectSelectedMedicine) //If user presses on a medicine and want to inspect it
        {
			if (InspectSelectedMedicine != null)
			{
				_medication.CurrentMedicine = InspectSelectedMedicine; //Get the insepcted medicine, gives it value to CurrentMedicen
				if(Shell.Current.CurrentPage.GetType() != typeof(MedicationDetailsPage))
				{

					Shell.Current.GoToAsync("MedicationDetailsPage", true); //Route to MedicationDetailsPage
				}
				else
				{
					WeakReferenceMessenger.Default.Send(new ValueChangedMessage<string>("RefreshPage")); //sends message that a new medicine is selected and therefore the page needs to be refreshed
				}
            }
		}


		[RelayCommand]
		public void BuyChosenMedicine(Medicine BuySelectedMedicine) //If user wants to buy a medicine by pressing on the buy
		{
			if (BuySelectedMedicine != null) //if there is a medicine selected
			{
				Cart? cart = _personService.ItemsCart.FirstOrDefault(x => x.Medicine == BuySelectedMedicine); //Add the item to the cart

				if (cart == null) //If there are no items in the cart
				{
					_personService.ItemsCart.Add(new Cart //Create new cart information
					{
						Medicine = BuySelectedMedicine,
						Quantity = quantity,
						Information = BuySelectedMedicine.Information
					});
				}
				else 
				{
					Cart? item = _personService.ItemsCart.Find(a => a == cart); //Find the current cart information
					if(item != null)
						item.Quantity += quantity; //add the totla cuantity
				}
				quantity = 1;
				//Function to create popup with a message that an item have been added to the cart
				PopupView? popup = new PopupView(this);
				if(Application.Current != null)
					if(Application.Current.MainPage != null)
						Application.Current.MainPage.ShowPopup(popup);
			}
		}
	}
}
