using System.Security.Claims;
using BarberShop.Application.Interfaces;
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
	public Role UserRole
	{
		get
		{
			if (User is null)
				throw new UnauthorizedAccessException();
			var claim = User.Claims.FirstOrDefault(r => r.Type == Claims.Role)?.Value;
			return int.TryParse(claim, out var roleId) ? Role.FromValue(roleId)! : throw new UnauthorizedAccessException();
		}
	}

	public int UserId
	{
		get
		{
			if (User is null)
				throw new UnauthorizedAccessException();
			var claim = User.Claims.FirstOrDefault(r => r.Type == Claims.Id)?.Value;
			return int.TryParse(claim, out var userId) ? userId : throw new UnauthorizedAccessException();
		}
	}

	public int? ShopId
	{
		get
		{
			if (!UserRole.Equals(Role.ShopAdmin))
				return null;
			var claim = User!.Claims.FirstOrDefault(r => r.Type == Claims.ShopId)?.Value;
			return int.TryParse(claim, out var shopId) ? shopId : null;
		}
	}

	public string UserFullName
			=> User?.Claims.FirstOrDefault(r => r.Type == Claims.FullName)?.Value!;

	public string Email
		=> User?.Claims.FirstOrDefault(r => r.Type == Claims.Email)?.Value!;
}