using BarberShop.Application.Dto.BarberServices;
using BarberShop.Application.Services.BarberServices.Commands;
using BarberShop.Application.Services.BarberServices.Queries;
using BarberShop.Application.Services.Shops.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.API.Controllers;

[ApiController, Route("api/{shopId}/services")]
public class ServiceController : ControllerBase
{
	private readonly IMediator _mediator;

	public ServiceController(IMediator mediator)
	{
		_mediator = mediator;
	}
	[HttpGet]
	public async Task<ActionResult<IEnumerable<BarberServiceDto>>> GetAll([FromRoute]int shopId)
	{
		var result = await _mediator.Send(new GetAllServicesQuery(shopId));
		return Ok(result);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<BarberServiceDto>> GetById([FromRoute] int shopId, [FromRoute] int id)
	{
		var result = await _mediator.Send(new GetServiceQuery(shopId, id));
		return Ok(result);
	}

	[HttpPost]
	public async Task<ActionResult> Add([FromRoute] int shopId, [FromBody] CreateBarberServiceDto dto)
	{
		var id = await _mediator.Send(new CreateServiceCommand(shopId, dto));
		return Created("/api/{shopId}/services/{id}", null);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete([FromRoute] int shopId, [FromRoute] int id)
	{
		await _mediator.Send(new DeleteServiceCommand(shopId, id));
		return NoContent();
	}
}