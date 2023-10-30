using BarberShop.Accounts.Models.Dto;
using BarberShop.Accounts.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Accounts.Controllers;

[Route("account")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly IAccountService _accountService;

	public AccountController(IAccountService accountService)
	{
		_accountService = accountService;
	}

	[HttpGet("login")]
	public ActionResult<string> Login(LoginDto dto)
	{
		var token = _accountService.GenerateJwt(dto);
		return Ok(token);
	}

	[HttpPost("register")]
	public ActionResult Register(CreateCustomerDto dto)
	{
		_accountService.Register(dto);
		return Ok();
	}
	
	
}