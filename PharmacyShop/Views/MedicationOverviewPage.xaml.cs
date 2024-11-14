using PharmacyShop.ViewModels;

namespace PharmacyShop.Views;

public partial class MedicationOverviewPage : ContentPage
{
	public MedicationOverviewPage(MedicationOverviewPageViewModel medicinViewModel)
	{
		InitializeComponent();
		BindingContext = medicinViewModel;
	}
}