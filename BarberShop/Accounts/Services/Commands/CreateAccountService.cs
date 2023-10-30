using AutoMapper;
using BarberShop.Accounts.Entities;
using BarberShop.Accounts.Models.Dto;
using BarberShop.Shared.Data;
using Microsoft.AspNetCore.Identity;

namespace BarberShop.Accounts.Services.Commands;

public class CreateAccountService : ICreateAccountService
{
	private readonly IMapper _mapper;
	private readonly BarberShopDbContext _dbContext;
	private readonly IPasswordHasher<User> _hasher;

	public CreateAccountService(IMapper mapper, BarberShopDbContext dbContext, IPasswordHasher<User> hasher)
	{
		_mapper = mapper;
		_dbContext = dbContext;
		_hasher = hasher;
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
}