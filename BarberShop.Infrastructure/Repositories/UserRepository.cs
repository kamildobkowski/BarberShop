using System.Linq.Expressions;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites.Users;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
	private readonly BarberShopDbContext _dbContext;

	public UserRepository(BarberShopDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}

	public override async Task<User?> GetAsync(Expression<Func<User, bool>> lambda)
	{
		return await _dbContext
			.Users
			.Include(r => r.Role)
			.Include(r => r.Customer)
			.Include(r => r.ShopAdmin)
			.FirstOrDefaultAsync(lambda);
	}

	public override async Task<User?> GetByIdAsync(int id)
		=> await _dbContext
			.Users
			.Include(r => r.Role)
			.Include(r => r.ShopAdmin)
			.Include(r => r.Customer)
			.FirstOrDefaultAsync(r => r.Id == id);

	public override async Task<IEnumerable<User>> GetAllAsync()
		=> await _dbContext
			.Users
			.Include(r => r.Role)
			.Include(r => r.ShopAdmin)
			.Include(r => r.Customer)
			.ToListAsync();
}