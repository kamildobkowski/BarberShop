using AutoMapper;
using BarberShop.Shared.Data;
using BarberShop.Shared.Exceptions;
using BarberShop.Shops.Entities;
using BarberShop.Shops.Models.Dto;
using BarberShop.Shops.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BarberShop.Shops.Services;

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

	public IEnumerable<GetServiceDto> GetAll(int shopId)
	{
		var services = _dbContext.Services.ToList();
		var entities = from s in services
			where s.ShopId == shopId
			select s;
		if (entities.IsNullOrEmpty()) 
			throw new NotFoundException();
		var result = _mapper.Map<IEnumerable<GetServiceDto>>(entities);
		return result;
	}

	public int Add(int shopId, CreateServiceDto dto)
	{
		var shop = _dbContext.Shops.FirstOrDefault(c => c.Id == shopId);
		if (shop is null)
			throw new NotFoundException();
		var entity = _mapper.Map<Service>(dto);
		entity.ShopId = shopId;
		_dbContext.Services.Add(entity);
		_dbContext.SaveChanges();
		return entity.Id;
	}

	public void Delete(int shopId, int id)
	{
		var entity = _dbContext.Services
			.FirstOrDefault(r => r.Id == id && r.ShopId == shopId);
		if (entity is null)
			throw new NotFoundException("Service not found!");
		_dbContext.Services.Remove(entity);
		_dbContext.SaveChanges();
	}

	public void Update(int shopId, int id, CreateServiceDto dto)
	{
		var entity = _dbContext.Services
			.FirstOrDefault(r => r.Id == id && r.ShopId == shopId);
		if (entity is null)
			throw new NotFoundException("Service not found!");
		var newEntity = _mapper.Map<Service>(dto);
		if (newEntity.Name is not null)
			entity.Name = newEntity.Name;
		if (!newEntity.Duration.Duration().Equals(TimeSpan.Zero))
			entity.Duration = newEntity.Duration;
		if (newEntity.Price != 0m)
			entity.Price = newEntity.Price;

		_dbContext.SaveChanges();
	}
}