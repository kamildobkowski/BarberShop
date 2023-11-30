using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites.Appointments;
using BarberShop.Infrastructure.Persistence;

namespace BarberShop.Infrastructure.Repositories;

public class TimeTableRepository : GenericRepository<Slot>, ITimeTableRepository 
{
	private readonly BarberShopDbContext _dbContext;

	public TimeTableRepository(BarberShopDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
}