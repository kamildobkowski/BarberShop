using System.ComponentModel.DataAnnotations;

namespace BarberShop.Domain.Entites.Users;

public class ShopAdmin
{
	public int UserId { get; set; }
	public User User { get; set; } = default!;
	public int ShopId { get; set; }
	public virtual Shop Shop { get; set; } = default!;
}