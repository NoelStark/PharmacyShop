using PharmacyShop.ViewModels;
using PharmacyShop.ViewModels.Checkout.CheckoutViewModels;
using PharmacyShop.ViewModels.Checkout.ConfirmationViewModels;

namespace PharmacyShop.Views.Checkout;

public partial class ConfirmationPage : ContentPage
{
	public ConfirmationPage(ConfirmationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is ConfirmationViewModel viewModel)
		{
			viewModel.Reinitialize();
		}
	}
}