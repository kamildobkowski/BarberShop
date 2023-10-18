using BarberShop.Entities.BarberShop;
using BarberShop.Models.Dto;

namespace BarberShop.Services.Interfaces;

public interface IShopService
{
	public Task<int> AddShopAsync(CreateShopDto dto);
}