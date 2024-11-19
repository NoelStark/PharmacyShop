using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.Models
{
	public class PaymentInfo
	{
		public Person Person { get; set; }
		public required string CreditCardName { get; set; }
		public required string CreditCardNumber { get; set; }
		public required string CreditCardType { get; set; }
		public required string ExpireDate { get; set; }
		public required string SecurityCode { get; set; }

	}
}
