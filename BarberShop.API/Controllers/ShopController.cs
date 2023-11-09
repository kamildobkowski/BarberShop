using BarberShop.Application.Dto.Shops;
using BarberShop.Application.Services.Shops.Commands;
using BarberShop.Application.Services.Shops.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.API.Controllers;

[ApiController]
[Route("api/shops")]
public class ShopController : ControllerBase
{
	private readonly IMediator _mediator;

	public ShopController(IMediator mediator)
	{
		_mediator = mediator;
	}
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var entites = await _mediator.Send(new GetAllShopsQuery());
		return Ok(entites);
	}

	[HttpPost]
	public async Task Add(CreateShopDto dto)
	{
		await _mediator.Send(new CreateShopCommand()
		{
			CreateShopDto = dto
		});
	}

	[HttpPut("{shopId}")]
	public async Task Update([FromRoute]int shopId, UpdateShopDto dto)
	{
		await _mediator.Send(new UpdateShopCommand
		{
			ShopId = shopId,
			UpdateShopDto = dto
		});
	}

	[HttpPut("{shopId}/address")]
	public async Task UpdateAddress([FromRoute] int shopId, UpdateShopAddressDto dto)
	{
		await _mediator.Send(new UpdateShopAddressCommand
		{
			ShopId = shopId,
			UpdateShopAddressDto = dto
		});
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete([FromRoute] int id)
	{
		await _mediator.Send(new DeleteShopCommand(id));
		return NoContent();
	}
}