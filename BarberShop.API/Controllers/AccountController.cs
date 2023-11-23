using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BarberShop.Application.Dto.Account;
using BarberShop.Application.Services.Accounts.Commands;
using BarberShop.Application.Services.Accounts.Queries;
using BarberShop.Domain.Entites.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BarberShop.API.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
	private readonly IMediator _mediator;
	private readonly AuthenticationSettings _authenticationSettings;

	public AccountController(IMediator mediator ,AuthenticationSettings authenticationSettings)
	{
		_mediator = mediator;
		_authenticationSettings = authenticationSettings;
	}
	
	[HttpPost("register")]
	public async Task<ActionResult> RegisterCustomer([FromBody] CreateCustomerDto dto)
	{
		await _mediator.Send(new CreateCustomerCommand(dto));
		return Ok();
	}

	[HttpPost("register/shopadmin")]
	public async Task<ActionResult> RegisterShopAdmin([FromBody] CreateShopAdminDto dto)
	{
		await _mediator.Send(new CreateShopAdminCommand(dto));
		return Ok();
	}

	[HttpPost("login")]
	public async Task<ActionResult<String>> Login([FromBody] LoginDto dto)
	{
		var user = await _mediator.Send(new VerifyUserLoginQuery(dto));
		var token = GenerateToken(user);
		return Ok(token);
	}
	
	private string GenerateToken(User user)
	{
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Name, $"{user.FirstName} {user.Surname}"),
			new Claim(ClaimTypes.Role, $"{user.Role.Name}")
		};

		if (!string.IsNullOrEmpty(user.Nationality))
		{
			claims.Add(new Claim("Nationality", user.Nationality));
		}
		
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