using BarberShop.Domain.Common;

namespace BarberShop.Domain.Entites.Users;

public class User : BaseEntity
{
	public string Email { get; set; } = default!;
	public string PasswordHash { get; set; } = default!;
	public string FirstName { get; set; } = default!;
	public string Surname { get; set; } = default!;
	public string? Nationality { get; set; }
	public int RoleId { get; set; }
	public virtual Customer? Customer { get; set; }
	public virtual ShopAdmin? ShopAdmin { get; set; }
}