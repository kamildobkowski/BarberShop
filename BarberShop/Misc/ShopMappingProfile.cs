using AutoMapper;
using BarberShop.Entities.BarberShop;
using BarberShop.Models.Dto;

namespace BarberShop.Misc;

public class ShopMappingProfile : Profile
{
	public ShopMappingProfile()
	{
		CreateMap<CreateShopDto, Shop>()
			.ForMember(r => r.Address, c => c.MapFrom(dto => new Address()
			{
				City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street, Number = dto.Number,
				ApartamentNumber = dto.ApartamentNumber
			}));
	}
}