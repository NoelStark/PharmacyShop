using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.Models
{
	public class PaymentInfo
	{
		public Person Person { get; set; } = new Person();
		public string CreditCardName { get; set; } = string.Empty;
		public string CreditCardNumber { get; set; } = string.Empty;
		public string CreditCardType { get; set; } = string.Empty;
		public string ExpireDate { get; set; } = string.Empty;
		public string SecurityCode { get; set; } = string.Empty;

	}
}
