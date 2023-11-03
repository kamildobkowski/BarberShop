using BarberShop.Domain.Common;

namespace BarberShop.Domain.Entites;

public class Review : BaseEntity
{
	public string? Description { get; set; }
	public int Rating { get; set; }
	public int ShopId { get; set; }
	public virtual Shop Shop { get; set; }
}