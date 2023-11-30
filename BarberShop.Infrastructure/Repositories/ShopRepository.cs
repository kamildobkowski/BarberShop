using System.Linq.Expressions;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using BarberShop.Infrastructure.ExternalServices;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
}