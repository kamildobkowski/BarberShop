using AutoMapper;
using BarberShop.Application.Dto;
using BarberShop.Application.Dto.BarberServices;
using BarberShop.Application.Dto.Reviews;
using BarberShop.Application.Dto.Shops;
using BarberShop.Domain.Entites;
using BarberShop.Domain.ValueObjects;

namespace BarberShop.Application.Profiles;

public class ShopMappingProfile : Profile
{
	public ShopMappingProfile()
	{
		CreateMap<CreateShopDto, Shop>()
			.ForMember(c => c.Address, r => r.MapFrom(s => new Address
			{
				Street = s.Street,
				Number = s.Number,
				ApartamentNumber = s.ApartamentNumber,
				City = s.City,
				PostalCode = s.PostalCode
			}));
		
		CreateMap<Shop, ShopDto>()
			.ForMember(c => c.City, r => r.MapFrom(s => s.Address.City))
			.ForMember(c => c.Number, r => r.MapFrom(s => s.Address.Number))
			.ForMember(c => c.Street, r => r.MapFrom(s => s.Address.Street))
			.ForMember(c => c.ApartamentNumber, r => r.MapFrom(s => s.Address.ApartamentNumber))
			.ForMember(c => c.PostalCode, r => r.MapFrom(s => s.Address.PostalCode))
			.ForMember(c => c.Latitude, r => r.MapFrom(s => s.Address.Latitude))
			.ForMember(c => c.Longitude, r => r.MapFrom(s => s.Address.Longitude));
		CreateMap<UpdateShopDto, Shop>()
			.ForAllMembers(r=>r.Condition((src, dest, srcValue) => srcValue != null));
		CreateMap<UpdateShopAddressDto, Address>()
			.ForAllMembers(r=>r.Condition((src, dest, val)=>val!=null));
	}
}