using System.Linq.Expressions;
using BarberShop.Domain.Common;
using BarberShop.Domain.Entites;

namespace BarberShop.Application.Interfaces.Repositories;

public interface IGenericRepository<T>
{
	Task<T?> GetByIdAsync(int id);
	Task<T?> GetAsync(Expression<Func<T, bool>> lambda);
	Task<IEnumerable<T>> GetAllAsync();
	IQueryable<T> GetQueryable();
	void Delete(T entity);
	void Add(T entity);
	Task SaveChangesAsync();
}