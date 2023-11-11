using BarberShop.Domain.Common;
using BarberShop.Domain.Entites.Appointments;
using BarberShop.Domain.ValueObjects;

namespace BarberShop.Domain.Entites;

public class Shop : BaseEntity
{
	public string Name { get; set; } = default!;
	public List<BarberService> Services { get; set; } = default!;
	public List<Review> Reviews { get; set; } = default!;
	public Address Address { get; set; } = default!;
	public List<Appointment> Appointments { get; set; } = default!;
}