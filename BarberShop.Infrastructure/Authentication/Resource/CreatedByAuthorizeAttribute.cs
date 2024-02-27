using BarberShop.Application.Interfaces;
using BarberShop.Domain.Common;
using BarberShop.Domain.Entites.Users;
using BarberShop.Infrastructure.Authorization.Jwt;
using BarberShop.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Infrastructure.Authorization.Resource;

public class CreatedByAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
	private readonly Type _type;

	public CreatedByAuthorizeAttribute(Type type)
	{
		_type = type;
	}
	public void OnAuthorization(AuthorizationFilterContext context)
	{
		var dbContext = context.HttpContext.RequestServices.GetRequiredService<BarberShopDbContext>();
		var userContext = context.HttpContext.RequestServices.GetRequiredService<IUserContextService>();
		if (userContext.UserRole == null)
		{
			context.Result = new UnauthorizedResult();
			return;
		}
		if (userContext.UserRole.Equals(Role.Admin))
		{
			context.Result = new OkResult();
			return;
		}
		if (!context.RouteData.Values.TryGetValue("id", out var idObj)
		    || !int.TryParse(idObj?.ToString(), out var id))
		{
			context.Result = new UnauthorizedResult();
			return;
		}

		var dbSet = 
			dbContext
				.GetType()
				.GetMethods()
				.FirstOrDefault(r => r is { IsGenericMethod: true, Name: "Set" })?
				.MakeGenericMethod(_type)
				.Invoke(dbContext, null) as IQueryable<BaseEntity>;
		var entity = dbSet?.Where(x => x.Id == id).FirstOrDefault();
		if (entity is null)
		{
			context.Result = new BadRequestResult();
			return;
		}

		if (entity.CreatedById != userContext.UserId)
		{
			context.Result = new ForbidResult();
			return;
		}

		context.Result = new OkResult();

	}
}