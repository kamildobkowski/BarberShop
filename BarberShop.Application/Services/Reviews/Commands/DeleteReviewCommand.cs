using BarberShop.Application.Interfaces.Repositories;
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
		var entity = await _repository.GetByIdAsync(request.ShopId, request.Id);
		await _repository.DeleteAsync(entity);
	}
} 