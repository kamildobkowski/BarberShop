using AutoMapper;
using BarberShop.Application.Dto.Shops;
using BarberShop.Application.Interfaces;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Exceptions;
using MediatR;

namespace BarberShop.Application.Services.Shops.Commands;

public record UpdateShopCommand : IRequest
{
	public int ShopId { get; init; }
	
	public UpdateShopDto UpdateShopDto { get; init; } = default!;
}
internal class UpdateShopCommandHandler : IRequestHandler<UpdateShopCommand>
{
	private readonly IShopRepository _shopRepository;
	private readonly IMapper _mapper;
	private readonly IAuthorizationService _authorizationService;

	public UpdateShopCommandHandler(IShopRepository shopRepository, IMapper mapper, IAuthorizationService authorizationService)
	{
		_shopRepository = shopRepository;
		_mapper = mapper;
		_authorizationService = authorizationService;
	}
	public async Task Handle(UpdateShopCommand request, CancellationToken cancellationToken)
	{
		_authorizationService.AuthorizeShopAdmin(request.ShopId);
		var entity = await _shopRepository.GetByIdAsync(request.ShopId);
		if (entity is null)
			throw new NotFoundException();
		_mapper.Map(request.UpdateShopDto, entity);
		await _shopRepository.SaveChangesAsync();
	}
}
