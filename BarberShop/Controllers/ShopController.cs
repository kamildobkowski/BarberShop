using BarberShop.Models.Dto;
using BarberShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Controllers;

[Route("api")]
[ApiController]
public class ShopController
{
	private readonly IShopService _shopService;

	public ShopController(IShopService shopService)
	{
		_shopService = shopService;
	}

	[HttpPost]
	public CreateShopDto AddShop([FromBody] CreateShopDto dto)
	{
		_shopService.AddShop(dto);
		return dto;
	}
	[HttpGet]
	public IEnumerable<GetShopDto> GetAll()
	{
		var entities = _shopService.GetAllShops();
		return entities;
	}
	[HttpGet("{id}")]
	public GetShopDto GetById([FromRoute] int id)
	{
		var entity = _shopService.GetById(id);
		return entity;
	}
}