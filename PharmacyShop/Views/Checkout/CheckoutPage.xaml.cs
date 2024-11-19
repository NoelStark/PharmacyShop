using PharmacyShop.ViewModels;
using PharmacyShop.ViewModels.Checkout.CheckoutViewModels;

namespace PharmacyShop.Views.Checkout;

public partial class CheckoutPage : ContentPage
{
	public CheckoutPage(CheckoutViewModel checkoutViewModel)
	{
		InitializeComponent();
		BindingContext = checkoutViewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if(BindingContext is CheckoutViewModel viewModel)
		{
			viewModel.Reinitialize();
		}
	}
}