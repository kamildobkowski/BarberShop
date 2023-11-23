using AutoMapper;
using BarberShop.Application.Dto.Account;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BarberShop.Application.Services.Accounts.Commands;

public record CreateShopAdminCommand(CreateShopAdminDto Dto, int? ShopId = null) : IRequest;
internal class CreateShopAdminCommandHandler : IRequestHandler<CreateShopAdminCommand>
{
	private readonly IMapper _mapper;
	private readonly IUserRepository _repository;
	private readonly IPasswordHasher<User> _hasher;

	public CreateShopAdminCommandHandler(IMapper mapper, IUserRepository repository, IPasswordHasher<User> hasher)
	{
		_mapper = mapper;
		_repository = repository;
		_hasher = hasher;
	}
	public async Task Handle(CreateShopAdminCommand request, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<User>(request.Dto);
		entity.PasswordHash = _hasher.HashPassword(entity, request.Dto.Password);
		if (request.ShopId is not null)
			entity.ShopAdmin!.ShopId = request.ShopId.Value;
		_repository.Add(entity);
		await _repository.SaveChangesAsync();
	}
}