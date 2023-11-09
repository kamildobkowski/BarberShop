using AutoMapper;
using BarberShop.Application.Dto.Reviews;
using BarberShop.Domain.Entites;

namespace BarberShop.Application.Profiles;

public class ReviewMappingProfile : Profile
{
	public ReviewMappingProfile()
	{
		CreateMap<Review, ReviewDto>();
		CreateMap<CreateReviewDto, Review>();
	}
}