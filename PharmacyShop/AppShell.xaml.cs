using PharmacyShop.Views;
using PharmacyShop.Views.Checkout;

namespace PharmacyShop
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute("MedicationDetailsPage", typeof(MedicationDetailsPage));
			Routing.RegisterRoute("PersonalInfoPage", typeof(PersonalInfoPage));
			Routing.RegisterRoute("PaymentInfoPage", typeof(PaymentInfoPage));
			Routing.RegisterRoute("ConfirmationPage", typeof(ConfirmationPage));
		}
	}
}
