using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.Models
{
	public class Medicine
	{
		public string Name { get; set; }
		public string Dose { get; set; }
		public string Substance { get; set; }
		public string Description { get; set; }
		public string Useage { get; set; }
		public string Amount { get; set; }
		public ItemInformation Information {  get; set; }
    }
}
