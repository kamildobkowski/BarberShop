using BarberShop.Domain.Common;

namespace BarberShop.Domain.Entites;

public sealed class Permissions : Enumeration<Permissions> 
{
	public Permissions(int id, string name) : base(id, name)
	{
	}
}