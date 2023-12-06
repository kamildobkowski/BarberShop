using AutoMapper;
using BarberShop.Application.Dto.Appointments;
using BarberShop.Domain.Entites.Appointments;

namespace BarberShop.Application.Profiles;

public class AppointmentMappingProfile : Profile
{
	public AppointmentMappingProfile()
	{
		CreateMap<CreateAppointmentDto, Appointment>();
	}
}