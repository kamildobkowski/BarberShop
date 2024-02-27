using FluentValidation;

namespace BarberShop.Application.Dto.Account;

public record LoginDto (string Email, string Password);

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