namespace BarberShop.Application.Interfaces.Repositories;

public interface IPagination<T> where T : class
{
	Task<IEnumerable<T>> GetPageAsync(int page, int pageSize, string? filter, string? orderBy, bool asc);
}