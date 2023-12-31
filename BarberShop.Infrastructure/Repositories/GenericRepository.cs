using System.Linq.Expressions;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Repositories;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
{
	private readonly BarberShopDbContext _dbContext;

	protected GenericRepository(BarberShopDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public virtual async Task<T?> GetByIdAsync(int id)
		=> await _dbContext.Set<T>().FindAsync(id);

	public virtual Task<T?> GetAsync(Expression<Func<T, bool>> lambda)
		=> _dbContext.Set<T>().FirstOrDefaultAsync(lambda);

	public virtual async Task<IEnumerable<T>> GetAllAsync()
		=> await _dbContext.Set<T>().ToListAsync();

	public IQueryable<T> GetQueryable()
		=> _dbContext.Set<T>();

	public virtual void Delete(T entity)
		=> _dbContext.Set<T>().Remove(entity);

	public virtual void Add(T entity)
		=> _dbContext.Set<T>().Add(entity);

	public virtual async Task SaveChangesAsync()
		=> await _dbContext.SaveChangesAsync();

	public virtual void AddRange(IEnumerable<T> entities)
		=> _dbContext.Set<T>().AddRange(entities);

	public virtual void DeleteRange(IEnumerable<T> entities)
		=> _dbContext.Set<T>().RemoveRange(entities);

	protected IQueryable<T> GetPageQuery(int page, int pageSize, Expression<Func<T, object>> orderBy, 
		Expression<Func<T, bool>> filter, bool asc=true)
		=> asc ? _dbContext.Set<T>()
				.Where(filter)
				.OrderBy(orderBy)
				.Skip((page-1)*pageSize)
				.Take(pageSize)
			: _dbContext.Set<T>()
				.Where(filter)
				.OrderByDescending(orderBy)
				.Skip((page-1)*pageSize)
				.Take(pageSize);
}