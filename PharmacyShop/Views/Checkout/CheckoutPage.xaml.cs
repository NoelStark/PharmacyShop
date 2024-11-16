using PharmacyShop.ViewModels;

namespace PharmacyShop.Views.Checkout;

public partial class CheckoutPage : ContentPage
{
	public CheckoutPage(CheckoutViewModel checkoutViewModel)
	{
		InitializeComponent();
		BindingContext = checkoutViewModel;
	}
}