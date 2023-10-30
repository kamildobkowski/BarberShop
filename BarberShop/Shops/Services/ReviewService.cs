using AutoMapper;
using BarberShop.Shared.Data;
using BarberShop.Shared.Exceptions;
using BarberShop.Shops.Entities;
using BarberShop.Shops.Models.Dto;
using BarberShop.Shops.Services.Interfaces;

namespace BarberShop.Shops.Services;

public class ReviewService : IReviewService
{
	private readonly BarberShopDbContext _dbContext;
	private readonly IMapper _mapper;

	public ReviewService(BarberShopDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}
	public IEnumerable<GetReviewDto> GetAll(int shopId)
	{
		var entitiesList = _dbContext.Reviews.ToList();
		var entities = 
			from i in entitiesList
			where i.ShopId == shopId
			select i;
		var dtos = _mapper.Map<IEnumerable<GetReviewDto>>(entities);
		return dtos;
	}

	public GetReviewDto GetById(int shopId, int id)
	{
		var entity = _dbContext.Reviews
			.FirstOrDefault(r => r.ShopId == shopId && r.Id == id);
		if (entity is null)
			throw new NotFoundException("Review has not been found!");
		var dto = _mapper.Map<GetReviewDto>(entity);
		return dto;
	}

	public int AddReview(int shopId, CreateReviewDto dto)
	{
		var entity = _mapper.Map<Review>(dto);
		var shopEntity = _dbContext.Shops.FirstOrDefault(r => r.Id == shopId);
		if (shopEntity is null)
			throw new NotFoundException("Shop not found!");
		if (entity is null)
			throw new ArgumentException("Review cannot be null!");
		entity.ShopId = shopId;
		_dbContext.Reviews.Add(entity);
		_dbContext.SaveChanges();
		return entity.Id;
	}

	public void DeleteReview(int shopId, int id)
	{
		var entity = _dbContext.Reviews
			.FirstOrDefault(r => r.Id == id && r.ShopId == shopId);
		if (entity is null)
			throw new NotFoundException("Review has not been found!");
		_dbContext.Reviews.Remove(entity);
		_dbContext.SaveChanges();
	}
}