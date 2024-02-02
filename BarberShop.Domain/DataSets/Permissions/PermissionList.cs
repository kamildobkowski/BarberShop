using BarberShop.Domain.Entites;
using BarberShop.Domain.Entites.Users;
using BarberShop.Domain.ValueObjects;

namespace BarberShop.Domain.DataSets.Permissions;

public static class PermissionList
{
	public static Dictionary<Role, IEnumerable<EnPermission>> Permissions { get; } = InitializeDictrionary();
	private static readonly EnPermission[] Admin = Enum.GetValues<EnPermission>();
	private static readonly EnPermission[] ShopAdmin = 
	{
		EnPermission.ReadShop,
		EnPermission.UpdateShop
	};

	private static readonly EnPermission[] Customer =
	{
		EnPermission.ReadShop
	};

	private static Dictionary<Role, IEnumerable<EnPermission>> InitializeDictrionary()
	{
		var dictionary = new Dictionary<Role, IEnumerable<EnPermission>>
		{
			{ Role.Admin, Admin },
			{ Role.ShopAdmin, ShopAdmin },
			{ Role.Customer, Customer }
		};
		return dictionary;
	}

	public static IEnumerable<RolePermission> RolePermissionsToEnumerable()
	{
		foreach (var role in Permissions)
		{
			foreach (var permission in role.Value)
			{
				yield return new RolePermission(role.Key.Id, (int)permission);
			}
		}
	}
}