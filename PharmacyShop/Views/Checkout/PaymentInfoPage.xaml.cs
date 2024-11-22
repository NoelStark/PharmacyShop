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
				viewModel.PropertyChanged += ExpireDate_PropertyChanged;
			}
		}

		private void ExpireDate_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if(e.PropertyName == nameof(PaymentInfoViewModel.ExpireDate))
			{
				Dispatcher.Dispatch(() =>
				{
					var entry = ExpireDateEntry;
					if (entry != null && (entry.Text.Length == 3 || entry.Text.Length == 4) && entry.Text.Contains("/"))
					{
						entry.CursorPosition = entry.Text.Length;
					}
				});
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
