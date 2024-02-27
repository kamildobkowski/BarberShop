using BarberShop.Application.Interfaces;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Exceptions;
using MediatR;

namespace BarberShop.Application.Services.Shops.Commands;

public record DeleteShopCommand(int Id) : IRequest;

internal class DeleteShopCommandHandler : IRequestHandler<DeleteShopCommand>
{
	private readonly IShopRepository _repository;
	private readonly IAuthorizationService _authorizationService;

	public DeleteShopCommandHandler(IShopRepository repository, IAuthorizationService authorizationService)
	{
		_repository = repository;
		_authorizationService = authorizationService;
	}
	public async Task Handle(DeleteShopCommand request, CancellationToken cancellationToken)
	{
		_authorizationService.AuthorizeShopAdmin(request.Id);
		var entity = await _repository.GetByIdAsync(request.Id);
		if (entity is null)
			throw new NotFoundException();
		_repository.Delete(entity);
		await _repository.SaveChangesAsync();
	}
}