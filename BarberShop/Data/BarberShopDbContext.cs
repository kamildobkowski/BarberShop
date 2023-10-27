using BarberShop.Entities.BarberShop;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Data;

public class BarberShopDbContext : DbContext
{
	private readonly string? _connectionString;

	public DbSet<Shop> Shops { get; set; }
	public DbSet<Review> Reviews { get; set; }
	public DbSet<Service> Services { get; set; }
	public DbSet<Address> Addresses { get; set; }
	
	public BarberShopDbContext(IConfiguration config)
	{
		_connectionString = config.GetValue<string>("ConnectionString:dbConnectionString");
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(_connectionString);
	}
	
}