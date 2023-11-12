using AutoMapper;
using BarberShop.Application.Dto.BarberServices;
using BarberShop.Application.Interfaces.Repositories;
using MediatR;

namespace BarberShop.Application.Services.BarberServices.Queries;

public record GetServiceQuery(int ShopId, int Id) : IRequest<BarberServiceDto>;

internal class GetServiceQueryHandler : IRequestHandler<GetServiceQuery, BarberServiceDto>
{
	private readonly IBarberServiceRepository _repository;
	private readonly IMapper _mapper;

	public GetServiceQueryHandler(IBarberServiceRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
	public async Task<BarberServiceDto> Handle(GetServiceQuery request, CancellationToken cancellationToken)
	{
		var entity = await _repository
			.GetAsync(r=>r.ShopId==request.ShopId && r.Id==request.Id);
		var dto = _mapper.Map<BarberServiceDto>(entity);
		return dto;
	}
}