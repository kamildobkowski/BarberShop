using System.Linq.Expressions;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using BarberShop.Domain.Exceptions;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Repositories;

public class BarberServiceRepository : GenericRepository<BarberService>, IBarberServiceRepository
{
	private readonly BarberShopDbContext _dbContext;

	public BarberServiceRepository(BarberShopDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
	
}