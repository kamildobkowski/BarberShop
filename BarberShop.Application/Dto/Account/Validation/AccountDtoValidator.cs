using FluentValidation;

namespace BarberShop.Application.Dto.Account.Validation;

public class CreateCustomerDtoValidator : AbstractValidator<CreateCustomerDto>
{
	public CreateCustomerDtoValidator()
	{
		RuleFor(r => r.Email)
			.EmailAddress().WithMessage("Invalid email address")
			.NotEmpty().WithMessage("The field is required");
		RuleFor(r => r.Password)
			.MinimumLength(8).WithMessage("Your password must have minimal length of 8 characters")
			.Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
			.Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
			.Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
			.NotEmpty().WithMessage("The field is required");
		RuleFor(r => r.PhoneNumber.Trim())
			.NotEmpty().WithMessage("The field is required")
			.MinimumLength(8);
		RuleFor(r => r.ConfirmPassword)
			.Equal(r => r.Password).WithMessage("Passwords do not match");
	}
}

public class CreateShopAdminDtoValidator : AbstractValidator<CreateShopAdminDto>
{
	public CreateShopAdminDtoValidator()
	{
		RuleFor(r => r.Email)
			.EmailAddress().WithMessage("Invalid email address")
			.NotEmpty().WithMessage("The field is required");
		RuleFor(r => r.Password)
			.MinimumLength(8).WithMessage("Your password must have minimal length of 8 characters")
			.Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
			.Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
			.Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
			.NotEmpty().WithMessage("The field is required");
		RuleFor(r => r.ConfirmPassword)
			.Equal(r => r.Password).WithMessage("Passwords do not match");
	}
}

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
	public LoginDtoValidator()
	{
		RuleFor(r => r.Email)
			.NotEmpty().WithMessage("The field is required")
			.EmailAddress();
		RuleFor(r => r.Password)
			.NotEmpty().WithMessage("The field is required");
	}
}