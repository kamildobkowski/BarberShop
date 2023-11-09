using AutoMapper;
using BarberShop.Application.Dto.BarberServices;
using BarberShop.Domain.Entites;

namespace BarberShop.Application.Profiles;

public class BarberServiceMappingProfile : Profile
{
	public BarberServiceMappingProfile()
	{
		CreateMap<BarberService, BarberServiceDto>();
		CreateMap<CreateBarberServiceDto, BarberService>();
	}
}