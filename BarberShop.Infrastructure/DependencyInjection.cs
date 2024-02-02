using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites.Users;
using BarberShop.Infrastructure.Authorization;
using BarberShop.Infrastructure.ExternalServices;
using BarberShop.Infrastructure.Persistence;
using BarberShop.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Infrastructure;

public static class DependencyInjection
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<BarberShopDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Docker")), ServiceLifetime.Scoped);
		services.AddScoped<IShopRepository, ShopRepository>();
		services.AddScoped<IBarberServiceRepository, BarberServiceRepository>();
		services.AddScoped<IReviewRepository, ReviewRepository>();
		services.AddSingleton<ILocationService, LocationService>();
		services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddHttpClient<LocationService>();
		services.AddScoped<ITimeTableRepository, TimeTableRepository>();
		services.AddScoped<IAppointmentRepository, AppointmentRepository>();
		services.AddAuthorization();
		services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
		services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
	}

	public static void Seed(this WebApplication app)
	{
		
	}
}