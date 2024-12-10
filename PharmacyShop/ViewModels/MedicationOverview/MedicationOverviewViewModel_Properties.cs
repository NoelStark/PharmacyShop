using CommunityToolkit.Mvvm.ComponentModel;
using PharmacyShop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels.MedicationOverview
{
	public partial class MedicationOverviewPageViewModel : ObservableObject
	{
		private int quantity = 1;

		private List<Medicine> medicationList = new();
		public ObservableCollection<Medicine> Medicine { get; private set; } = new(); //new observable collection for Medicine

		//ObserbavleProperties that are binded to the xaml code

		[ObservableProperty]
		private string searchText = string.Empty;

        [ObservableProperty]
        private bool isVisible = false;

        [ObservableProperty]
        private bool searchAndFilterIsVisible = true;

        [ObservableProperty]
		public bool filmdragerad = false;

		[ObservableProperty]
		public bool brustablett = false;

		[ObservableProperty]
		public bool flytande = false;

		[ObservableProperty]
		public bool oral = false;

		[ObservableProperty]
		public bool dragerad = false;

		[ObservableProperty]
		private string userInputMinValue = string.Empty;

		[ObservableProperty]
		private string userInputMaxValue = string.Empty;

        //Creates a dictionary that holds keys representing the names users can filter by using checkboxes, with default values
        private readonly Dictionary<string, bool> filterDictionary = new()
		{
			{"Filmdragerad", false },
			{"Brustablett", false },
			{"Oral", false },
			{"Flytande", false },
			{"Dragerad", false },
		};

		//On property change method from users input in searchbar
		partial void OnSearchTextChanged(string value)
		{
			SearchProduct();
		}
        //On property change methods of filterbased checkboxes from users input
        partial void OnFilmdrageradChanged(bool value)
		{
			filterDictionary["Filmdragerad"] = value;
		}

		partial void OnBrustablettChanged(bool value)
		{
			filterDictionary["Brustablett"] = value;
		}

		partial void OnOralChanged(bool value)
		{
			filterDictionary["Oral"] = value;
		}

		partial void OnFlytandeChanged(bool value)
		{
			filterDictionary["Flytande"] = value;
		}

		partial void OnDrageradChanged(bool value)
		{
			filterDictionary["Dragerad"] = value;
		}
	}
}
