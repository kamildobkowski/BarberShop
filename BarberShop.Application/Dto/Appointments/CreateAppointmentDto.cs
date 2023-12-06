namespace BarberShop.Application.Dto.Appointments;


public record CreateAppointmentDto (int ShopId, DateTime StartDate, int ServiceId);