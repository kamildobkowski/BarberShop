<<<<<<<< HEAD:BarberShop.Infrastructure/Authorization/AuthenticationSettings.cs
namespace BarberShop.Infrastructure.Authorization;
========
namespace BarberShop.Infrastructure.Authorization.Jwt;
>>>>>>>> 93f592b (Move jwt provider to infrastructure project):BarberShop.Infrastructure/Authorization/Jwt/AuthenticationSettings.cs

public class AuthenticationSettings
{
	public string JwtKey { get; set; } = default!;
	public string JwtIssuer { get; set; } = default!;
	public int JwtExpireDays { get; set; }
}