using BarberShop.Domain.DataSets.Permissions;
using BarberShop.Domain.Entites;
using BarberShop.Domain.Entites.Users;
using BarberShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Infrastructure.Persistence.Configurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
	public void Configure(EntityTypeBuilder<RolePermission> builder)
	{
		builder.HasKey(r => new { r.RoleId, r.PermissionId });

		builder.HasData(PermissionList.RolePermissionsToEnumerable());
	}

	private static RolePermission Create(Role role, EnPermission permission)
	{
		return new RolePermission(role.Id, (int)permission);
	}
}