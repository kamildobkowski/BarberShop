using BarberShop.Application.Dto.Reviews;
using BarberShop.Application.Services.Reviews.Commands;
using BarberShop.Application.Services.Reviews.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.API.Controllers;

[Route("api/{shopId}/reviews")]
[ApiController]
public class ReviewController : ControllerBase
{
	private readonly IMediator _mediator;

	public ReviewController(IMediator mediator)
	{
		_mediator = mediator;
	}
	
	[HttpGet]
	public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviews([FromRoute] int shopId)
	{
		var result = await _mediator.Send(new GetAllReviewsQuery(shopId));
		return Ok(result);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ReviewDto>> GetReview([FromRoute] int shopId, [FromRoute] int id)
	{
		var result = await _mediator.Send(new GetReviewQuery(shopId, id));
		return Ok(result);
	}

	[HttpPost]
	public async Task<ActionResult> AddReview([FromRoute] int shopId, [FromBody] CreateReviewDto dto)
	{
		var id = await _mediator.Send(new CreateReviewCommand(shopId, dto));
		return Created("api/{shopId}/reviews/{id}", null);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete([FromRoute] int shopId, [FromRoute] int id)
	{
		await _mediator.Send(new DeleteReviewCommand(shopId, id));
		return NoContent();
	}
	
}