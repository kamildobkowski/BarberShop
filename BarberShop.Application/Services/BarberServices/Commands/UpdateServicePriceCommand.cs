using BarberShop.Application.Interfaces;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Exceptions;
using MediatR;

namespace BarberShop.Application.Services.BarberServices.Commands;

public record UpdateServicePriceCommand(int ShopId, int Id, decimal NewPrice) : IRequest;

internal class UpdateServicePriceCommandHandler : IRequestHandler<UpdateServicePriceCommand>
{
	private readonly IBarberServiceRepository _repository;
	private readonly IAuthorizationService _authorizationService;

	public UpdateServicePriceCommandHandler(IBarberServiceRepository repository, IAuthorizationService authorizationService)
	{
		_repository = repository;
		_authorizationService = authorizationService;
	}
	public async Task Handle(UpdateServicePriceCommand request, CancellationToken cancellationToken)
	{
		_authorizationService.AuthorizeShopAdmin(request.ShopId);
		var entity = await _repository
			.GetAsync(r=>r.ShopId==request.ShopId && r.Id==request.Id);
		if (entity is null)
			throw new NotFoundException();
		entity.Price = request.NewPrice;
		entity.Updated = DateTime.UtcNow;
		await _repository.SaveChangesAsync();
	}
}
