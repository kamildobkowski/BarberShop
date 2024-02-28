using BarberShop.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace BarberShop.Application.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next.Invoke(context);
		}
		catch (NotFoundException e)
		{
			context.Response.StatusCode = StatusCodes.Status404NotFound;
			await context.Response.WriteAsync(e.Message);
		}
		catch (WrongCredentialsException e)
		{
			context.Response.StatusCode = StatusCodes.Status403Forbidden;
			await context.Response.WriteAsync(e.Message);
		}
		catch (UnauthorizedAccessException e)
		{
			context.Response.StatusCode = StatusCodes.Status403Forbidden;
			await context.Response.WriteAsync(e.Message);
		}
		catch (Exception)
		{
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			await context.Response.WriteAsync("Something went wrong");
		}
	}
}