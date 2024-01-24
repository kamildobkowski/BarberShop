using BarberShop.Application.Dto.BarberServices;

namespace BarberShop.Application.Dto.Appointments;

public sealed record AppointmentDto
{
	public int Id { get; init; }
	public DateTime StartTime { get; init; }
	public BarberServiceDto BarberServiceDto { get; init; } = null!;
	public int CustomerUserId { get; init; }
	public int ShopId { get; init; }
}
