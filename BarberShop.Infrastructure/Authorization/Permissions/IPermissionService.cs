namespace BarberShop.Infrastructure.Authorization.Permissions;

public interface IPermissionService
{
	Task<HashSet<string>> GetPermissionsAsync(int userId);
}