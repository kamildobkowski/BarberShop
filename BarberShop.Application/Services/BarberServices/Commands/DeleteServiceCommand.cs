using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Exceptions;
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
		var entity = await _repository.GetAsync(r=>r.Id==request.Id);
		if (entity is null)
			throw new NotFoundException();
		_repository.Delete(entity);
		await _repository.SaveChangesAsync();
	}
}