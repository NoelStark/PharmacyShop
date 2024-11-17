using PharmacyShop.ViewModels;

namespace PharmacyShop.Views.Checkout;

public partial class PersonalInfoPage : ContentPage
{
	public PersonalInfoPage(PersonalInfoViewModel personalInfoViewModel)
	{
		InitializeComponent();
		BindingContext = personalInfoViewModel;
		if(BindingContext is PersonalInfoViewModel viewModel)
		{
			viewModel.PropertyChanged += PostalCode_PropertyChanged;
		}

	}

	private void PostalCode_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		if (e.PropertyName == nameof(PersonalInfoViewModel.PostalCode))
		{
			Dispatcher.Dispatch(() =>
			{
				var entry = PostalCode;
				if (entry != null && entry.Text.Length == 4 && entry.Text.Contains(" "))
				{
					entry.CursorPosition = 4;
				}
			});
			
		}
		else if(e.PropertyName == nameof(PersonalInfoViewModel.Phone))
		{
			Dispatcher.Dispatch(() =>
			{
				var entry = Phone;
				int[] positions = { 4, 8, 11 };
				if (entry != null && positions.Contains(entry.Text.Length) && entry.Text.Contains(" "))
				{
					entry.CursorPosition = entry.Text.Length;
				}
			});
		}
	}
}