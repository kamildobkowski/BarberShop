using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using MediatR;

namespace BarberShop.Application.Services.Shops.Commands;

public class CreateShopCommand : IRequest
{
	public Shop Shop { get; set; } = default!;
}

public class CreateShopCommandHandler : IRequestHandler<CreateShopCommand>
{
	private readonly IShopRepository _shopRepository;

	public CreateShopCommandHandler(IShopRepository shopRepository)
	{
		_shopRepository = shopRepository;
	}
	public async Task Handle(CreateShopCommand request, CancellationToken cancellationToken)
	{
		await _shopRepository.AddAsync(request.Shop);
	}
}