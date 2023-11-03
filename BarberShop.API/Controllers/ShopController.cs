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
	public async Task Add(BarberShop.Domain.Entites.Shop shop)
	{
		await _mediator.Send(new CreateShopCommand()
		{
			Shop = shop
		});
	}

}