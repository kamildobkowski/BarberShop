using System.Security.Claims;
using BarberShop.Application.Dto.Appointments;
using BarberShop.Application.Services.Appointments.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BarberShop.API.Controllers;

[ApiController, Authorize]
[Route("api/shops/{shopId}/appointments")]
public class AppointmentController : ControllerBase
{
	private readonly IMediator _mediator;

	public AppointmentController(IMediator mediator)
	{
		_mediator = mediator;
	}
	
	[HttpPost]
	[Authorize(Roles="Customer")]
	public async Task<ActionResult> CreateAppointment([BindRequired, FromRoute] int shopId, 
		[BindRequired, FromQuery] DateTime startDate, 
		[BindRequired, FromQuery] int serviceId)
	{
		var userId = int.Parse(User.FindFirst(r => r.Type == ClaimTypes.NameIdentifier)!.Value);
		await _mediator.Send(new CreateAppointmentCommand(shopId, userId, startDate, serviceId));
		return Ok();
	}
	
}