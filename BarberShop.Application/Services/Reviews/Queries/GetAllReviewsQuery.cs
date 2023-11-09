using AutoMapper;
using BarberShop.Application.Dto.Reviews;
using BarberShop.Application.Interfaces.Repositories;
using MediatR;

namespace BarberShop.Application.Services.Reviews.Queries;

public record GetAllReviewsQuery(int ShopId) : IRequest<IEnumerable<ReviewDto>>;

internal class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, IEnumerable<ReviewDto>>
{
	private readonly IReviewRepository _repository;
	private readonly IMapper _mapper;

	public GetAllReviewsQueryHandler(IReviewRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
	public async Task<IEnumerable<ReviewDto>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
	{
		var entites = await _repository.GetByShopId(request.ShopId);
		var dtos = _mapper.Map<IEnumerable<ReviewDto>>(entites);
		return dtos;
	}
}