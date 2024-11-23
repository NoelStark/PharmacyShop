using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.Models
{
	public class Medicine
	{
		public string Name { get; set; } = string.Empty;
		public string Dose { get; set; } = string.Empty;
		public string Substance { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Useage { get; set; } = string.Empty;
		public string Amount { get; set; } = string.Empty;
		public int ArticleNumber { get; set; }
		public ItemInformation? Information {  get; set; }
    }
}
