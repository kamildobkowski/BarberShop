using BarberShop.Shops.Models.Dto;

namespace BarberShop.Shops.Services.Interfaces;

public interface IShopService
{
	public Task<int> AddShop(CreateShopDto dto);
	public IEnumerable<GetShopDto> GetAllShops();
	public GetShopDto GetById(int id);
	public void DeleteShop(int shopId);
	public void Update(int id, CreateShopDto dto);
}