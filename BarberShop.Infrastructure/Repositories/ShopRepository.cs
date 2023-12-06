using System.Linq.Expressions;
using BarberShop.Application;
using BarberShop.Application.Dto;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Application.Models;
using BarberShop.Domain.Entites;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BarberShop.Infrastructure.Repositories;

public class ShopRepository : GenericRepository<Shop>, IShopRepository
{
	private readonly BarberShopDbContext _dbContext;

	public ShopRepository(BarberShopDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}

	public override async Task<Shop?> GetAsync(Expression<Func<Shop, bool>> lambda)
		=> await _dbContext.Shops
			.Include(r=>r.Address)
			.Include(r=>r.Services)
			.Include(r=>r.Reviews)
			.Include(r=>r.Appointments)
			.FirstOrDefaultAsync(lambda);

	public override async Task<IEnumerable<Shop>> GetAllAsync()
		=> await _dbContext.Shops
			.Include(r => r.Address)
			.Include(r => r.Services)
			.Include(r => r.Reviews)
			.Include(r => r.Appointments)
			.ToListAsync();

	public async Task<PagedList<Shop>> GetPageAsync(int page, int pageSize, string? filter, string? orderBy ,bool sortOrder = true)
	{

		Expression<Func<Shop, object>> orderByLambda = orderBy.IsNullOrEmpty()
			? (shop) => shop.Id
			: orderBy?.ToLower() switch
			{
				"name" => shop => shop.Name,
				_ => shop => shop.Id
			};

		return await GetPageQuery
			(page, pageSize, orderByLambda,
				r => filter == null || r.Name.ToLower().Contains(filter.ToLower()), sortOrder)
			.Include(r => r.Reviews)
			.Include(r => r.Address)
			.Include(r => r.Services)
			.ToPagedList(page, pageSize);
	}
}