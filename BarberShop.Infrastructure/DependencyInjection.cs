using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Infrastructure.ExternalServices;
using BarberShop.Infrastructure.Persistence;
using BarberShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Infrastructure;

public static class DependencyInjection
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<BarberShopDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Docker")));
		services.AddScoped<IShopRepository, ShopRepository>();
		services.AddSingleton<ILocationService, LocationService>();
	}
}