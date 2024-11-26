using PharmacyShop.ViewModels;
using PharmacyShop.ViewModels.Checkout.PersonalInformation;

namespace PharmacyShop.Views.Checkout;

public partial class PersonalInfoPage : ContentPage
{
	public PersonalInfoPage(PersonalInfoViewModel personalInfoViewModel)
	{
		InitializeComponent();
		BindingContext = personalInfoViewModel;
		if(BindingContext is PersonalInfoViewModel viewModel)
		{
			viewModel.PropertyChanged += Field_PropertyChanged;
		}

	}
	private void Field_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		Entry? entry = null;
		if (e.PropertyName == nameof(PersonalInfoViewModel.PostalCode))
			entry = PostalCode;
		else if (e.PropertyName == nameof(PersonalInfoViewModel.Phone))
			entry = Phone;
        else if (e.PropertyName == nameof(PersonalInfoViewModel.Street))
            entry = StreetEntry;

        if (entry != null)
		{
			Dispatcher.Dispatch(() =>
			{
				entry.CursorPosition = entry.Text.Length;
			});
		}
		
	}
}