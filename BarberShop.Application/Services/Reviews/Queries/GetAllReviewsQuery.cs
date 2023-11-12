using AutoMapper;
using BarberShop.Application.Dto.Reviews;
using BarberShop.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
		var entites = await _repository
			.GetQueryable()
			.Where(r=>r.ShopId==request.ShopId)
			.ToListAsync(cancellationToken: cancellationToken);
		var dtos = _mapper.Map<IEnumerable<ReviewDto>>(entites);
		return dtos;
	}
}