namespace BarberShop.Application.Dto.Appointments;

public record RemoveSlotDto(DateOnly Date, TimeOnly StartTime, TimeOnly EndTime);