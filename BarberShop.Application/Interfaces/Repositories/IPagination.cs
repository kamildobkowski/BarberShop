using BarberShop.Domain.Common;
using BarberShop.Domain.Entites;
using BarberShop.Domain.Entites.Appointments;

namespace BarberShop.Application.Interfaces.Repositories;

public interface IPagination<T> where T : class
{
	Task<PagedList<T>> GetPageAsync(int page, int pageSize, string? filter, string? orderBy, bool sortOrder);
}