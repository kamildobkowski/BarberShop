using BarberShop.Shops.Models.Dto;

namespace BarberShop.Shops.Services.Interfaces;

public interface IReviewService
{
	public IEnumerable<GetReviewDto> GetAll(int shopId);
	public GetReviewDto GetById(int shopId, int id);
	public int AddReview(int shopId, CreateReviewDto dto);
	public void DeleteReview(int shopId, int id);
}