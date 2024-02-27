using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BarberShop.Application.Interfaces;
using BarberShop.Domain.Entites.Users;
using Microsoft.IdentityModel.Tokens;

namespace BarberShop.Infrastructure.Authorization.Jwt;

public class JwtService : IJwtService
{
	private readonly AuthenticationSettings _authenticationSettings;

	public JwtService(AuthenticationSettings authenticationSettings)
	{
		_authenticationSettings = authenticationSettings;
	}
	public string GenerateToken(User user)
	{
		var claims = new List<Claim>
		{
			new(Claims.Id, user.Id.ToString()),
			new(Claims.FullName, $"{user.FirstName} {user.Surname}"),
			new(Claims.Role, $"{user.Role.Name}"),
			new(Claims.Email, $"{user.Email}")
		};
		if(user.ShopAdmin is not null)
			claims.Add(new Claim(Claims.ShopId, user.ShopAdmin.ShopId.ToString()));
		
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
		var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var date = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

		var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
			_authenticationSettings.JwtIssuer, 
			claims, 
			expires: date, 
			signingCredentials: cred
		);

		var tokenHandler = new JwtSecurityTokenHandler();
		return tokenHandler.WriteToken(token);
	}
}