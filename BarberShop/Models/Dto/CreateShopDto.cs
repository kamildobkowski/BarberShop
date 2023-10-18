using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models.Dto;

public class CreateShopDto
{
	[Required]
	public string Name { get; set; }
	[Required]
	public string Street { get; set; }
	[Required]
	public string City { get; set; }
	[Required]
	public int Number { get; set; }
	[Required]
	public int ApartamentNumber { get; set; }
	[Required]
	public int PostalCode { get; set; }
	
}