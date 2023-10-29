using BarberShop.Models.Dto;
using BarberShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Controllers;

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
	public ActionResult Register(CreateUserDto dto)
	{
		_accountService.Register(dto);
		return Ok();
	}
	
	
}