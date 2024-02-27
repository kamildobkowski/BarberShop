using FluentValidation;

namespace BarberShop.Application.Dto.Shops;

public class UpdateShopAddressDto
{
	public string? Street { get; set; } = default!;
	public int? Number { get; set; }
	public int? ApartamentNumber { get; set; }
	public string? City { get; set; } = default!;
	public string? PostalCode { get; set; }
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