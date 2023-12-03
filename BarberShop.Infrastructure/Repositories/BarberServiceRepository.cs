using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using BarberShop.Infrastructure.Persistence;

namespace BarberShop.Infrastructure.Repositories;

public class BarberServiceRepository : GenericRepository<BarberService>, IBarberServiceRepository
{
	private readonly BarberShopDbContext _dbContext;

	public BarberServiceRepository(BarberShopDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
	
}