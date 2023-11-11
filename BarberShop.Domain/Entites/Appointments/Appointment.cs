using System.ComponentModel.DataAnnotations.Schema;
using BarberShop.Domain.Common;
using BarberShop.Domain.Entites.Users;

namespace BarberShop.Domain.Entites.Appointments;

public class Appointment : BaseEntity
{
	public DateTime StartTime { get; set; }
	public int ServiceId { get; set; }
	public virtual BarberService Service { get; set; } = default!;
	public int UserId { get; set; }
	public virtual Customer Customer { get; set; } = default!;
	public int? ShopId { get; set; }
	public Shop? Shop { get; set; }
}