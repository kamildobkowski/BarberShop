using BarberShop.Shared.Exceptions;

namespace BarberShop.Shared.Middleware;

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
			context.Response.StatusCode = 404;
			await context.Response.WriteAsync(e.Message);
		}
		catch (Exception e)
		{
			context.Response.StatusCode = 500;
			await context.Response.WriteAsync(e.Message);
		}
	}
}