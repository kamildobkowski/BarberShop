using AutoMapper;
using BarberShop.Application.Dto.Reviews;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites;
using MediatR;

namespace BarberShop.Application.Services.Reviews.Commands;

public record CreateReviewCommand(int ShopId, CreateReviewDto Dto) : IRequest<int>;

internal class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, int>
{
	private readonly IReviewRepository _repository;
	private readonly IMapper _mapper;

	public CreateReviewCommandHandler(IReviewRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
	public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<Review>(request.Dto);
		_repository.Add(entity);
		await _repository.SaveChangesAsync();
		return entity.Id;
	}
}