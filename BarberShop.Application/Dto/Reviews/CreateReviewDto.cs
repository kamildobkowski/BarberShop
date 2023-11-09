using System.ComponentModel.DataAnnotations;

namespace BarberShop.Application.Dto.Reviews;

public record CreateReviewDto
{
	public string? Description { get; set; }
	[Range(1,5)]
	public int Rating { get; set; }
}