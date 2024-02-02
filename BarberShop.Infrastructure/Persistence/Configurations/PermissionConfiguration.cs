using System.Collections;
using BarberShop.Domain.Entites;
using BarberShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Infrastructure.Persistence.Configurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
	public void Configure(EntityTypeBuilder<Permission> builder)
	{
		builder.HasKey(r => r.Id);

		var permissions = Enum.GetValues<EnPermission>()
			.Select(r => new Permission
			(
				(int)r, r.ToString()
			));
		
		builder.HasData(permissions);
	}
}