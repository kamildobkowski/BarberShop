using System.Security.Claims;

namespace BarberShop.Infrastructure.Authorization.Jwt;

public static class Claims
{
	public const string Id = ClaimTypes.NameIdentifier;
	public const string FullName = ClaimTypes.Name;
	public const string Role = ClaimTypes.Role;
	public const string Email = ClaimTypes.Email;
	public const string ShopId = "ShopIdentifier";
}