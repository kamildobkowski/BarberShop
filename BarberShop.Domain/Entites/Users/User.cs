using BarberShop.Domain.Common;

namespace BarberShop.Domain.Entites.Users;

public class User : BaseEntity
{
	public string Email { get; set; } = default!;
	public string PasswordHash { get; set; } = default!;
	public string FirstName { get; set; } = default!;
	public string Surname { get; set; } = default!;
	public int RoleId { get; set; }
	public Role Role { get; set; } = default!;
	public Customer? Customer { get; set; }
	public ShopAdmin? ShopAdmin { get; set; }
}