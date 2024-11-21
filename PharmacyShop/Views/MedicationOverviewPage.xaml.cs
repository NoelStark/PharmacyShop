using PharmacyShop.ViewModels;
using PharmacyShop.ViewModels.MedicationDetails;
using PharmacyShop.ViewModels.MedicationOverview;

namespace PharmacyShop.Views;

public partial class MedicationOverviewPage : ContentPage
{
	public MedicationOverviewPage(MedicationOverviewPageViewModel medicinViewModel)
	{
		InitializeComponent();
		BindingContext = medicinViewModel;
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();
		if(BindingContext is MedicationOverviewPageViewModel viewModel)
		{
			_= viewModel.Initialize();
		}
	}
}