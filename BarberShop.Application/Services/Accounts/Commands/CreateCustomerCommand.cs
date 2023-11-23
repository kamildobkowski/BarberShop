using AutoMapper;
using BarberShop.Application.Dto.Account;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BarberShop.Application.Services.Accounts.Commands;

public record CreateCustomerCommand(CreateCustomerDto Dto) : IRequest;

internal class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
{
	private readonly IUserRepository _repository;
	private readonly IPasswordHasher<User> _hasher;
	private readonly IMapper _mapper;

	public CreateCustomerCommandHandler(IUserRepository repository, IPasswordHasher<User> hasher, IMapper mapper)
	{
		_repository = repository;
		_hasher = hasher;
		_mapper = mapper;
	}
	public async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<User>(request.Dto);
		entity.PasswordHash= _hasher.HashPassword(entity, request.Dto.Password);
		_repository.Add(entity);
		await _repository.SaveChangesAsync();
	}
}