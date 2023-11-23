using BarberShop.Application.Dto.Account;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites.Users;
using BarberShop.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BarberShop.Application.Services.Accounts.Queries;

public record VerifyUserLoginQuery(LoginDto Dto) : IRequest<User>;

internal class VerifyUserLoginQueryHandler : IRequestHandler<VerifyUserLoginQuery, User>
{
	private readonly IUserRepository _userRepository;
	private readonly IPasswordHasher<User> _hasher;

	public VerifyUserLoginQueryHandler(IUserRepository userRepository, IPasswordHasher<User> hasher)
	{
		_userRepository = userRepository;
		_hasher = hasher;
	}
	public async Task<User> Handle(VerifyUserLoginQuery request, CancellationToken cancellationToken)
	{
		var entity = await _userRepository
			.GetAsync(r => r.Email == request.Dto.Email);
		if (entity is null)
			throw new WrongCredentialsException("Incorrect email and/or password");
		var result = _hasher
			.VerifyHashedPassword(entity, entity.PasswordHash, request.Dto.Password);
		if (result == PasswordVerificationResult.Failed)
			throw new WrongCredentialsException("Incorrect email and/or password");
		return entity;
	}
}
