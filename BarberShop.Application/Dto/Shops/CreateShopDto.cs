namespace BarberShop.Application.Dto.Shops;

public class CreateShopDto
{
	public string Name { get; set; } = default!;
	public string Street { get; set; } = default!;
	public int Number { get; set; }
	public int? ApartamentNumber { get; set; }
	public string City { get; set; } = default!;
	public string PostalCode { get; set; } = default!;
}