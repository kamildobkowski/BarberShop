using System.Runtime.CompilerServices;
using BarberShop.Application.Dto;
using BarberShop.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Application;

public static class QueryablePagedList
{
	public static async Task<PagedList<T>> ToPagedList<T>(this IQueryable<T> queryable, int page, int pageSize)
	{
		return new PagedList<T>(await queryable.ToListAsync(), page, pageSize, await queryable.CountAsync());
	}
}