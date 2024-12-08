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

		private ConcurrentDictionary<string, List<Medicine>> search = new(); //creating a ConcurrentDictionary som har nyckelvärde par
		private void FillSearch() //Metod som fyller i search
        {   //För snabbaste och bästa effektivitet så används Parallel, det fyller sökindexet parallellt med data från en lista över mediciner.
            Parallel.ForEach(medicationList, item =>
			{
				//Lägger till dessa i sökindexet
				AddToSearch(item.Name, item);
				AddToSearch(item.Dose, item);
				AddToSearch(item.Description, item);
			});		
		}

		private void AddToSearch(string text, Medicine medicine) //Methods to add medicines to a list based on the users input in the searchbar
		{
			List<Medicine> list = search.GetOrAdd(text, _ => new List<Medicine>()); //hämtar alla sökindex och nyckelvärde par som sedan läggs till i list
			lock (list) //säkerställer att endast en tråd åt gången kan arbeta med listan vilket gör den snabbare
            {
				list.Add(medicine); //lägger till dem i listan
			}
	
		}

		[RelayCommand]
		public void SearchProduct() //Metod som hanterar sökord från användarens input i sökfältet
		{
			List<Medicine> foundMedicines = new List<Medicine>(); //skapar en ny lista
			if (SearchText == string.Empty) //Om sökfältet är tomt så laddas alla mediciner in
			{
				_= Filter(medicationList);
			}
			else
			{
				bool foundMatch = false;
				HashSet<Medicine> result = new HashSet<Medicine>();
				foreach(string key in search.Keys) //Söker igenom alla nyklar ifrån search listan
				{
					if (key.ToLower().Contains(SearchText.ToLower())) //Om nycklar hittas baserad på användarens sökord
					{
						if (search.TryGetValue(key, out List<Medicine>? medicines)) //Hämtar värdet från nycklen
						{
							foundMedicines.AddRange(medicines); //lägger till elementet sist i listan
							foundMatch = true; //Säkerställer att en match har hittats
						}
					}
				}
				_= Filter(foundMatch ? foundMedicines : new List<Medicine>()); //Anropar metoden för filtrering med lista som parameter
			}
		}


		[RelayCommand]
		public void GoToCheckout() //If user pressed on the checkout icon
		{
			Shell.Current.GoToAsync("//CheckoutPage"); //route to CheckoutPage
        }


		[RelayCommand]
		public void InspectChosenMedicine(Medicine InspectSelectedMedicine) //If user presses on a medicine and want to inspect it
        {
			if (InspectSelectedMedicine != null) //If there is a selected medicine
			{
				_medication.CurrentMedicine = InspectSelectedMedicine; //Get the insepcted medicine, gives it value to CurrentMedicen
				WeakReferenceMessenger.Default.Send(new ValueChangedMessage<string>("RefreshPage")); //sends message that a new medicine is selected and therefore the page needs to be refreshed
				Shell.Current.GoToAsync("MedicationDetailsPage", true); //Route to MedicationDetailsPage
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
