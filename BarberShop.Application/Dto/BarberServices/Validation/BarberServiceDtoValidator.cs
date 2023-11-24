using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using FluentValidation;

namespace BarberShop.Application.Dto.BarberServices.Validation;

public class CreateBarberServiceDtoValidator : AbstractValidator<BarberService>
{
	public CreateBarberServiceDtoValidator(IShopRepository repository)
	{
		RuleFor(r => r.Name)
			.NotEmpty().WithMessage("The field is required")
			.MinimumLength(2)
			.MaximumLength(20);
		RuleFor(r => r.ShopId)
			.NotEmpty().WithMessage("The field is required")
			.Custom((value, context) =>
			{
				var entity = repository.GetAsync(r=>r.Id==value).Result;
				context.AddFailure("Shop with given Id does not exist");
			});
		RuleFor(r => r.Duration)
			.NotEmpty().WithMessage("The field is required")
			.GreaterThanOrEqualTo(TimeSpan.FromMinutes(5))
			.WithMessage("Duration must be greater than 5 minutes");
		RuleFor(r => r.Price)
			.NotEmpty().WithMessage("The field is required")
			.PrecisionScale(10, 2, true)
			.GreaterThan(0m);
	}
}