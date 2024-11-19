using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels
{
	public partial class PopupViewModel :ObservableObject
	{
		[ObservableProperty]
		private bool isClosed = false;

		[ObservableProperty]
		private bool shouldClose = false;
		private async void ClosePopup()
		{

			await Task.Delay(2000);
			if (!IsClosed)
			{
				ShouldClose = true;
			}

		}

        public PopupViewModel()
        {
			ClosePopup();

		}

    }
}
