namespace BarberShop.Application.Dto.Slots;

public record CreateSlotDto(DateOnly Date, TimeOnly StartTime, TimeOnly EndTime);