using AutoMapper;
using BarberShop.Application.Dto.Shops;
using BarberShop.Application.Interfaces;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using MediatR;

namespace BarberShop.Application.Services.Shops.Commands;

public class CreateShopCommand : IRequest<int>
{
	public CreateShopDto CreateShopDto { get; set; } = default!;
}

public class CreateShopCommandHandler : IRequestHandler<CreateShopCommand, int>
{
	private readonly IShopRepository _shopRepository;
	private readonly IMapper _mapper;
	private readonly IUserContextService _userContextService;
	private readonly IAuthorizationService _authorizationService;

	public CreateShopCommandHandler(IShopRepository shopRepository, IMapper mapper, IUserContextService userContextService, IAuthorizationService authorizationService)
	{
		_shopRepository = shopRepository;
		_mapper = mapper;
		_userContextService = userContextService;
		_authorizationService = authorizationService;
	}
	public async Task<int> Handle(CreateShopCommand request, CancellationToken cancellationToken)
	{
		_authorizationService.AuthorizeShopAdmin(null);
		var entity = _mapper.Map<Shop>(request.CreateShopDto);
		entity.CreatedById = (int)_userContextService.UserId!;
		_shopRepository.Add(entity);
		await _shopRepository.SaveChangesAsync();
		return entity.Id;
	}
}