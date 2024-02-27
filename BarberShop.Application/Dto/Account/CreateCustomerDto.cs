using BarberShop.Domain.Entites.Users;
using FluentValidation;

namespace BarberShop.Application.Dto.Account;

public record CreateCustomerDto
{
	public string Email { get; set; } = default!;
	public string Password { get; set; } = default!;
	public string ConfirmPassword { get; set; } = default!;
	public string FirstName { get; set; } = default!;
	public string Surname { get; set; } = default!;
	public string? Nationality { get; set; }
	public string PhoneNumber { get; set; } = default!;
}

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
