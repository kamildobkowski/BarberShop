using BarberShop.Application.Interfaces.Repositories;
using MediatR;

namespace BarberShop.Application.Services.BarberServices.Commands;

public record UpdateServicePriceCommand(int ShopId, int Id, decimal NewPrice) : IRequest;

internal class UpdateServicePriceCommandHandler : IRequestHandler<UpdateServicePriceCommand>
{
	private readonly IBarberServiceRepository _repository;

	public UpdateServicePriceCommandHandler(IBarberServiceRepository repository)
	{
		_repository = repository;
	}
	public async Task Handle(UpdateServicePriceCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetByIdAsync(request.ShopId, request.Id);
		entity.Price = request.NewPrice;
		entity.Updated = DateTime.UtcNow;
		await _repository.SaveChangesAsync();
	}
}
