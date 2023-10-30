namespace BarberShop.Shops.Models.Dto;

public class GetShopDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public List<GetServiceDto>? Services { get; set; }
	public List<GetReviewDto>? Reviews { get; set; }
	public string Street { get; set; }
	public string City { get; set; }
	public int Number { get; set; }
	public int ApartamentNumber { get; set; }
	public string PostalCode { get; set; }
	public double Longitude { get; set; }
	public double Latitude { get; set; }
}