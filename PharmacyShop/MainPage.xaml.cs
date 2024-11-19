using PharmacyShop.Models;
using System.Collections.ObjectModel;

namespace PharmacyShop
{
	public partial class MainPage : ContentPage
	{


		public MainPage()
		{
			InitializeComponent();
			
		}

		private void OnCounterClicked(object sender, EventArgs e)
		{

			SemanticScreenReader.Announce(CounterBtn.Text);
		}
	}

}
