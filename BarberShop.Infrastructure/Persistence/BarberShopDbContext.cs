using BarberShop.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Persistence;

public class BarberShopDbContext : DbContext
{
	
	public BarberShopDbContext(DbContextOptions<BarberShopDbContext> dbContextOptions)
	{
		
	}
	public DbSet<Shop?> Shops { get; set; }
	public DbSet<BarberService> Services { get; set; }
	public DbSet<Review> Reviews { get; set; }
	public DbSet<Address> Addresses { get; set; }
	public Task<int> SaveChangesAsync()
	{
		return base.SaveChangesAsync();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<BarberShop.Domain.Entites.Shop>().HasOne(r => r.Address);
	}
}