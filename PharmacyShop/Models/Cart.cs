using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.Models
{
    public partial class Cart : ObservableObject
    {
        public Medicine Medicine { get; set; }
        [ObservableProperty]
        public int quantity;
        public int ItemPrice { get; set; }
		[ObservableProperty]
		public int totalItemsPrice;
    }
}
