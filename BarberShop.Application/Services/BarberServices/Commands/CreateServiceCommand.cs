using AutoMapper;
using BarberShop.Application.Dto.BarberServices;
using BarberShop.Application.Interfaces;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using MediatR;

namespace BarberShop.Application.Services.BarberServices.Commands;

public record CreateServiceCommand(int ShopId, CreateBarberServiceDto Dto) : IRequest<int>;

internal class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, int>
{
	private readonly IBarberServiceRepository _repository;
	private readonly IMapper _mapper;
	private readonly IUserContextService _userContextService;
	private readonly IAuthorizationService _authorizationService;

	public CreateServiceCommandHandler(IBarberServiceRepository repository, IMapper mapper,
		IUserContextService userContextService, IAuthorizationService authorizationService)
	{
		_repository = repository;
		_mapper = mapper;
		_userContextService = userContextService;
		_authorizationService = authorizationService;
	}
	
	public async Task<int> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
	{
		_authorizationService.AuthorizeShopAdmin(request.ShopId);
		var entity = _mapper.Map<BarberService>(request.Dto);
		_repository.Add(entity);
		entity.CreatedById = (int)_userContextService.UserId!;
		await _repository.SaveChangesAsync();
		return entity.Id;
	}
}