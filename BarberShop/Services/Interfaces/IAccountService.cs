using BarberShop.Models.Dto;

namespace BarberShop.Services.Interfaces;

public interface IAccountService
{
	public void Register(CreateUserDto dto);
	public string GenerateJwt(LoginDto dto);
}