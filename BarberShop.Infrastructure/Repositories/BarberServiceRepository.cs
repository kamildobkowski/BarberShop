using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
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
	public async Task<BarberService?> GetByIdAsync(int id)
	{
		return await _dbContext.Services.FirstOrDefaultAsync(r => r.Id == id);
	}

	public async Task<IEnumerable<BarberService?>?> GetAllAsync()
	{
		return await _dbContext.Services.ToListAsync();
	}

	public async Task DeleteAsync(BarberService entity)
	{
		_dbContext.Services.Remove(entity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task AddAsync(BarberService entity)
	{
		_dbContext.Services.Add(entity);
		await _dbContext.SaveChangesAsync();
	}

	public Task UpdateAsync(BarberService entity)
	{
		throw new NotImplementedException();
	}

	public async Task<BarberService?> GetByIdAsync(int shopId, int id)
	{
		return await _dbContext.Services
			.FirstOrDefaultAsync(r => r.Id == id && r.ShopId == shopId);
	}

	public async Task AddAsync(BarberService entity, int shopId)
	{
		var shop = _dbContext.Shops
			.FirstOrDefaultAsync(r => r.Id == shopId);
		if (shop is null)
			throw new Exception();
		entity.ShopId = shopId;
		_dbContext.Services.Add(entity);
		await _dbContext.SaveChangesAsync();
	}
}