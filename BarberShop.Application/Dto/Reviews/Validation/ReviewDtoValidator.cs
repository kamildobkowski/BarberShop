using FluentValidation;

namespace BarberShop.Application.Dto.Reviews.Validation;

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

public class UpdateReviewDto : AbstractValidator<CreateReviewDto>
{
	public UpdateReviewDto()
	{
		RuleFor(r=>r.Rating)
			.GreaterThanOrEqualTo(1)
			.LessThanOrEqualTo(5)
			.NotEmpty().WithMessage("The field is required");
	}
}