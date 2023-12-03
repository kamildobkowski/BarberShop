using AutoMapper;
using BarberShop.Application.Dto.Appointments;
using BarberShop.Domain.Entites.Appointments;

namespace BarberShop.Application.Profiles;

public class SlotMappingProfile : Profile
{
	public SlotMappingProfile()
	{
		CreateMap<Slot, SlotDto>();
	}
}