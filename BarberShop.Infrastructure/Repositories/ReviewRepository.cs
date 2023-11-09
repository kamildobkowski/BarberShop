using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using BarberShop.Domain.Exceptions;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
	private readonly BarberShopDbContext _dbContext;

	public ReviewRepository(BarberShopDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public async Task<IEnumerable<Review?>?> GetAllAsync()
	{
		return await _dbContext.Reviews.ToListAsync();
	}

	public async Task DeleteAsync(Review entity)
	{
		_dbContext.Reviews.Remove(entity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task<int> AddAsync(Review entity)
	{
		_dbContext.Reviews.Add(entity);
		await _dbContext.SaveChangesAsync();
		return entity.Id;
	}

	public async Task UpdateAsync(Review entity)
	{
		_dbContext.Reviews.Update(entity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task SaveChangesAsync()
	{
		await _dbContext.SaveChangesAsync();
	}

	public async Task<Review> GetByIdAsync(int id)
	{
		var entity = await _dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);
		return entity;
	}

	public async Task<Review> GetByIdAsync(int shopId, int id)
	{
		var entity = await _dbContext.Reviews
			.FirstOrDefaultAsync(r => r.Id == id && r.ShopId==shopId);
		return entity;
	}

	public async Task AddAsync(Review entity, int shopId)
	{
		var shop = await _dbContext.Shops.FirstOrDefaultAsync(r => r!.Id == shopId);
		if (shop is null)
			throw new NotFoundException();
		entity.ShopId = shopId;
		_dbContext.Reviews.Add(entity);
		await _dbContext.SaveChangesAsync();
	}
}