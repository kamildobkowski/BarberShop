using BarberShop.Accounts.Models.Dto;

namespace BarberShop.Accounts.Services.Queries;

public interface ILoginService
{
	string GenerateJwt(LoginDto dto);
}