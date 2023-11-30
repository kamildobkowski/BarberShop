namespace BarberShop.Application.Dto.Appointments;

public record CreateSlotDto(DateOnly Date, TimeOnly StartTime, TimeOnly EndTime);