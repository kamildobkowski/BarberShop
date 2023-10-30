using System.ComponentModel.DataAnnotations;

namespace BarberShop.Shops.Entities;

public class Service
{
	public int Id { get; set; }
	[Required]
	public string Name { get; set; }
	[Required]
	public decimal Price { get; set; }
	public TimeSpan Duration { get; set; }
	public int ShopId { get; set; }
	public virtual Shop Shop { get; set; }
	
}