using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.Models
{
	public class Person
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Email { get; set; }
		public required string Phone { get; set; }
		public required string City { get; set; }
		public required string Street { get; set; }
		public required string PostalCode { get; set; }

		public string FullName => FirstName + " " + LastName;
    }
}
