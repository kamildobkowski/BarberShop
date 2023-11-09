namespace BarberShop.Application.Dto.Reviews;

public record UpdateReviewDto
{
	public string? Description { get; set; }
	public int? Rating { get; set; }
}