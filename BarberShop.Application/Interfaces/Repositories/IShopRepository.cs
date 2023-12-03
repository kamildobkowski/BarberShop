using BarberShop.Domain.Entites;

namespace BarberShop.Application.Interfaces.Repositories;

public interface IShopRepository : IGenericRepository<Shop>, IPagination<Shop>
{
	
}