using BarberShop.Application.Interfaces;
using BarberShop.Application.Interfaces.Repositories;
using FluentValidation;

namespace BarberShop.Application.Dto.Shops;

public class CreateShopDto
{
	public string Name { get; set; } = default!;
	public string Street { get; set; } = default!;
	public int Number { get; set; }
	public int? ApartamentNumber { get; set; }
	public string City { get; set; } = default!;
	public string PostalCode { get; set; } = default!;
}

public class CreateShopDtoValidator : AbstractValidator<CreateShopDto>
{
	public CreateShopDtoValidator(IShopRepository repository, IUserContextService userContextService)
	{
		RuleFor(r => r.Name)
			.NotEmpty().WithMessage("The field is required")
			.Custom((value, context) =>
			{
				var entity = repository.GetAsync(r =>
					r.Name.ToLower().TrimStart().TrimEnd().Equals(value.ToLower().TrimStart().TrimEnd())).Result;
				if (entity is not null)
					context.AddFailure("Shop with given name already exists");
			});
		RuleFor(r => r.PostalCode)
			.NotEmpty().WithMessage("The field is required")
			.Custom((value, context) =>
			{
				var code = (from i in value
					where char.IsDigit(i)
					select i).ToString();
				if (code is not { Length: 5 })
					context.AddFailure("Invalid postal code");
			});
	}
}