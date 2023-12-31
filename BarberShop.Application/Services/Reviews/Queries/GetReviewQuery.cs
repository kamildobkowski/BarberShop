using AutoMapper;
using BarberShop.Application.Dto.Reviews;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Exceptions;
using MediatR;

namespace BarberShop.Application.Services.Reviews.Queries;

public record GetReviewQuery(int ShopId, int Id) : IRequest<ReviewDto>;

internal class GetReviewQueryHandler : IRequestHandler<GetReviewQuery, ReviewDto>
{
	private readonly IReviewRepository _repository;
	private readonly IMapper _mapper;

	public GetReviewQueryHandler(IReviewRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
	public async Task<ReviewDto> Handle(GetReviewQuery request, CancellationToken cancellationToken)
	{
		var entity = await _repository
			.GetAsync(r=>r.Id==request.Id && r.ShopId==request.ShopId);
		if (entity is null)
			throw new NotFoundException();
		var dto = _mapper.Map<ReviewDto>(entity);
		return dto;
	}
}