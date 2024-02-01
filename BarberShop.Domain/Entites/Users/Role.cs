using BarberShop.Domain.Common;

namespace BarberShop.Domain.Entites.Users;

public sealed class Role : Enumeration<Role>
{
	public static readonly Role Admin = new(1, "Admin");
	public static readonly Role ShopAdmin = new(2, "ShopAdmin");
	public static readonly Role Customer = new(3, "Customer");
	private Role(int id, string name) : base(id, name)
	{
		
	}

	public ICollection<User> Users { get; set; }
	public ICollection<Permissions> Permissions { get; set; }
}