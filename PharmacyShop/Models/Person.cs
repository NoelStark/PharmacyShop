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
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string Street { get; set; } = string.Empty;
		public string PostalCode { get; set; } = string.Empty;

		public string FullName => FirstName + " " + LastName;
    }
}
