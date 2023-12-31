namespace BarberShop.API;

public class AuthenticationSettings
{
	public string JwtKey { get; set; } = default!;
	public string JwtIssuer { get; set; } = default!;
	public int JwtExpireDays { get; set; }
}