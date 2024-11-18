using PharmacyShop.ViewModels;

namespace PharmacyShop.Views;

public partial class MedicationDetailsPage : ContentPage
{
	public MedicationDetailsPage(MedicationDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is MedicationDetailsViewModel viewModel)
		{
			viewModel.Reinitialize();
		}
	}

	private void Picker_SelectedIndexChanged(object sender, EventArgs e)
	{
		var picker = (Picker)sender;
		picker.SelectedIndex = -1;
	}

	private void OnPicker(object sender, FocusEventArgs e) 
	{
		DescriptionPicker.Unfocus();
	}

	private void OnTap(object sender, EventArgs e)
	{
		if (BindingContext is MedicationDetailsViewModel viewModel)
		{
			viewModel.IsSearchVisible = false;
			SearchBarControl.Unfocus();
		}
	}
}