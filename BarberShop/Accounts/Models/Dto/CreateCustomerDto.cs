using System.ComponentModel.DataAnnotations;
using BarberShop.Accounts.Entities;

namespace BarberShop.Accounts.Models.Dto;

public class CreateCustomerDto
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