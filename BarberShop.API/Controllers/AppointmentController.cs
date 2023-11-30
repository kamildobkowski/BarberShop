using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.API.Controllers;

[ApiController]
public class AppointmentController : ControllerBase
{
	private readonly IMediator _mediator;

	public AppointmentController(IMediator mediator)
	{
		_mediator = mediator;
	}
	
}