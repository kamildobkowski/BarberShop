using System.Security.Claims;
using BarberShop.Domain.Entites.Users;
using Microsoft.AspNetCore.Http;

namespace BarberShop.Infrastructure.Authorization.Jwt;

public class UserContextService : IUserContextService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public UserContextService(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
	public Role? UserRole
	{
		get
		{
			if (User is null)
				return null;
			var claim = User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.Role)?.Value;
			return !int.TryParse(claim, out var roleId) ? null : Role.FromValue(roleId);
		}
	}

	public int? UserId
	{
		get
		{
			if (User is null)
				return null;
			var claim = User.Claims.FirstOrDefault(r => r.Type == ClaimTypes.NameIdentifier)?.Value;
			return int.TryParse(claim, out var userId) ? userId : null;
		}
	}

	public int? ShopId
	{
		get
		{
			if (UserRole == null || !UserRole.Equals(Role.ShopAdmin))
				return null;
			var claim = User!.Claims.FirstOrDefault(r => r.Type == "ShopIdentifier")?.Value;
			return int.TryParse(claim, out var shopId) ? shopId : null;
		}
	}

	public string? UserFullName
			=> User?.Claims.FirstOrDefault(r => r.Type == ClaimTypes.Name)?.Value;

	public string? Email
		=> User?.Claims.FirstOrDefault(r => r.Type == ClaimTypes.Email)?.Value;
}