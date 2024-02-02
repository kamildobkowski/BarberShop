using System.Text;
using BarberShop.Application.Interfaces;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites.Users;
using BarberShop.Infrastructure.Authorization;
using BarberShop.Infrastructure.Authorization.Jwt;
using BarberShop.Infrastructure.Authorization.Permissions;
using BarberShop.Infrastructure.ExternalServices;
using BarberShop.Infrastructure.Persistence;
using BarberShop.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
		var authenticationSettings = new AuthenticationSettings();
		configuration.GetSection("Authentication").Bind(authenticationSettings);

		services.AddAuthentication(option =>
		{
			option.DefaultAuthenticateScheme = "Bearer";
			option.DefaultScheme = "Bearer";
			option.DefaultChallengeScheme = "Bearer";
		}).AddJwtBearer(cfg =>
		{
			cfg.RequireHttpsMetadata = false;
			cfg.SaveToken = true;
			cfg.TokenValidationParameters = new TokenValidationParameters
			{
				ValidIssuer = authenticationSettings.JwtIssuer,
				ValidAudience = authenticationSettings.JwtIssuer,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
			};
		});
		services.AddSingleton(authenticationSettings);
		services.AddScoped<IJwtService, JwtService>();
	}

	public static void Seed(this WebApplication app)
	{
		
	}
}