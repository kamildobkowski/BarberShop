using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Exceptions;
using MediatR;

namespace BarberShop.Application.Services.Reviews.Commands;

public record DeleteReviewCommand(int ShopId, int Id) : IRequest;
internal class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
{
	private readonly IReviewRepository _repository;

	public DeleteReviewCommandHandler(IReviewRepository repository)
	{
		_repository = repository;
	}
	public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository
			.GetAsync(r=>r.Id==request.Id && r.ShopId==request.ShopId);
		if (entity is null)
			throw new NotFoundException();
		_repository.Delete(entity);
		await _repository.SaveChangesAsync();
	}
} 