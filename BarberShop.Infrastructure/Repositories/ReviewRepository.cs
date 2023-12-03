using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using BarberShop.Infrastructure.Persistence;

namespace BarberShop.Infrastructure.Repositories;

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
	private readonly BarberShopDbContext _dbContext;
	public ReviewRepository(BarberShopDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
}