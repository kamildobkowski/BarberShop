namespace BarberShop.Application.Dto.Appointments;

public record CreateAppointmentDto (DateTime StartDate, int ServiceId);