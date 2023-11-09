using AutoMapper;
using BarberShop.Application.Dto.BarberServices;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using MediatR;

namespace BarberShop.Application.Services.BarberServices.Commands;

public record CreateServiceCommand(int ShopId, CreateBarberServiceDto Dto) : IRequest<int>;

internal class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, int>
{
	private readonly IBarberServiceRepository _repository;
	private readonly IMapper _mapper;

	public CreateServiceCommandHandler(IBarberServiceRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
	
	public async Task<int> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<BarberService>(request.Dto);
		var id = await _repository.AddAsync(entity, request.ShopId);
		return id;
	}
}