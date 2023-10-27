using BarberShop.Models.Dto;
using BarberShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Controllers;

[Route("api/shop")]
[ApiController]
public class ShopController : ControllerBase
{
	private readonly IShopService _shopService;

	public ShopController(IShopService shopService)
	{
		_shopService = shopService;
	}

	[HttpPost]
	public ActionResult AddShop([FromBody] CreateShopDto dto)
	{
		var id = _shopService.AddShop(dto).GetAwaiter().GetResult();
		return Created($"/api/shop/{id}", null);
	}
	[HttpGet]
	public ActionResult<IEnumerable<GetShopDto>> GetAll()
	{
		var entities = _shopService.GetAllShops();
		return Ok(entities);
	}
	[HttpGet("{id}")]
	public ActionResult<GetShopDto> GetById([FromRoute] int id)
	{
		var entity = _shopService.GetById(id);
		return Ok(entity);
	}

	[HttpDelete("{id}")]
	public ActionResult Delete([FromRoute] int id)
	{
		_shopService.DeleteShop(id);
		return NoContent();
	}

	[HttpPut("{id}")]
	public ActionResult Update([FromRoute] int id, [FromBody] CreateShopDto dto)
	{
		_shopService.Update(id, dto);
		return Ok();
	}
}