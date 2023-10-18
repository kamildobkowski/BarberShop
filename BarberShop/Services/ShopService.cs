using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using AutoMapper;
using BarberShop.Data;
using BarberShop.Entities.BarberShop;
using BarberShop.Models.Dto;
using BarberShop.Services.Interfaces;

namespace BarberShop.Services;

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
	public async Task<int> AddShopAsync(CreateShopDto dto)
	{
		var entity = _mapper.Map<Shop>(dto);
		await _locationService.GetCoordinates(entity.Address);
		await _dbContext.Shops.AddAsync(entity);
		await _dbContext.SaveChangesAsync();
		return entity.Id;
	}

	
}