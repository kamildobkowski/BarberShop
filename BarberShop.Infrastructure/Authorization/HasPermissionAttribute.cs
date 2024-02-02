using BarberShop.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;

namespace BarberShop.Infrastructure.Authorization;

public class HasPermissionAttribute : AuthorizeAttribute
{
	public HasPermissionAttribute(EnPermission permission) 
		: base(policy: permission.ToString())
	{
		
	}
}