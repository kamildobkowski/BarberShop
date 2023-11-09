namespace BarberShop.Application.Dto.Shops;

public class UpdateShopAddressDto
{
	public string? Street { get; set; } = default!;
	public int? Number { get; set; }
	public int? ApartamentNumber { get; set; }
	public string? City { get; set; } = default!;
	public string? PostalCode { get; set; }
}