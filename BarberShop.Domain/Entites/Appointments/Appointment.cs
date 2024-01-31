using System.ComponentModel.DataAnnotations.Schema;
using BarberShop.Domain.Common;
using BarberShop.Domain.Entites.Users;
using BarberShop.Domain.ValueObjects;

namespace BarberShop.Domain.Entites.Appointments;

public class Appointment : BaseEntity
{
	public DateTime StartTime { get; set; }
	public int ServiceId { get; set; }
	public virtual BarberService Service { get; set; } = default!;
	public int CustomerUserId { get; set; }
	public virtual Customer Customer { get; set; } = default!;
	public int ShopId { get; set; }
	public virtual Shop Shop { get; set; } = default!;
	public AppointmentStatus Status { get; set; } = AppointmentStatus.Created;
}