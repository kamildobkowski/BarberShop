using BarberShop.Domain.Entites;
using BarberShop.Domain.ValueObjects;

namespace BarberShop.Application.Interfaces.Repositories;

public interface IShopRepository : IGenericRepository<Shop>
{
	Task<IEnumerable<Review>> GetReviewsAsync(int shopId);
	Task<IEnumerable<BarberService>> GetServicesAsync(int shopId);
	Task UpdateAddressAsync(int shopId, Address updatedAddress);
}