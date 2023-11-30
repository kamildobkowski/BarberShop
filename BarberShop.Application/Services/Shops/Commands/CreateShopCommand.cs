using AutoMapper;
using BarberShop.Application.Dto.Shops;
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

	public CreateShopCommandHandler(IShopRepository shopRepository, IMapper mapper)
	{
		_shopRepository = shopRepository;
		_mapper = mapper;
	}
	public async Task<int> Handle(CreateShopCommand request, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<Shop>(request.CreateShopDto);
		_shopRepository.Add(entity);
		await _shopRepository.SaveChangesAsync();
		return entity.Id;
	}
}