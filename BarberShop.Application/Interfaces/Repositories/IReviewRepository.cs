using BarberShop.Domain.Entites;

namespace BarberShop.Application.Interfaces.Repositories;

public interface IReviewRepository : IGenericRepository<Review>
{
	Task<Review?> GetByIdAsync(int shopId, int id);
	Task AddAsync(Review entity, int shopId);
}