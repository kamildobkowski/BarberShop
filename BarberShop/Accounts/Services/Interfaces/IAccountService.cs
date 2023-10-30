using BarberShop.Accounts.Models.Dto;

namespace BarberShop.Accounts.Services.Interfaces;

public interface IAccountService
{
	public void Register(CreateCustomerDto dto);
	public string GenerateJwt(LoginDto dto);
}