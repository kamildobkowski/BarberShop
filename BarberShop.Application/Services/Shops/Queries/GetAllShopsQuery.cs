using AutoMapper;
using BarberShop.Application.Dto;
using BarberShop.Application.Dto.Shops;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Common;
using BarberShop.Domain.Entites;
using MediatR;

namespace BarberShop.Application.Services.Shops.Queries;

public record GetAllShopsQuery(int Page = 1, int PageSize = 10, string? Filter = null, string? OrderBy = null, bool SortOrder = true ) : IRequest<PagedList<ShopDto>>;

internal class GetAllShopsQueryHandler : IRequestHandler<GetAllShopsQuery, PagedList<ShopDto>>
{
	private readonly IShopRepository _repository;
	private readonly IMapper _mapper;

	public GetAllShopsQueryHandler(IShopRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
	public async Task<PagedList<ShopDto>> Handle(GetAllShopsQuery request, CancellationToken cancellationToken)
	{
		var entites = await _repository.GetPageAsync(request.Page, request.PageSize, request.Filter, request.OrderBy, request.SortOrder);
		var dtos = new PagedList<ShopDto>(_mapper.Map<List<ShopDto>>(entites.List), entites.Page, entites.PageSize, entites.Count);
		return dtos;
	}
}