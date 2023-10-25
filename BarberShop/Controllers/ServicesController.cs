using BarberShop.Entities.BarberShop;
using BarberShop.Models.Dto;
using BarberShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Controllers;

[Route($"api/{{shopId}}/service")]
[ApiController]
public class ServicesController : ControllerBase
{
	private readonly IServicesService _service;

	public ServicesController(IServicesService service)
	{
		_service = service;
	}

	[HttpGet]
	public ActionResult<IEnumerable<GetServiceDto>?> GetAll([FromRoute] int shopId)
	{
		var entities = _service.GetAll(shopId);
		return Ok(entities);
	}

	[HttpGet("{id}")]
	public ActionResult<GetServiceDto> Get([FromRoute] int shopId, [FromRoute] int id)
	{
		var entity = _service.GetById(shopId, id);
		return Ok(entity);
	}

	[HttpPost]
	public ActionResult Add([FromRoute] int shopId, [FromBody] CreateServiceDto dto)
	{
		var entityId = _service.Add(shopId, dto);
		return Created($"/api/{shopId}/service/{entityId}", null);
	}
}