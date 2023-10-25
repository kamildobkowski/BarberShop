using AutoMapper;
using BarberShop.Data;
using BarberShop.Entities.BarberShop;
using BarberShop.Models.Dto;
using BarberShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

	
}