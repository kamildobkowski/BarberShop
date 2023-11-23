namespace BarberShop.Application.Dto.Account;

public record CreateShopAdminDto
(
	string Email,
	string Password,
	string FirstName,
	string Surname, 
	string? Nationality
);