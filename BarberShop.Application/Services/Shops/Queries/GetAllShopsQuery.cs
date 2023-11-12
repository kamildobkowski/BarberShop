using AutoMapper;
using BarberShop.Application.Dto;
using BarberShop.Application.Dto.Shops;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using MediatR;

namespace BarberShop.Application.Services.Shops.Queries;

public record GetAllShopsQuery : IRequest<IEnumerable<ShopDto>>;

internal class GetAllShopsQueryHandler : IRequestHandler<GetAllShopsQuery, IEnumerable<ShopDto>>
{
	private readonly IShopRepository _repository;
	private readonly IMapper _mapper;

	public GetAllShopsQueryHandler(IShopRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
	public async Task<IEnumerable<ShopDto>> Handle(GetAllShopsQuery request, CancellationToken cancellationToken)
	{
		var entites = await _repository.GetAllAsync();
		var dtos = _mapper.Map<IEnumerable<ShopDto>>(entites);
		return dtos;
	}
}