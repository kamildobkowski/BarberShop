using BarberShop.Models.Dto;
using BarberShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Controllers;

[Route("/api/shop/{shopId}/reviews")]
[ApiController]
public class ReviewController : ControllerBase
{
	private readonly IReviewService _service;

	public ReviewController(IReviewService service)
	{
		_service = service;
	}
	[HttpGet]
	public ActionResult<IEnumerable<GetReviewDto>> GetAll([FromRoute] int shopId)
	{
		var entities = _service.GetAll(shopId);
		return Ok(entities);
	}

	[HttpPost("{id}")]
	public ActionResult<GetReviewDto> Get([FromRoute] int shopId, [FromRoute] int id)
	{
		var entity = _service.GetById(shopId, id);
		return Ok(entity);
	}

	[HttpPost]
	public ActionResult Add([FromRoute] int shopId, [FromBody] CreateReviewDto dto)
	{
		var id = _service.AddReview(shopId, dto);
		return Created($"/api/shop/{shopId}/reviews/{id}", null);
	}
}