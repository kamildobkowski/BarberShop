using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using MediatR;

namespace BarberShop.Application.Services.Shops.Queries;

public record GetShopQuery(int ShopId) : IRequest<Shop?>;

internal class GetShopQueryHandler : IRequestHandler<GetShopQuery, Shop?>
{
	private readonly IShopRepository _shopRepository;

	public GetShopQueryHandler(IShopRepository shopRepository)
	{
		_shopRepository = shopRepository;
	}
	public async Task<Shop?> Handle(GetShopQuery request, CancellationToken cancellationToken)
	{
		return await _shopRepository.GetByIdAsync(request.ShopId);
	}
}