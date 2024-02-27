using BarberShop.Domain.Entites;
using BarberShop.Domain.Entites.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Http;

namespace BarberShop.Infrastructure.Persistence.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.HasKey(r => r.Id);
		builder
			.HasMany(r => r.Users)
			.WithOne();
		builder.HasData(Role.GetValues());
	}
}