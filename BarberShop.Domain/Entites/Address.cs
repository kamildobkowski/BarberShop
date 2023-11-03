using BarberShop.Domain.Common;

namespace BarberShop.Domain.Entites;

public class Address : BaseEntity
{
	public string Street { get; set; } = default!;
	public int Number { get; set; }
	public int? ApartamentNumber { get; set; }
	public string City { get; set; } = default!;
	public string? PostalCode { get; set; }
	public double? Latitude { get; set; }
	public double? Longitude { get; set; }
}