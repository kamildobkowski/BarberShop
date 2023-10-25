using BarberShop.Entities.BarberShop;
using BarberShop.Models.Dto;

namespace BarberShop.Services.Interfaces;

public interface IShopService
{
	public Task<int> AddShop(CreateShopDto dto);
	public IEnumerable<GetShopDto> GetAllShops();
	public GetShopDto GetById(int id);
}