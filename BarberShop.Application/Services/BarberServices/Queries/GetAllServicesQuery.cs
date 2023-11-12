using AutoMapper;
using BarberShop.Application.Dto.BarberServices;
using BarberShop.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Application.Services.BarberServices.Queries;

public record GetAllServicesQuery(int ShopId) : IRequest<IEnumerable<BarberServiceDto>>;

internal class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, IEnumerable<BarberServiceDto>>
{
	private readonly IBarberServiceRepository _repository;
	private readonly IMapper _mapper;

	public GetAllServicesQueryHandler(IBarberServiceRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
	public async Task<IEnumerable<BarberServiceDto>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
	{
		var entites = await _repository.GetQueryable().Where(r => r.ShopId == request.ShopId).ToListAsync(cancellationToken: cancellationToken);
		var dtos = _mapper.Map<IEnumerable<BarberServiceDto>>(entites);
		return dtos;
	}
}