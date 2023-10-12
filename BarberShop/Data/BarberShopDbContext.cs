using Microsoft.EntityFrameworkCore;

namespace BarberShop.Data;

public class BarberShopDbContext : DbContext
{
	private readonly string? _connectionString;

	public BarberShopDbContext(IConfiguration config)
	{
		_connectionString = config.GetValue<string>("ConnectionString:dbConnectionString");
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(_connectionString);
	}
	
}