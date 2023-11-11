using BarberShop.Domain.Common;

namespace BarberShop.Domain.Entites.Appointments;

public class Slot : BaseEntity
{
	public int ShopId { get; set; }
	public DateTime TimeSlot { get; set; }
	public int? AppointmentId { get; set; }
}