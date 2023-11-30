using BarberShop.Domain.Common;

namespace BarberShop.Domain.Entites.Appointments;

public class Slot : BaseEntity
{
	public int ShopId { get; set; }
	public virtual Shop Shop { get; set; } = default!;
	public DateTime TimeSlot { get; set; }
	public int? AppointmentId { get; set; }
	public virtual Appointment? Appointment { get; set; }

	public static List<Slot> CreateEmptySlots(DateOnly date, TimeOnly start, TimeOnly end, int shopId)
	{
		var slots = new List<Slot>();
		while (end - start >= TimeSpan.FromMinutes(15))
		{
			slots.Add(new Slot
			{
				ShopId = shopId,
				TimeSlot = date.ToDateTime(start),
				AppointmentId = null
			});
			start = start.Add(TimeSpan.FromMinutes(15));
		}
		return slots;
	}
}