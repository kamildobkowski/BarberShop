using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Infrastructure.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
	private readonly IServiceScopeFactory _serviceScopeFactory;

	public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
	{
		_serviceScopeFactory = serviceScopeFactory;
	}

	protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, 
		PermissionRequirement requirement)
	{
		var userIdClaim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
		if (!int.TryParse(userIdClaim, out var userId))
		{
			return;
		}
		using var scope = _serviceScopeFactory.CreateScope();
		var permissionService = scope
			.ServiceProvider
			.GetRequiredService<IPermissionService>();
		var permissions = await permissionService.GetPermissionsAsync(userId);
		if (permissions.Contains(requirement.Permission))
		{
			context.Succeed(requirement);
		}
	}
}