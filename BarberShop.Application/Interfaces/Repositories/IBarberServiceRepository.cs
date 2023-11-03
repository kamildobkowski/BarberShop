using BarberShop.Domain.Entites;

namespace BarberShop.Application.Interfaces.Repositories;

public interface IBarberServiceRepository : IGenericRepository<BarberService>
{
	Task<BarberService?> GetByIdAsync(int shopId, int id);
	Task AddAsync(BarberService entity, int shopId);
}