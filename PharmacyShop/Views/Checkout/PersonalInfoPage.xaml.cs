using PharmacyShop.ViewModels;

namespace PharmacyShop.Views.Checkout;

public partial class PersonalInfoPage : ContentPage
{
	public PersonalInfoPage(PersonalInfoViewModel personalInfoViewModel)
	{
		InitializeComponent();
		BindingContext = personalInfoViewModel;

	}
}