using BarberShop.Domain.Entites;
using BarberShop.Domain.Entites.Appointments;
using BarberShop.Domain.Entites.Users;
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
	public DbSet<User> Users { get; set; } = default!;
	public DbSet<Role> Roles { get; set; } = default!;
	public DbSet<ShopAdmin> ShopAdmins { get; set; } = default!;
	public DbSet<Customer> Customers { get; set; } = default!;
	public DbSet<Appointment> Appointments { get; set; } = default!;
	public DbSet<Slot> TimeTable { get; set; } = default!;
	public Task<int> SaveChangesAsync()
	{
		return base.SaveChangesAsync();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Shop>().HasOne(r => r.Address);
		modelBuilder.Entity<Shop>()
			.HasMany(r => r.Appointments)
			.WithOne(r => r.Shop)
			.OnDelete(DeleteBehavior.Restrict);
		modelBuilder.Entity<User>()
			.HasOne(r => r.Customer)
			.WithOne(r => r.User)
			.HasForeignKey<Customer>(r => r.UserId)
			.IsRequired(false);
		modelBuilder.Entity<User>()
			.HasOne(r => r.ShopAdmin)
			.WithOne(r => r.User)
			.HasForeignKey<ShopAdmin>(r => r.UserId)
			.IsRequired(false);
		modelBuilder.Entity<Customer>()
			.HasKey(r => r.UserId);
		modelBuilder.Entity<ShopAdmin>()
			.HasKey(r => r.UserId);
		modelBuilder.Entity<Appointment>()
			.HasOne(r => r.Shop)
			.WithMany(r => r.Appointments)
			.OnDelete(DeleteBehavior.Restrict);
	}
}