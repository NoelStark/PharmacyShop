using PharmacyShop.Models;
using System.Collections.ObjectModel;

namespace PharmacyShop
{
	public partial class MainPage : ContentPage
	{
		int count = 0;

		public MainPage()
		{
			InitializeComponent();
			
		}

		private void OnCounterClicked(object sender, EventArgs e)
		{
			Collection<Medicine> test = new Collection<Medicine>();
			Dictionary<string, string> list = new();
			list.Add("Alvedon", "Test");
			list.Add("Abc", "TuggTablett");
			list.Add("Asjklda", "MunsönderfallandeTablett");
			list.Add("Qasjdla", "TuggTablett");
			string key = "Alvedon";
			string word = "Al";

			var foundMedicine = list.Where(x => x.Key.ToLower().StartsWith(word.ToLower())).ToList();

			Console.WriteLine();
			count++;

			if (count == 1)
				CounterBtn.Text = $"Clicked {count} time";
			else
				CounterBtn.Text = $"Clicked {count} times";

			SemanticScreenReader.Announce(CounterBtn.Text);
		}
	}

}
