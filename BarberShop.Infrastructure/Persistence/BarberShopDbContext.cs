using BarberShop.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Persistence;

public class BarberShopDbContext : DbContext
{
	
	public BarberShopDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
	{
		
	}
	public DbSet<Shop> Shops { get; set; } = default!;
	public DbSet<BarberService> Services { get; set; } = default!;
	public DbSet<Review> Reviews { get; set; } = default!;
	public Task<int> SaveChangesAsync()
	{
		return base.SaveChangesAsync();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<BarberShop.Domain.Entites.Shop>().HasOne(r => r.Address);
	}
}