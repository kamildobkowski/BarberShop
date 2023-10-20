using BarberShop.Entities.BarberShop;

namespace BarberShop.Models.Dto;

public class GetShopDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public List<Service>? Services { get; set; }
	public List<Review>? Reviews { get; set; }
	public string Street { get; set; }
	public string City { get; set; }
	public int Number { get; set; }
	public int ApartamentNumber { get; set; }
	public int PostalCode { get; set; }
}