using CommunityToolkit.Mvvm.ComponentModel;
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

		}

		public MedicationDetailsViewModel(MedicineService medicineService)
        {
			_medicineService = medicineService;
			FillFields();

			MessagingCenter.Subscribe<MedicationOverviewPageViewModel, List<Medicine>>(this, "FilteredMedicineList", (sender, Result) =>
			{
				Medicines.Clear();
				foreach(Medicine medicine in Result)
				{
					Medicines.Add(medicine);
				}

			});

		}

		private void FillFields()
		{
			Medicine medicine = _medicineService.CurrentMedicine;
			Substance = medicine.Substance;
			Dosage = medicine.Dose;
			Usage = medicine.Description;
			Amount = "20";
			Options.Add(medicine.Useage);
			CurrentMedicine = medicine;
			Title = medicine.Name + " " + medicine.Dose;
		}
	}
}
