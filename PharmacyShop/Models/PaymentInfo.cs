using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.Models
{
	public class PaymentInfo
	{
		public required Person Person { get; set; }
		public required string CreditCardName { get; set; }
		public required long CreditCardNumber { get; set; }
		public required DateTime ExpireDate { get; set; }
		public required int SecurityCode { get; set; }

	}
}
