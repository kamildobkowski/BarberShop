using BarberShop.Application.Dto.Appointments;
using BarberShop.Application.Services.Appointments.Commands;
using BarberShop.Application.Services.Appointments.Queries;
using BarberShop.Domain.Entites.Appointments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
	public async Task<ActionResult> CreateAppointment([FromRoute] int shopId, 
		[FromQuery] DateTime startDate, [FromBody] int serviceId)
	{
		var command = new CreateAppointmentCommand(shopId, startDate, serviceId);
		var id = await _mediator.Send(command);
		return Created($"api/shops/{shopId}/appointments/{id}", null);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<AppointmentDto>> GetAppointment([FromRoute] int id)
	{
		var query = new GetAppointmentQuery(id);
		var dto = await _mediator.Send(query);
		return Ok(dto);
	}
	
	
	
}