using BarberShop.Entities.BarberShop;
using BarberShop.Models.Dto;
using BarberShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Controllers;

[Route($"api/{{shopId}}/services")]
[Controller]
public class ServicesController : ControllerBase
{
	private readonly IServicesService _service;

	public ServicesController(IServicesService service)
	{
		_service = service;
	}

	[HttpGet]
	public IEnumerable<Service>? GetAll([FromRoute] int shopId)
	{
		var entities = _service.GetAll(shopId);
		return entities;
	}

	[HttpGet("{id}")]
	public GetServiceDto Get([FromRoute] int shopId, [FromRoute] int id)
	{
		var entity = _service.GetById(shopId, id);
		return entity;
	}

	[HttpPost]
	public void Add([FromRoute] int shopId, [FromBody] CreateServiceDto dto)
	{
		_service.Add(shopId, dto);
	}
}