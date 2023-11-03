using BarberShop.Domain.Entites;

namespace BarberShop.Application.Interfaces.Repositories;

public interface IShopRepository : IGenericRepository<Shop>
{
	Task<IEnumerable<Review>> GetReviewsAsync(int shopId);
	Task<IEnumerable<BarberService>> GetServicesAsync(int shopId);
}