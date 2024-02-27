using BarberShop.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Pagination;

public static class QueryablePagedList
{
	public static async Task<PagedList<T>> ToPagedList<T>(this IQueryable<T> queryable, int page, int pageSize)
	{
		return new PagedList<T>(await queryable.ToListAsync(), page, pageSize,  await queryable.CountAsync());
	}
}