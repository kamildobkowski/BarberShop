using BarberShop.Accounts.Models.Dto;
using BarberShop.Accounts.Services.Commands;
using BarberShop.Accounts.Services.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Accounts.Controllers;

[Route("account")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly ICreateAccountService _createAccountService;
	private readonly ILoginService _loginService;

	public AccountController(ICreateAccountService createAccountService, ILoginService loginService)
	{
		_createAccountService = createAccountService;
		_loginService = loginService;
	}

	[HttpGet("login")]
	public ActionResult<string> Login(LoginDto dto)
	{
		var token = _loginService.GenerateJwt(dto);
		return Ok(token);
	}

	[HttpPost("register")]
	public ActionResult Register(CreateCustomerDto dto)
	{
		_createAccountService.Register(dto);
		return Ok();
	}
	
	
}