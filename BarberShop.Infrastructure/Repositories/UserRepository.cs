using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites.Users;
using BarberShop.Domain.Exceptions;
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
	
}