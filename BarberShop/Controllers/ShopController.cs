using BarberShop.Entities.BarberShop;
using BarberShop.Models.Dto;
using BarberShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Controllers;

[Route("api")]
public class ShopController
{
	private readonly IShopService _shopService;

	public ShopController(IShopService shopService)
	{
		_shopService = shopService;
	}
	[HttpPost]
	public Task<int> AddShop([FromBody]CreateShopDto dto)
	{
		var entityId = _shopService.AddShop(dto);
		return entityId;
	}
}