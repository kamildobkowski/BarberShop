using BarberShop.Application.Interfaces.Repositories;
using MediatR;

namespace BarberShop.Application.Services.Shops.Commands;

public record DeleteShopCommand(int Id) : IRequest;

internal class DeleteShopCommandHandler : IRequestHandler<DeleteShopCommand>
{
	private readonly IShopRepository _repository;

	public DeleteShopCommandHandler(IShopRepository repository)
	{
		_repository = repository;
	}
	public async Task Handle(DeleteShopCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetByIdAsync(request.Id);
		await _repository.DeleteAsync(entity);
	}
}