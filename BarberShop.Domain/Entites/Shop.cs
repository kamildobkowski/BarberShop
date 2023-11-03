using BarberShop.Domain.Common;

namespace BarberShop.Domain.Entites;

public class Shop : BaseEntity
{
	public string Name { get; set; } = default!;
	public List<BarberService> Services { get; set; } = default!;
	public List<Review> Reviews { get; set; } = default!;
	public int AddressId { get; set; }
	public virtual Address Address { get; set; } = default!;
}