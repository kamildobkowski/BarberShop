namespace BarberShop.Entities.Auth;

public class User
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Surname { get; set; }
	public DateTime? DateOfBirth { get; set; }
	public string Email { get; set; }
	public string PasswordHash { get; set; } 
	public string Nationality { get; set; }
	public Role Role { get; set; }
}