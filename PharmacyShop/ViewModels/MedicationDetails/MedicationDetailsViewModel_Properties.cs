using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using PharmacyShop.Models;
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
		//ObservableCollections collections
		public ObservableCollection<string> Options { get; set; } = new();
		public ObservableCollection<Medicine> Medicines { get; set; } = new();

		//Observableproperties variablers
		[ObservableProperty]
		private int quantity = 0;

		[ObservableProperty]
		private bool isSearchVisible = false;

		[ObservableProperty]
		private string title = string.Empty;

		[ObservableProperty]
		private string substance = string.Empty;

		[ObservableProperty]
		private string dosage = string.Empty;

		[ObservableProperty]
		private string usage = string.Empty;

		[ObservableProperty]
		private string amount = string.Empty;

		[ObservableProperty]
		private Medicine currentMedicine = new Medicine();

		[ObservableProperty]
		public string searchText = string.Empty;

		partial void OnSearchTextChanged(string value) //Onpropertychangemetod that takes the searchtext as a parameter
		{
			WeakReferenceMessenger.Default.Send(new ValueChangedMessage<string>(value));
			Medicines = _medicationOverview.Medicine;
			OnPropertyChanged(nameof(Medicines));
			IsSearchVisible = true;
		}
	}
}
