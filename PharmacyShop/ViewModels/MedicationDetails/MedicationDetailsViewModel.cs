using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using PharmacyShop.Models;
using PharmacyShop.Services;
using PharmacyShop.ViewModels.MedicationOverview;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels.MedicationDetails
{
	public partial class MedicationDetailsViewModel : ObservableObject
	{
		private readonly MedicineService _medicineService;
		private readonly MedicationOverviewPageViewModel _medicationOverview;

		public MedicationDetailsViewModel(MedicineService medicineService, MedicationOverviewPageViewModel medicationOverview)
        {
			_medicineService = medicineService;
			_medicationOverview = medicationOverview;
		}

		public void Reinitialize()
		{

			FillFields();
			IsSearchVisible = false;
			// Register for the message only if not already registered
			WeakReferenceMessenger.Default.Register<ValueChangedMessage<List<Medicine>>>(this, (recipient, message) =>
			{
				Medicines.Clear();
				foreach (Medicine medicine in message.Value)
				{
					Medicines.Add(medicine);
				}
			});
			
			WeakReferenceMessenger.Default.Register<ValueChangedMessage<string>>(this, (recipient, message) =>
			{
				if (message.Value == "RefreshPage")
				{
					// Logic to handle the message
					IsSearchVisible = false;
					FillFields();
				}
			});
			
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
	}
}
