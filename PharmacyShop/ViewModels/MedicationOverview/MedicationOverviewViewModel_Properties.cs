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
		public ObservableCollection<Medicine> Medicine { get; private set; } = new();

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

		private readonly Dictionary<string, bool> filterDictionary = new()
		{
			{"Filmdragerad", false },
			{"Brustablett", false },
			{"Oral", false },
			{"Flytande", false },
			{"Dragerad", false },
		};

		partial void OnSearchTextChanged(string value)
		{
			SearchProduct();
		}

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
