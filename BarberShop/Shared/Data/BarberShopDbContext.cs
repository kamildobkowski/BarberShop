using BarberShop.Accounts.Entities;
using BarberShop.Accounts.Entities.Roles;
using BarberShop.Shops.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Shared.Data;

public class BarberShopDbContext : DbContext
{
	private readonly string? _connectionString;

	public DbSet<Shop> Shops { get; set; }
	public DbSet<Review> Reviews { get; set; }
	public DbSet<Service> Services { get; set; }
	public DbSet<Address> Addresses { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<ShopAdmin> ShopAdmins { get; set; }
	
	public BarberShopDbContext(IConfiguration config)
	{
		_connectionString = config.GetValue<string>("ConnectionString:dbConnectionString");
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(_connectionString);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ShopAdmin>().ToTable("ShopAdmins");
	}
	
}