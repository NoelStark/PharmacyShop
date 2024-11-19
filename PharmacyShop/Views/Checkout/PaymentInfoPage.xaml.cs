using PharmacyShop.ViewModels;
using PharmacyShop.ViewModels.Checkout.PaymentInfoViewModels;

namespace PharmacyShop.Views.Checkout
{
    public partial class PaymentInfoPage : ContentPage
    {
        public PaymentInfoPage(PaymentInfoViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (BindingContext is PaymentInfoViewModel viewModel)
			{
				viewModel.Reinitialize();
			}
		}
	}
}
