using System.Security.Claims;
using BarberShop.Domain.Entites.Users;

namespace BarberShop.Application.Interfaces;

public interface IUserContextService
{
	ClaimsPrincipal? User { get; }
	public Role UserRole { get; }
	public int UserId { get; }
	public int? ShopId { get; }
	public string UserFullName { get; }
	public string Email { get; }
}