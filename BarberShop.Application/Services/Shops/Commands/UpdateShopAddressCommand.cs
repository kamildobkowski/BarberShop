using AutoMapper;
using BarberShop.Application.Dto.Shops;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Exceptions;
using BarberShop.Domain.ValueObjects;
using MediatR;

namespace BarberShop.Application.Services.Shops.Commands;

public record UpdateShopAddressCommand : IRequest
{
	public int ShopId { get; set; }
	public UpdateShopAddressDto UpdateShopAddressDto { get; set; } = default!;
}

internal class UpdateShopAddressCommandHandler : IRequestHandler<UpdateShopAddressCommand>
{
	private readonly IShopRepository _shopRepository;
	private readonly IMapper _mapper;

	public UpdateShopAddressCommandHandler(IShopRepository shopRepository, IMapper mapper)
	{
		_shopRepository = shopRepository;
		_mapper = mapper;
	}
	public async Task Handle(UpdateShopAddressCommand request, CancellationToken cancellationToken)
	{
		var updatedAddress = _mapper.Map<Address>(request.UpdateShopAddressDto);
		var entity = await _shopRepository.GetAsync(r => r.Id == request.ShopId);
		if (entity is null)
			throw new NotFoundException();
		_mapper.Map(updatedAddress, entity);
		await _shopRepository.SaveChangesAsync();
	}
}