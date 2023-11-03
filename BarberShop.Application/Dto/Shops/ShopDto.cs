using BarberShop.Application.Dto.BarberServices;
using BarberShop.Application.Dto.Reviews;

namespace BarberShop.Application.Dto.Shops;

public class ShopDto
{
	public string Name { get; set; } = default!;
	public List<BarberServiceDto> Services { get; set; } = default!;
	public List<ReviewDto> Reviews { get; set; } = default!;
	public string Street { get; set; } = default!;
	public int Number { get; set; }
	public int? ApartamentNumber { get; set; }
	public string City { get; set; } = default!;
	public string? PostalCode { get; set; }
	public double? Latitude { get; set; }
	public double? Longitude { get; set; }
}