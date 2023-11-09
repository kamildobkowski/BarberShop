using BarberShop.Application.Interfaces.Repositories;
using MediatR;

namespace BarberShop.Application.Services.BarberServices.Commands;

public record DeleteServiceCommand(int ShopId, int Id) : IRequest;

internal class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand>
{
	private readonly IBarberServiceRepository _repository;

	public DeleteServiceCommandHandler(IBarberServiceRepository repository)
	{
		_repository = repository;
	}
	public async Task Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetByIdAsync(request.ShopId, request.Id);
		await _repository.DeleteAsync(entity);
	}
}