using BarberShop.Domain.Common;
using BarberShop.Domain.Entites.Appointments;
using BarberShop.Domain.Entites.Users;
using BarberShop.Domain.ValueObjects;

namespace BarberShop.Domain.Entites;

public class Shop : BaseEntity
{
	public string Name { get; set; } = default!;
	public virtual List<BarberService> Services { get; set; } = default!;
	public virtual List<Review> Reviews { get; set; } = default!;
	public Address Address { get; set; } = default!;
	public virtual List<Appointment> Appointments { get; set; } = default!;
	public virtual ShopAdmin? ShopAdmin { get; set; } = default!;
}