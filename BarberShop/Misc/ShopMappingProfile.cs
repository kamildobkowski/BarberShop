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
		CreateMap<Shop, GetShopDto>()
			.ForMember(c => c.City, r => r.MapFrom(s => s.Address.City))
			.ForMember(c => c.Street, r => r.MapFrom(s => s.Address.Street))
			.ForMember(c => c.Number, r => r.MapFrom(s => s.Address.Number))
			.ForMember(c => c.ApartamentNumber, r => r.MapFrom(s => s.Address.ApartamentNumber))
			.ForMember(c => c.PostalCode, r => r.MapFrom(s => s.Address.PostalCode))
			.ForMember(c => c.Latitude, s => s.MapFrom(r => r.Address.Latitude))
			.ForMember(c => c.Longitude, r => r.MapFrom(s => s.Address.Longitude));
		CreateMap<CreateServiceDto, Service>();
		CreateMap<Service, GetServiceDto>();
		CreateMap<Review, GetReviewDto>();
		CreateMap<CreateReviewDto, Review>();
	}
}