using PharmacyShop.ViewModels;

namespace PharmacyShop.Views;

public partial class MedicationDetailsPage : ContentPage
{
	public MedicationDetailsPage(MedicationDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private void Picker_SelectedIndexChanged(object sender, EventArgs e)
	{
		var picker = (Picker)sender;
		picker.SelectedIndex = -1;
	}
}