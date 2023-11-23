using BarberShop.Domain.Common;

namespace BarberShop.Domain.Entites;

public class BarberService : BaseEntity
{
	public string Name { get; set; } = default!;
	public string? Description { get; set; }
	public decimal? Price { get; set; }
	public TimeSpan? Duration { get; set; }
	public int ShopId { get; set; }
	public Shop Shop { get; set; } = default!;
}