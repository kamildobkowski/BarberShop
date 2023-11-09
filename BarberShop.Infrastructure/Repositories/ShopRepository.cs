using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using BarberShop.Domain.Exceptions;
using BarberShop.Domain.ValueObjects;
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


	public async Task<Shop> GetByIdAsync(int id)
	{
		var entity = await _dbContext.Shops
			.Include(r => r.Address)
			.Include(r => r.Services)
			.Include(r => r.Reviews)
			.FirstOrDefaultAsync(r => r.Id == id);
		if (entity is null)
			throw new NotFoundException();
		return entity;
	}

	public async Task<IEnumerable<Shop>> GetAllAsync() => await _dbContext.Shops
		.Include(r=>r.Services)
		.Include(r=>r.Address)
		.Include(r=>r.Reviews)
		.ToListAsync();

	public async Task DeleteAsync(Shop entity)
	{
		_dbContext.Shops.Remove(entity);
		await _dbContext.SaveChangesAsync();
	}
	public async Task<int> AddAsync(Shop entity)
	{
		await _locationService.GetLocation(entity.Address);
		_dbContext.Shops.Add(entity);
		await _dbContext.SaveChangesAsync();
		return entity.Id;
	}

	public Task UpdateAsync(Shop entity)
	{
		throw new NotImplementedException();
	}

	public async Task SaveChangesAsync()
	{
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

	public async Task UpdateAddressAsync(int shopId, Address updatedAddress)
	{
		var entity = (await GetByIdAsync(shopId)).Address;
		if (updatedAddress.City is not null)
		{
			entity.City = updatedAddress.City;
		}
		if (updatedAddress.Street is not null)
		{
			entity.Street = updatedAddress.Street;
		}
		if (updatedAddress.Number is not null)
		{
			entity.Number = updatedAddress.Number;
		}
		if (updatedAddress.ApartamentNumber is not null)
		{
			entity.ApartamentNumber = updatedAddress.ApartamentNumber;
		}
		if (updatedAddress.PostalCode is not null)
		{
			entity.PostalCode = updatedAddress.PostalCode;
		}

		entity.Updated = updatedAddress.Created;

		await _dbContext.SaveChangesAsync();
	}
}