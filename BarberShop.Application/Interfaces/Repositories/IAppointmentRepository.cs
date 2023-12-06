using BarberShop.Domain.Entites.Appointments;

namespace BarberShop.Application.Interfaces.Repositories;

public interface IAppointmentRepository : IGenericRepository<Appointment>, IPagination<Appointment>
{
	
}