using CommunityToolkit.Maui.Views;
using PharmacyShop.ViewModels;

namespace PharmacyShop.Views
{
	public partial class PopupView : Popup
	{
		private readonly PopupViewModel _viewModel;
		public PopupView()
		{
			InitializeComponent();
			PopupViewModel viewModel = new PopupViewModel();
			_viewModel = viewModel;
			BindingContext = _viewModel;
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

