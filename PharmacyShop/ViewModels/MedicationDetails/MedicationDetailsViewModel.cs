﻿using CommunityToolkit.Mvvm.ComponentModel;
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
			_medicines = new ObservableCollection<Medicine>();
		}

		public void Reinitialize() //Reinitializes the page
        {
			FillFields();
			IsSearchVisible = false;
			SearchText = string.Empty;
			if (!WeakReferenceMessenger.Default.IsRegistered<ValueChangedMessage<List<Medicine>>>(this)) //If the user is not on the same medication
			{
				WeakReferenceMessenger.Default.Register<ValueChangedMessage<List<Medicine>>>(this, (recipient, message) => //Retrieves the new medication to be inspected
                {
					Medicines.Clear();
					foreach (Medicine medicine in message.Value)
					{
						Medicines.Add(medicine);
					}
				});
			}

			//Register for the message only if not already registered
			if (!WeakReferenceMessenger.Default.IsRegistered<ValueChangedMessage<string>>(this))
			{
				WeakReferenceMessenger.Default.Register<ValueChangedMessage<string>>(this, (recipient, message) =>
				{
					if (message.Value == "RefreshPage") //Om messaget är att refresha sidan, it calls on the method to fill in/clear all fields
					{
						IsSearchVisible = false;
						FillFields();
					}
				});
			}
		}

		private void FillFields() //Method for populating and clearing specific variables
        {
			SearchText = string.Empty;
			IsSearchVisible = false;
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
