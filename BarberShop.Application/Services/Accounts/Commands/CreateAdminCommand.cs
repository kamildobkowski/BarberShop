using AutoMapper;
using BarberShop.Application.Dto.Account;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BarberShop.Application.Services.Accounts.Commands;

public record CreateAdminCommand(CreateAdminDto Dto) :IRequest;

internal class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand>
{
	private readonly IUserRepository _repository;
	private readonly IPasswordHasher<User> _hasher;
	private readonly IMapper _mapper;

	public CreateAdminCommandHandler(IUserRepository repository, IPasswordHasher<User> hasher, IMapper mapper)
	{
		_repository = repository;
		_hasher = hasher;
		_mapper = mapper;
	}
	public async Task Handle(CreateAdminCommand request, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<User>(request.Dto);
		entity.PasswordHash = _hasher.HashPassword(entity, request.Dto.Password);
		_repository.Add(entity);
		await _repository.SaveChangesAsync();
	}
} 