using BarberShop.Domain.Entites.Users;

namespace BarberShop.Application.Interfaces;

public interface IJwtService
{
	string GenerateToken(User user);
}