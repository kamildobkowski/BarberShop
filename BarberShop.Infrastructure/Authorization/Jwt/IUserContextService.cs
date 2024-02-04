using System.Security.Claims;
using BarberShop.Domain.Entites.Users;

namespace BarberShop.Infrastructure.Authorization.Jwt;

public interface IUserContextService
{
	ClaimsPrincipal? User { get; }
	public Role? UserRole { get; }
	public int? UserId { get; }
	public int? ShopId { get; }
	public string? UserFullName { get; }
	public string? Email { get; }
}