using AutoMapper;
using BarberShop.Application.Dto.Reviews;
using BarberShop.Application.Interfaces;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using MediatR;

namespace BarberShop.Application.Services.Reviews.Commands;

public record CreateReviewCommand(int ShopId, CreateReviewDto Dto) : IRequest<int>;

internal class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, int>
{
	private readonly IReviewRepository _repository;
	private readonly IMapper _mapper;
	private readonly IUserContextService _userContextService;
	private readonly IAuthorizationService _authorizationService;

	public CreateReviewCommandHandler(IReviewRepository repository, IMapper mapper, IUserContextService userContextService, IAuthorizationService authorizationService)
	{
		_repository = repository;
		_mapper = mapper;
		_userContextService = userContextService;
		_authorizationService = authorizationService;
	}
	public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
	{
		_authorizationService.AuthorizeShopAdmin(request.ShopId);
		var entity = _mapper.Map<Review>(request.Dto);
		entity.CreatedById = (int)_userContextService.UserId!;
		_repository.Add(entity);
		await _repository.SaveChangesAsync();
		return entity.Id;
	}
}