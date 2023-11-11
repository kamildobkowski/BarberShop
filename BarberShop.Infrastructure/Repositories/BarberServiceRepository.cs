using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using BarberShop.Domain.Exceptions;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Repositories;

public class BarberServiceRepository : IBarberServiceRepository
{
	private readonly BarberShopDbContext _dbContext;

	public BarberServiceRepository(BarberShopDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public async Task<BarberService> GetByIdAsync(int id)
	{
		var entity = await _dbContext.Services.FirstOrDefaultAsync(r => r.Id == id);
		if (entity is null)
			throw new NotFoundException();
		return entity;
	}

	public async Task<IEnumerable<BarberService>> GetAllAsync()
	{
		return await _dbContext.Services.ToListAsync();
	}

	public async Task DeleteAsync(BarberService entity)
	{
		_dbContext.Services.Remove(entity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task<int> AddAsync(BarberService entity)
	{
		_dbContext.Services.Add(entity);
		await _dbContext.SaveChangesAsync();
		return entity.Id;
	}

	public Task UpdateAsync(BarberService entity)
	{
		throw new NotImplementedException();
	}

	public async Task SaveChangesAsync()
	{
		await _dbContext.SaveChangesAsync();
	}

	public async Task<BarberService> GetByIdAsync(int shopId, int id)
	{
		var entity = await _dbContext.Services
			.FirstOrDefaultAsync(r => r.Id == id && r.ShopId == shopId);
		if (entity is null)
			throw new NotFoundException();
		return entity;
	}

	public async Task<int> AddAsync(BarberService entity, int shopId)
	{
		var shop = await _dbContext.Shops
			.FirstOrDefaultAsync(r => r.Id == shopId);
		if (shop is null)
			throw new NotFoundException();
		entity.ShopId = shopId;
		_dbContext.Services.Add(entity);
		await _dbContext.SaveChangesAsync();
		return entity.Id;
	}

	public async Task<IEnumerable<BarberService>> GetAllByShopIdAsync(int shopId)
	{
		var shops = await _dbContext.Services
			.Where(r => r.ShopId == shopId)
			.ToListAsync();
		return shops;
	}
}