using System.Security.Claims;
using BarberShop.Application.Dto.Appointments;
using BarberShop.Application.Services.Slots;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.API.Controllers;

[Authorize(Roles = "ShopAdmin, Admin")]
[ApiController]
[Route("api/shopAdmin/schedule")]
public class TimeTableController : ControllerBase
{
	private readonly IMediator _mediator;

	public TimeTableController(IMediator mediator)
	{
		_mediator = mediator;
	}
	
	[HttpPost]
	[Authorize(Roles = "ShopAdmin")]
	public async Task<ActionResult> AddSchedule([FromBody]List<CreateSlotDto> dtos)
	{
		var shopId = User.FindFirstValue("ShopIdentifier");
		if (shopId is null)
			return StatusCode(403);
		await _mediator.Send(new AddSlotsCommand(dtos, int.Parse(shopId!)));
		return Ok();
	}
}