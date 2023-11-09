using BarberShop.Application.Dto.Reviews;
using BarberShop.Application.Interfaces.Repositories;
using MediatR;

namespace BarberShop.Application.Services.Reviews.Commands;

public record UpdateReviewCommand(int ShopId, int Id, UpdateReviewDto Dto) : IRequest;
internal class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
{
	private readonly IReviewRepository _repository;

	public UpdateReviewCommandHandler(IReviewRepository repository)
	{
		_repository = repository;
	}
	public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetByIdAsync(request.ShopId, request.Id);
		if (request.Dto.Description is not null)
			entity.Description = request.Dto.Description;
		if (request.Dto.Rating is not null)
			entity.Rating = request.Dto.Rating.Value;
		await _repository.SaveChangesAsync();
	}
} 