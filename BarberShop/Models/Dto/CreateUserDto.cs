using System.ComponentModel.DataAnnotations;
using BarberShop.Entities.Auth;

namespace BarberShop.Models.Dto;

public class CreateUserDto
{
	[Required]
	public string Name { get; set; }
	[Required]
	public string Surname { get; set; }
	public DateTime? DateOfBirth { get; set; }
	[Required]
	public string Email { get; set; }
	[Required]
	public string Password { get; set; }
	[Required]
	public string Nationality { get; set; }
	public Role Role { get; set; } = Role.Customer;
}