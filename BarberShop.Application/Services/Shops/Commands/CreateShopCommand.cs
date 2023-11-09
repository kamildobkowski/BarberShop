using AutoMapper;
using BarberShop.Application.Dto.Shops;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using MediatR;

namespace BarberShop.Application.Services.Shops.Commands;

public class CreateShopCommand : IRequest
{
	public CreateShopDto CreateShopDto { get; set; } = default!;
}

public class CreateShopCommandHandler : IRequestHandler<CreateShopCommand>
{
	private readonly IShopRepository _shopRepository;
	private readonly IMapper _mapper;

	public CreateShopCommandHandler(IShopRepository shopRepository, IMapper mapper)
	{
		_shopRepository = shopRepository;
		_mapper = mapper;
	}
	public async Task Handle(CreateShopCommand request, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<Shop>(request.CreateShopDto);
		await _shopRepository.AddAsync(entity);
	}
}