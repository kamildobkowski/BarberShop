using BarberShop.Domain.Entites.Users;

namespace BarberShop.Application.Dto.Account;

public record CreateCustomerDto
{
	public string Email { get; set; } = default!;
	public string Password { get; set; } = default!;
	public string ConfirmPassword { get; set; } = default!;
	public string FirstName { get; set; } = default!;
	public string Surname { get; set; } = default!;
	public string? Nationality { get; set; }
	public string PhoneNumber { get; set; } = default!;
}