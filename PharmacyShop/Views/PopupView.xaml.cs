using CommunityToolkit.Maui.Views;
using PharmacyShop.ViewModels;
using PharmacyShop.ViewModels.Checkout.CheckoutViewModels;
using PharmacyShop.ViewModels.MedicationDetails;
using PharmacyShop.ViewModels.MedicationOverview;

namespace PharmacyShop.Views
{
	public partial class PopupView : Popup
	{
		private readonly PopupViewModel _viewModel;
		public PopupView(object requestingModel, bool empty = false)
		{
			InitializeComponent();
			if (requestingModel is CheckoutViewModel checkoutViewModel2 && empty == true)
			{
				_viewModel = new PopupViewModel(checkoutViewModel2, empty);
			}
			else if (requestingModel is MedicationDetailsViewModel detailsViewModel)
            {
                _viewModel = new PopupViewModel(detailsViewModel);
            }
            else if (requestingModel is MedicationOverviewPageViewModel overviewViewModel)
            {
                _viewModel = new PopupViewModel(overviewViewModel);
            }
            else if (requestingModel is CheckoutViewModel checkoutViewModel)
            {
                _viewModel = new PopupViewModel(checkoutViewModel);
			}
		
			else
            {
                throw new ArgumentException("Unsupported view model type", nameof(requestingModel));
            }

            BindingContext = _viewModel;

            // Subscribe to PropertyChanged
            _viewModel.PropertyChanged += ClosePopup;
		}
		
		private void ClosePopup(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if(e.PropertyName == nameof(PopupViewModel.ShouldClose) && _viewModel.ShouldClose)
			{
				Close();
			}
		}

		protected override Task OnClosed(object? result, bool wasDismissedByTappingOutsideOfPopup, CancellationToken token = default)
		{
			_viewModel.IsClosed = true;
			return base.OnClosed(result, wasDismissedByTappingOutsideOfPopup, token);
		}


	}

}

