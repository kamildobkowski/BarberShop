using AutoMapper;
using BarberShop.Application.Dto.Appointments;
using BarberShop.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Application.Services.Slots;

public record GetSlotsQuery(int ShopId) : IRequest<List<SlotDto>>;

internal class GetSlotsQueryHandler : IRequestHandler<GetSlotsQuery, List<SlotDto>>
{
	private readonly ITimeTableRepository _repository;
	private readonly IMapper _mapper;

	public GetSlotsQueryHandler(ITimeTableRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
	public async Task<List<SlotDto>> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
	{
		var entites = await _repository.GetQueryable().Where(r => r.ShopId == request.ShopId).ToListAsync(cancellationToken: cancellationToken);
		var dtos = _mapper.Map<List<SlotDto>>(entites);
		return dtos;
	}
}