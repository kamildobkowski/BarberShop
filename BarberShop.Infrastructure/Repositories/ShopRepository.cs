using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using BarberShop.Infrastructure.ExternalServices;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Repositories;

public class ShopRepository : IShopRepository
{
	private readonly BarberShopDbContext _dbContext;
	private readonly ILocationService _locationService;

	public ShopRepository(BarberShopDbContext dbContext, ILocationService locationService)
	{
		_dbContext = dbContext;
		_locationService = locationService;
	}


	public async Task<Shop?> GetByIdAsync(int id) => await _dbContext.Shops.FirstOrDefaultAsync(r => r.Id == id);

	public async Task<IEnumerable<Shop?>?> GetAllAsync() => await _dbContext.Shops.Include(r=>r.Services).Include(r=>r.Address).ToListAsync();

	public async Task DeleteAsync(Shop entity)
	{
		_dbContext.Shops.Remove(entity);
		await _dbContext.SaveChangesAsync();
	}
	public async Task AddAsync(Shop entity)
	{
		await _locationService.GetLocation(entity.Address);
		_dbContext.Shops.Add(entity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(Shop entity)
	{
		_dbContext.Shops.Update(entity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task<IEnumerable<Review>> GetReviewsAsync(int shopId)
	{
		var entities = await _dbContext.Reviews.ToListAsync();
		var reviews =
			from i in entities
			where i.ShopId == shopId
			select i;
		return reviews;
	}

	public async Task<IEnumerable<BarberService>> GetServicesAsync(int shopId)
	{
		var entites = await _dbContext.Services.ToListAsync();
		var services = from i in entites
			where i.ShopId == shopId
			select i;
		return services;
	}
}