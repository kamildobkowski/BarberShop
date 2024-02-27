using BarberShop.Application.Dto.Reviews;
using BarberShop.Application.Interfaces;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Exceptions;
using MediatR;

namespace BarberShop.Application.Services.Reviews.Commands;

public record UpdateReviewCommand(int ShopId, int Id, UpdateReviewDto Dto) : IRequest;
internal class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
{
	private readonly IReviewRepository _repository;
	private readonly IAuthorizationService _authorizationService;

	public UpdateReviewCommandHandler(IReviewRepository repository, IAuthorizationService authorizationService)
	{
		_repository = repository;
		_authorizationService = authorizationService;
	}
	public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
	{
		_authorizationService.AuthorizeShopAdmin(request.ShopId);
		var entity = await _repository
			.GetAsync(r=>r.Id==request.Id && r.ShopId==request.ShopId);
		if (entity is null)
			throw new NotFoundException();
		if (request.Dto.Description is not null)
			entity.Description = request.Dto.Description;
		if (request.Dto.Rating is not null)
			entity.Rating = request.Dto.Rating.Value;
		await _repository.SaveChangesAsync();
	}
} 