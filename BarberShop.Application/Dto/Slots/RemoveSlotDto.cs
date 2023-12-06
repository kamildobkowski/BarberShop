namespace BarberShop.Application.Dto.Slots;

public record RemoveSlotDto(DateOnly Date, TimeOnly StartTime, TimeOnly EndTime);