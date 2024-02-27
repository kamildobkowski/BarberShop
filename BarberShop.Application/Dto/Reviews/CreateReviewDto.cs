using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace BarberShop.Application.Dto.Reviews;

public record CreateReviewDto
{
	public string? Description { get; set; }
	[Range(1,5)]
	public int Rating { get; set; }
}

public class CreateReviewDtoValidator : AbstractValidator<CreateReviewDto>
{
	public CreateReviewDtoValidator()
	{
		RuleFor(r => r.Rating)
			.GreaterThanOrEqualTo(1)
			.LessThanOrEqualTo(5)
			.NotEmpty().WithMessage("The field is required");
	}
}