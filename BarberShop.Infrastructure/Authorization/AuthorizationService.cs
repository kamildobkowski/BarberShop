using BarberShop.Application.Interfaces;
using BarberShop.Domain.Common;
using BarberShop.Domain.Entites.Users;

namespace BarberShop.Infrastructure.Authorization;

public class AuthorizationService : IAuthorizationService
{
	private readonly IUserContextService _userContextService;

	public AuthorizationService(IUserContextService userContextService)
	{
		_userContextService = userContextService;
	}

	public bool AuthorizeCreatedBy(BaseEntity entity)
		=> _userContextService.UserRole.Equals(Role.Admin)
		   || entity.CreatedById == _userContextService.UserId
			? true
			: throw new UnauthorizedAccessException();

	public bool AuthorizeShopAdmin(int? shopId)
		=>  (_userContextService.UserRole.Equals(Role.Admin)) 
		   || (_userContextService.UserRole.Equals(Role.ShopAdmin) 
		   && (shopId == null || _userContextService.ShopId == shopId))
			? true
			: throw new UnauthorizedAccessException();

	public bool AuthorizeAppointment(int? customerUserId, int? shopId)
		=> _userContextService.UserRole.Equals(Role.Admin)
		   || (_userContextService.UserRole.Equals(Role.ShopAdmin)
		   && (shopId is null || _userContextService.ShopId == shopId))
		   || (_userContextService.UserRole.Equals(Role.Customer)
		   && (customerUserId is null || _userContextService.UserId == customerUserId))
			? true
			: throw new UnauthorizedAccessException();

	public bool AuthorizeCustomer(int? customerId)
		=> _userContextService.UserRole.Equals(Role.Customer)
		   && (customerId is null || _userContextService.UserId == customerId);

	public bool AuthorizeAdmin()
		=> _userContextService.UserRole.Equals(Role.Admin)
			? true
			: throw new UnauthorizedAccessException();

	public bool AuthorizeShopAdminWithoutShopAssigned()
		=> _userContextService.UserRole.Equals(Role.ShopAdmin)
		   && _userContextService.ShopId is null;
}