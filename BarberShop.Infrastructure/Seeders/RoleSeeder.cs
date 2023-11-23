using BarberShop.Domain.Entites.Users;
using BarberShop.Infrastructure.Persistence;

namespace BarberShop.Infrastructure.Seeders;

public interface IRoleSeeder
{
	void Seed();
}

public class RoleSeeder : IRoleSeeder
{
	private readonly BarberShopDbContext _dbContext;

	public RoleSeeder(BarberShopDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public void Seed()
	{
		if (!_dbContext.Database.CanConnect())
			return;
		if (!_dbContext.Roles.Any())
		{
			var roles = GetRoles();
			_dbContext.Roles.AddRange(roles);
			_dbContext.SaveChanges();
		}
	}

	public List<Role> GetRoles()
	{
		var roles = new List<Role>
		{
			new()
			{
				Name = "Admin"
			},
			new()
			{
				Name = "Customer"
			},
			new()
			{
				Name = "ShopAdmin"
			}
		};
		return roles;
	}
}