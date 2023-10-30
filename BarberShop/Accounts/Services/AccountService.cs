using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BarberShop.Accounts.Entities;
using BarberShop.Accounts.Models.Dto;
using BarberShop.Accounts.Services.Interfaces;
using BarberShop.Shared.Data;
using BarberShop.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BarberShop.Accounts.Services;

public class AccountService : IAccountService
{
	private readonly BarberShopDbContext _dbContext;
	private readonly IMapper _mapper;
	private readonly IPasswordHasher<User> _hasher;
	private readonly AuthenticationSettings _authenticationSettings;

	public AccountService(BarberShopDbContext dbContext, IMapper mapper, IPasswordHasher<User> hasher, AuthenticationSettings authenticationSettings)
	{
		_dbContext = dbContext;
		_mapper = mapper;
		_hasher = hasher;
		_authenticationSettings = authenticationSettings;
	}

	public void Register(CreateCustomerDto dto)
	{
		var entity = _mapper.Map<User>(dto);
		if (entity is null)
			throw new ArgumentException();
		var hash = _hasher.HashPassword(entity, dto.Password);
		entity.PasswordHash = hash;
		_dbContext.Users.Add(entity);
		_dbContext.SaveChanges();
	}

	public string GenerateJwt(LoginDto dto)
	{
		var user = _dbContext.Users.FirstOrDefault(r => r.Email == dto.Email);
		if (user is null)
			throw new NotFoundException("Invalid email or password");
		var login = _hasher.VerifyHashedPassword(user, dto.Password, user.PasswordHash);
		if (PasswordVerificationResult.Failed == login)
			throw new NotFoundException("Invalid email or password");
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Role, user.Role.ToString()),
			new Claim(ClaimTypes.Name, $"{user.Name} {user.Surname}"),
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
		var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var date = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
		var token = new JwtSecurityToken(
			_authenticationSettings.JwtIssuer,
			_authenticationSettings.JwtIssuer,
			claims,
			expires: date,
			signingCredentials: cred);

		var tokenHandler = new JwtSecurityTokenHandler();
		return tokenHandler.WriteToken(token);
	}
}