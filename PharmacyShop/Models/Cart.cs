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
        public Medicine? Medicine { get; set; }
        [ObservableProperty]
        public int quantity = 0;
        public ItemInformation? Information { get; set; }
        [ObservableProperty]
        public decimal totalItemsPrice = 0;
    }
}
