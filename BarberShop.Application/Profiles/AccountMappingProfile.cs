using AutoMapper;
using BarberShop.Application.Dto.Account;
using BarberShop.Domain.Entites.Users;

namespace BarberShop.Application.Profiles;

public class AccountMappingProfile : Profile
{
	public AccountMappingProfile()
	{
		CreateMap<CreateCustomerDto, User>()
			.ForMember(r => r.RoleId, c => c.MapFrom(s => 2))
			.ForMember(r => r.Customer, c => c.MapFrom(s => new Customer
			{
				PhoneNumber = s.PhoneNumber
			}));
		CreateMap<CreateShopAdminDto, User>()
			.ForMember(r => r.RoleId, c => c.MapFrom(s => 3));
		CreateMap<CreateAdminDto, User>()
			.ForMember(r => r.RoleId, c => c.MapFrom(s => 1));

	}
}