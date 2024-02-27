using BarberShop.Application.Interfaces;
using BarberShop.Application.Interfaces.Repositories;
using FluentValidation;

namespace BarberShop.Application.Dto.Shops;

public class UpdateShopDto
{
	public string? Name { get; set; }
}

public class UpdateShopDtoValidator : AbstractValidator<UpdateShopDto>
{
	public UpdateShopDtoValidator(IShopRepository repository, IUserContextService userContextService)
	{
		RuleFor(r => r.Name)
			.Custom((value, context) =>
			{
				if (value is null)
					return;
				var entity = repository.GetAsync(r =>
					r.Name.ToLower().TrimStart().TrimEnd().Equals(value.ToLower().TrimStart().TrimEnd())).Result;
				if (entity is not null)
					context.AddFailure("Shop with given name already exists");
			});
	}
}