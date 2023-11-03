using BarberShop.Application.Profiles;
using BarberShop.Application.Services.Shops.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Application;

public static class DependencyInjection
{
	public static void AddApplication(this IServiceCollection services)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateShopCommand).Assembly));
		services.AddAutoMapper(typeof(ShopMappingProfile));
	}
}