using System.ComponentModel.DataAnnotations;

namespace BarberShop.Entities.BarberShop;

public class Review
{
	public int Id { get; set; }
	public string? Description { get; set; }
	[Required]
	[Range(1,5)]
	public int Stars { get; set; }
	public int ShopId { get; set; }
	public virtual Shop Shop { get; set; }
}