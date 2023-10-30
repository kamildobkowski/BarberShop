using System.ComponentModel.DataAnnotations;

namespace BarberShop.Shops.Entities;

public class Address
{
	public int Id { get; set; }
	[Required]
	public string Street { get; set; }
	[Required]
	public string City { get; set; }
	[Required]
	public int Number { get; set; }
	public int ApartamentNumber { get; set; }
	public string? PostalCode { get; set; }
	public double Longitude { get; set; }
	public double Latitude { get; set; }

}