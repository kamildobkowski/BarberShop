using BarberShop.Domain.Common;

namespace BarberShop.Application.Interfaces;

public interface IAuthorizationService
{
	bool AuthorizeCreatedBy(BaseEntity entity);
	bool AuthorizeShopAdmin(int? shopId);
	bool AuthorizeAppointment(int? customerUserId, int? shopId);
	bool AuthorizeCustomer(int? customerId);
	bool AuthorizeAdmin();
	bool AuthorizeShopAdminWithoutShopAssigned();
}