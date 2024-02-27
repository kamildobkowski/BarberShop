using BarberShop.Application.Interfaces;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Exceptions;
using MediatR;

namespace BarberShop.Application.Services.Reviews.Commands;

public record DeleteReviewCommand(int ShopId, int Id) : IRequest;
internal class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
{
	private readonly IReviewRepository _repository;
	private readonly IAuthorizationService _authorizationService;

	public DeleteReviewCommandHandler(IReviewRepository repository, IAuthorizationService authorizationService)
	{
		_repository = repository;
		_authorizationService = authorizationService;
	}
	public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
	{
		_authorizationService.AuthorizeShopAdmin(request.ShopId);
		var entity = await _repository
			.GetAsync(r=>r.Id==request.Id && r.ShopId==request.ShopId);
		if (entity is null)
			throw new NotFoundException();
		_repository.Delete(entity);
		await _repository.SaveChangesAsync();
	}
} 