using BarberShop.Entities.BarberShop;

namespace BarberShop.Models.Dto;

public class GetShopDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public List<GetServiceDto>? Services { get; set; }
	public List<Review>? Reviews { get; set; }
	public string Street { get; set; }
	public string City { get; set; }
	public int Number { get; set; }
	public int ApartamentNumber { get; set; }
	public string PostalCode { get; set; }
	public double Longitude { get; set; }
	public double Latitude { get; set; }
}