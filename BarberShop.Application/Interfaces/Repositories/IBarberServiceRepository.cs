using BarberShop.Application.Dto.BarberServices;
using BarberShop.Domain.Entites;

namespace BarberShop.Application.Interfaces.Repositories;

public interface IBarberServiceRepository : IGenericRepository<BarberService>
{
	Task<BarberService> GetByIdAsync(int shopId, int id);
	Task<int> AddAsync(BarberService entity, int shopId);
	Task<IEnumerable<BarberService>> GetAllByShopIdAsync(int shopId);
}