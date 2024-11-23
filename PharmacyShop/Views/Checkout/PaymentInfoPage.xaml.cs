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
				viewModel.PropertyChanged += Field_PropertyChanged;
			}
		}

		private void Field_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			Entry? entry = null;
			if(e.PropertyName == nameof(PaymentInfoViewModel.ExpireDate))
				entry = ExpireDateEntry;
			else if (e.PropertyName == nameof(PaymentInfoViewModel.CreditCardNumber))
				entry = CreditCardNumber;
			if(entry != null){

				Dispatcher.Dispatch(() =>
				{
					entry.CursorPosition = entry.Text.Length;
					
				});
			}
		}

	
	}
}
