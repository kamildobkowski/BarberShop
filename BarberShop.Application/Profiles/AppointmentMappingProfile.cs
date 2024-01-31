using AutoMapper;
using BarberShop.Application.Dto.Appointments;
using BarberShop.Application.Dto.BarberServices;
using BarberShop.Domain.Entites.Appointments;

namespace BarberShop.Application.Profiles;

public sealed class AppointmentMappingProfile : Profile
{
	public AppointmentMappingProfile()
	{
		CreateMap<Appointment, AppointmentDto>()
			.ForMember(r => r.Id, c => c.MapFrom(s => s.Id))
			.ForMember(r => r.ShopId, c => c.MapFrom(s => s.ShopId))
			.ForMember(r => r.StartTime, c => c.MapFrom(s => s.StartTime))
			.ForMember(r => r.CustomerUserId, c => c.MapFrom(s => s.CustomerUserId))
			.ForMember(r => r.BarberServiceDto, c => c.MapFrom(s => new BarberServiceDto
			{
				Id = s.Service.Id,
				Name = s.Service.Name,
				Description = s.Service.Description,
				Price = s.Service.Price,
				Duration = s.Service.Duration
			}))
			.ForMember(r => r.Status, c => c.MapFrom(s => s.Status.ToString()));

	}
}