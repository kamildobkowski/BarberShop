namespace BarberShop.Application.Dto.Appointments;

public record SlotDto
{
	public int Id { get; init; }
	public DateTime TimeSlot { get; init; }
	public int? AppointmentId { get; init; }
}