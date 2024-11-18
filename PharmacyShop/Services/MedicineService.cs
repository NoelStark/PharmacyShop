using PharmacyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.Services
{
	public class MedicineService
	{
		public List<Cart> GetItems()
		{
			return new List<Cart> {

				new Cart
				{
					Medicine = new Medicine { Name = "Alvedon", Description = "500mg" },
					Quantity = 1,
					ItemPrice = 32,
					TotalItemsPrice = 32
				},
				new Cart
				{
					Medicine = new Medicine { Name = "Ipren", Description = "200mg" },
					Quantity = 1,
					ItemPrice = 45,
					TotalItemsPrice = 45
				},
				new Cart
				{
					Medicine = new Medicine { Name = "Lalala", Description = "100ml" },
					Quantity = 1,
					ItemPrice = 63,
					TotalItemsPrice = 63
				},
				new Cart
				{
					Medicine = new Medicine { Name = "Lalala", Description = "100ml" },
					Quantity = 1,
					ItemPrice = 63,
					TotalItemsPrice = 63
				}
			};
		
		}
	}
}
