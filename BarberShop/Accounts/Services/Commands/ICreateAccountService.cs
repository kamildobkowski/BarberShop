using BarberShop.Accounts.Models.Dto;

namespace BarberShop.Accounts.Services.Commands;

public interface ICreateAccountService
{
	public void Register(CreateCustomerDto dto);
}