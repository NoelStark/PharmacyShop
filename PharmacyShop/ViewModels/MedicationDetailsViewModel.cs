using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PharmacyShop.Models;
using PharmacyShop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels
{
	public partial class MedicationDetailsViewModel : ObservableObject
	{

        public ObservableCollection<string> Options { get; set; } = new();
		private readonly MedicineService _medicineService;

		public ObservableCollection<Medicine> Medicines { get; set;} = new();

		[ObservableProperty]
		private int quantity;

		[ObservableProperty]
		private bool isSearchVisible;

		[ObservableProperty]
		private string title;

		[ObservableProperty]
		private string substance;

		[ObservableProperty]
		private string dosage;

		[ObservableProperty]
		private string usage;

		[ObservableProperty]
		private string amount;

		[ObservableProperty]
		private Medicine currentMedicine;

		[ObservableProperty]
		public string searchText;

		partial void OnSearchTextChanged(string value)
		{
			MessagingCenter.Send(this, "SearchTextChanged", value);
			IsSearchVisible = true;
		}

		[RelayCommand]
		async Task MedicineClick(Medicine medicine)
		{
			MessagingCenter.Send(this, "MedicineClicked", medicine);

		}

		public void Reinitialize()
		{

			FillFields();
			IsSearchVisible = false;
			MessagingCenter.Subscribe<MedicationOverviewPageViewModel, List<Medicine>>(this, "FilteredMedicineList", (sender, Result) =>
			{
				Medicines.Clear();
				foreach (Medicine medicine in Result)
				{
					Medicines.Add(medicine);
				}
			});

			MessagingCenter.Subscribe<MedicationOverviewPageViewModel>(this, "RefreshPage", (sender) =>
			{
				IsSearchVisible = false;
				FillFields();

			});
		}

		public MedicationDetailsViewModel(MedicineService medicineService)
        {
			_medicineService = medicineService;
			Reinitialize();

		}

		private void FillFields()
		{
			Options.Clear();
			Medicine medicine = _medicineService.CurrentMedicine;
			Substance = medicine.Substance;
			Dosage = medicine.Dose;
			Usage = medicine.Description;
			Amount = medicine.Amount;
			Options.Add(medicine.Useage);
			CurrentMedicine = medicine;
			Title = medicine.Name + " " + medicine.Dose;
			Quantity = 1;
		}

		[RelayCommand]
		async Task IncreaseQuantity()
		{
			Quantity++;
		}

		[RelayCommand]
		async Task DecreaseQuantity()
		{
			if(Quantity> 1)
			{
				Quantity--;
			}
		}
		[RelayCommand]
		async Task AddToCart()
		{
			MessagingCenter.Send(this, "AddToCart", new Dictionary<Medicine, int> { { _medicineService.CurrentMedicine, Quantity } });
		}
	}
}
