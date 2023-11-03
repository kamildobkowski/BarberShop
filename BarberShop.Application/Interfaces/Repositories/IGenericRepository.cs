using BarberShop.Domain.Common;
using BarberShop.Domain.Entites;

namespace BarberShop.Application.Interfaces.Repositories;

public interface IGenericRepository<T>
{
	Task<T?> GetByIdAsync(int id);
	Task<IEnumerable<T?>?> GetAllAsync();
	Task DeleteAsync(T entity);
	Task AddAsync(T entity);
	Task UpdateAsync(T entity);
}