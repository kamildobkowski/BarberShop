using System.Linq.Expressions;
using BarberShop.Application;
using BarberShop.Application.Dto;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites.Appointments;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BarberShop.Infrastructure.Repositories;

public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{
	private readonly BarberShopDbContext _dbContext;

	public AppointmentRepository(BarberShopDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}

	public Task<PagedList<Appointment>> GetPageAsync(int page, int pageSize, string? filter, string? orderBy, bool sortOrder)
	{
		Expression<Func<Appointment, object>> ordedByLambda = orderBy!.ToLower() switch
		{
			"shop" => app => app.ShopId,
			"date" => app => app.StartTime,
			_ => app => app.Shop.Name
		};
		return base.GetPageQuery(page, pageSize, ordedByLambda,
				(app) => filter == null 
				         || app.Service.Name.Contains(filter) 
				         || app.Shop.Name.Contains(filter),
				sortOrder)
			.Include(r => r.Shop)
			.Include(r => r.Service)
			.Include(r => r.Customer)
			.ToPagedList(page, pageSize);
	}
}