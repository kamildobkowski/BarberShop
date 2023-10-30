using AutoMapper;
using BarberShop.Shared.Data;
using BarberShop.Shared.Exceptions;
using BarberShop.Shops.Entities;
using BarberShop.Shops.Models.Dto;
using BarberShop.Shops.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Shops.Services;

public class ShopService : IShopService
{
	private readonly IMapper _mapper;
	private readonly ILocationService _locationService;
	private readonly BarberShopDbContext _dbContext;

	public ShopService(IMapper mapper, ILocationService locationService, BarberShopDbContext dbContext)
	{
		_mapper = mapper;
		_locationService = locationService;
		_dbContext = dbContext;
	}
	
	public async Task<int> AddShop(CreateShopDto dto)
	{
		var entity = _mapper.Map<Shop>(dto);
		await _locationService.GetCoordinates(entity.Address);
		await _dbContext.Shops.AddAsync(entity);
		await _dbContext.SaveChangesAsync();
		return entity.Id;
	}

	public IEnumerable<GetShopDto> GetAllShops()
	{
		var entitiesList = _dbContext.Shops
			.Include(r => r.Address)
			.Include(r => r.Services)
			.Include(r => r.Reviews)
			.ToList();
		var dtos = _mapper.Map<List<GetShopDto>>(entitiesList);

		return dtos;
	}

	public GetShopDto GetById(int id)
	{
		var entity = _dbContext.Shops
			.Include(r=> r.Address)
			.FirstOrDefault(r => r.Id == id);
		var dto = _mapper.Map<GetShopDto>(entity);
		return dto;
	}

	public void DeleteShop(int shopId)
	{
		var entity = _dbContext.Shops.FirstOrDefault(r => r.Id == shopId);
		if (entity is null)
			throw new NotFoundException();
		_dbContext.Shops.Remove(entity);
		_dbContext.SaveChanges();
	}

	public void Update(int id, CreateShopDto dto)
	{
		var entity = _dbContext.Shops.Include(shop => shop.Address).FirstOrDefault(r => r.Id == id);
		if (entity is null)
			throw new NotFoundException();
		var updated = _mapper.Map<Shop>(dto);
		_locationService.GetCoordinates(updated.Address).GetAwaiter().GetResult();
		if (updated.Address is not null)
		{
			_dbContext.Addresses.Remove(entity.Address);
			entity.Address = updated.Address;
		}
		if(updated.Name is not null) entity.Name = updated.Name;
		_dbContext.SaveChanges();
	}
}