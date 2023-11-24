using BarberShop.Application.Interfaces.Repositories;
using FluentValidation;

namespace BarberShop.Application.Dto.Shops.Validation;

public class CreateShopDtoValidator : AbstractValidator<CreateShopDto>
{
	public CreateShopDtoValidator(IShopRepository repository)
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
					where Char.IsDigit(i)
					select i).ToString();
				if (code == null || code.Length != 5)
					context.AddFailure("Invalid postal code");
			});
	}
}

public class UpdateShopAddressDtoValidator : AbstractValidator<UpdateShopAddressDto>
{
	public UpdateShopAddressDtoValidator()
	{
		RuleFor(r => r.PostalCode)
			.Custom((value, context) =>
			{
				if (value is null)
					return;
				var code = (from i in value
					where Char.IsDigit(i)
					select i).ToString();
				if (code == null || code.Length != 5)
					context.AddFailure("Invalid postal code");
			});
	}
}

public class UpdateShopDtoValidator : AbstractValidator<UpdateShopDto>
{
	public UpdateShopDtoValidator(IShopRepository repository)
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