using BarberShop.Domain.Entites.Users;
using BarberShop.Infrastructure.Persistence;

namespace BarberShop.Infrastructure.Repositories;

public class CustomerRepository : GenericRepository<Customer>
{
	private readonly BarberShopDbContext _dbContext;
	public CustomerRepository(BarberShopDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
}