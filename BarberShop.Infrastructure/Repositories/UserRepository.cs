using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites.Users;
using BarberShop.Domain.Exceptions;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
	private readonly BarberShopDbContext _dbContext;

	public UserRepository(BarberShopDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public async Task<User> GetByIdAsync(int id)
	{
		var entity = await _dbContext.Users.FirstOrDefaultAsync(r => r.Id == id);
		if (entity is null)
			throw new NotFoundException();
		return entity;
	}

	public async Task<IEnumerable<User>> GetAllAsync()
	{
		return await _dbContext.Users.ToListAsync();
	}

	public async Task DeleteAsync(User entity)
	{
		_dbContext.Users.Remove(entity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task<int> AddAsync(User entity)
	{
		_dbContext.Users.Add(entity);
		await _dbContext.SaveChangesAsync();
		return entity.Id;
	}

	public async Task SaveChangesAsync()
	{
		await _dbContext.SaveChangesAsync();
	}
}