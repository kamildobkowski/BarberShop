using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Authorization;

public class PermissionService : IPermissionService
{
	private readonly BarberShopDbContext _dbContext;

	public PermissionService(BarberShopDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public Task<HashSet<string>> GetPermissionsAsync(int userId)
	{
		return Task.FromResult(_dbContext.Users
			.Include(r => r.Role)
			.ThenInclude(r => r.Permissions)
			.Where(r => r.Id == userId)
			.Select(r => r.Role.Permissions)
			.SelectMany(r => r)
			.Select(r => r.Name)
			.ToHashSet());
	}
}