using PharmacyShop.ViewModels;

namespace PharmacyShop.Views;

public partial class CheckoutPage : ContentPage
{
	public CheckoutPage(CheckoutViewModel checkoutViewModel)
	{
		InitializeComponent();
		BindingContext = checkoutViewModel;
	}
}