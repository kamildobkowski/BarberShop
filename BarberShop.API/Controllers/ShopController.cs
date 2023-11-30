using System.Security.Claims;
using BarberShop.Application.Dto.Shops;
using BarberShop.Application.Services.Accounts.Commands;
using BarberShop.Application.Services.Shops.Commands;
using BarberShop.Application.Services.Shops.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById([FromRoute] int id)
	{
		var entity = await _mediator.Send(new GetShopQuery(id));
		return Ok(entity);
	}

	[Authorize(Roles="ShopAdmin, Admin")]
	[HttpPost]
	public async Task Add(CreateShopDto dto)
	{
		var shopId = await _mediator.Send(new CreateShopCommand
		{
			CreateShopDto = dto
		});
		if (User.Claims.First(c => c.Type == ClaimTypes.Role).ToString() == "ShopAdmin")
		{
			string email = User.Claims.First(c => c.Type == ClaimTypes.Email).ToString();
			try
			{
				var id = int.Parse(User.Claims.First(c => c.Type == "ShopIdentifier").ToString());
			}
			catch (InvalidOperationException)
			{
				await _mediator.Send(new AddShopIdToShopAdminCommand(email, shopId));
			}
			
			
		}
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