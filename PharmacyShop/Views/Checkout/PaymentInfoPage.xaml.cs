using PharmacyShop.ViewModels;
using PharmacyShop.ViewModels.Checkout.PaymentInfoViewModels;
using static Microsoft.Maui.ApplicationModel.Permissions;

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
				viewModel.PropertyChanged += CreditCard_PropertyChanged;
			}
		}

		private void CreditCard_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(PaymentInfoViewModel.CreditCardNumber))
			{
				Dispatcher.Dispatch(() =>
				{
					var entry = CreditCardNumber;
					int[] positions = { 6, 11, 16 };
					if (entry != null && positions.Contains(entry.Text.Length) && entry.Text.Contains(" "))
					{
						entry.CursorPosition = entry.Text.Length;
					}
				});

			}
		}
	}
}
