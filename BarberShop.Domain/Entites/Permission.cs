using BarberShop.Domain.Common;

namespace BarberShop.Domain.Entites;

public sealed class Permission : Enumeration<Permission> 
{
	public Permission(int id, string name) : base(id, name)
	{
	}
}