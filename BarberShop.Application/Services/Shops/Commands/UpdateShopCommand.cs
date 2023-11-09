using AutoMapper;
using BarberShop.Application.Dto.Shops;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using MediatR;

namespace BarberShop.Application.Services.Shops.Commands;

public class UpdateShopCommand : IRequest
{
	public int ShopId { get; set; }
	
	public UpdateShopDto UpdateShopDto { get; set; } = default!;
}
internal class UpdateShopCommandHandler : IRequestHandler<UpdateShopCommand>
{
	private readonly IShopRepository _shopRepository;
	private readonly IMapper _mapper;

	public UpdateShopCommandHandler(IShopRepository shopRepository, IMapper mapper)
	{
		_shopRepository = shopRepository;
		_mapper = mapper;
	}
	public async Task Handle(UpdateShopCommand request, CancellationToken cancellationToken)
	{
		var entity = await _shopRepository.GetByIdAsync(request.ShopId);
		_mapper.Map(request.UpdateShopDto, entity);
		await _shopRepository.SaveChangesAsync();
	}
}
