using AutoMapper;
using BarberShop.Application.Dto.Slots;
using BarberShop.Application.Interfaces;
using BarberShop.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Application.Services.Slots.Queries;

public record GetSlotsQuery(int ShopId) : IRequest<List<SlotDto>>;

internal class GetSlotsQueryHandler : IRequestHandler<GetSlotsQuery, List<SlotDto>>
{
	private readonly ITimeTableRepository _repository;
	private readonly IMapper _mapper;
	private readonly IAuthorizationService _authorizationService;

	public GetSlotsQueryHandler(ITimeTableRepository repository, IMapper mapper, IAuthorizationService authorizationService)
	{
		_repository = repository;
		_mapper = mapper;
		_authorizationService = authorizationService;
	}
	public async Task<List<SlotDto>> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
	{
		_authorizationService.AuthorizeShopAdmin(request.ShopId);
		var entites = await _repository.GetQueryable().Where(r => r.ShopId == request.ShopId).ToListAsync(cancellationToken: cancellationToken);
		var dtos = _mapper.Map<List<SlotDto>>(entites);
		return dtos;
	}
}