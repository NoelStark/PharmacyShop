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
		private int quantity = 1; //Säkerställer att kvanititet alltid är 1 vid tillagd medicin i varukorg

		private List<Medicine> medicationList = new(); //Ny lista för mediciner
		public ObservableCollection<Medicine> Medicine { get; private set; } = new(); //Ny observable collection Medicine

		//ObserbavleProperties som är bindade til xaml koden

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

        //Skapar en dictionary som håller nyklar vilket är namnet som användaren kan filtrera på med hjälp av checkboxes, med default värde
        private readonly Dictionary<string, bool> filterDictionary = new()
		{
			{"Filmdragerad", false },
			{"Brustablett", false },
			{"Oral", false },
			{"Flytande", false },
			{"Dragerad", false },
		};

		//On property change metod för sökord från användaren
		partial void OnSearchTextChanged(string value)
		{
			SearchProduct();
		}
        //On property change metoder från filter baserade checkboxes från användaren
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
