using PharmacyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.Services
{
	public class PersonService
	{
		public Person CurrentPerson { get; set; } = new Person();
		public PaymentInfo? PaymentInfo { get; set; }
		public List<Cart> ItemsCart { get; set; } = new();
		public decimal TotalCartCost { get; set; }
		public decimal ShippingCost { get; set; }
	}
}
