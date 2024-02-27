using BarberShop.Application.Dto.Account;
using BarberShop.Application.Services.Accounts.Commands;
using BarberShop.Application.Services.Accounts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.API.Controllers;

[ApiController]
[Route("api")]
public class AccountController : ControllerBase
{
	private readonly IMediator _mediator;

	public AccountController(IMediator mediator)
	{
		_mediator = mediator;
	}
	
	[HttpPost("register")]
	public async Task<ActionResult> RegisterCustomer([FromBody] CreateCustomerDto dto)
	{
		await _mediator.Send(new CreateCustomerCommand(dto));
		return Ok();
	}

	[HttpPost("register/shopadmin")]
	public async Task<ActionResult> RegisterShopAdmin([FromBody] CreateShopAdminDto dto)
	{
		await _mediator.Send(new CreateShopAdminCommand(dto));
		return Ok();
	}

	[HttpPost("register/admin")]
	public async Task<ActionResult> RegisterAdmin(CreateAdminDto dto)
	{
		await _mediator.Send(new CreateAdminCommand(dto));
		return Ok();
	}
	
	[HttpPost("login")]
	public async Task<ActionResult<String>> Login([FromBody] LoginDto dto)
	{
		var token = await _mediator.Send(new VerifyUserLoginQuery(dto));
		return Ok(token);
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> AddShopToShopAdmin([FromBody] AddShopAdminToShopDto dto)
	{
		await _mediator.Send(new AddShopIdToShopAdminCommand(dto.Email, dto.ShopId));
		return Ok();
	}
}