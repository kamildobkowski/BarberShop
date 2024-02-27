using FluentValidation;

namespace BarberShop.Application.Dto.Reviews;

public record UpdateReviewDto
{
	public string? Description { get; set; }
	public int? Rating { get; set; }
}

public class UpdateReviewDtoValidator : AbstractValidator<CreateReviewDto>
{
	public UpdateReviewDtoValidator()
	{
		RuleFor(r=>r.Rating)
			.GreaterThanOrEqualTo(1)
			.LessThanOrEqualTo(5)
			.NotEmpty().WithMessage("The field is required");
	}
}