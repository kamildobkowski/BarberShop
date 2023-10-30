using BarberShop.Shops.Entities;

namespace BarberShop.Accounts.Entities.Roles;

public class ShopAdmin : User
{
	public int ShopId { get; set; }
	public virtual Shop Shop { get; set; }
}