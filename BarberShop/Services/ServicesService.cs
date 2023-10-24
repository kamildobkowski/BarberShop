using AutoMapper;
using BarberShop.Data;
using BarberShop.Entities.BarberShop;
using BarberShop.Models.Dto;
using BarberShop.Models.Exceptions;
using BarberShop.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BarberShop.Services;

public class ServicesService : IServicesService
{
	private readonly BarberShopDbContext _dbContext;
	private readonly IMapper _mapper;

	public ServicesService(BarberShopDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	public GetServiceDto GetById(int shopId, int serviceId)
	{
		var entity = _dbContext.Services
			.FirstOrDefault(c => c.Id == serviceId && c.ShopId == shopId);
		if (entity is null) throw new Exception();
		var dto = _mapper.Map<GetServiceDto>(entity);
		return dto;
	}

	public IEnumerable<Service> GetAll(int shopId)
	{
		var services = _dbContext.Services.ToList();
		var entities = from s in services
			where s.ShopId == shopId
			select s;
		var enumerable = entities.ToList();
		if (enumerable.IsNullOrEmpty()) 
			throw new NotFoundException();

		return enumerable;
	}

	public void Add(int shopId, CreateServiceDto dto)
	{
		var shop = _dbContext.Shops.FirstOrDefault(c => c.Id == shopId);
		if (shop is null)
			throw new NotFoundException();
		var entity = _mapper.Map<Service>(dto);
		entity.ShopId = shopId;
		_dbContext.Services.Add(entity);
		_dbContext.SaveChanges();
	}
}