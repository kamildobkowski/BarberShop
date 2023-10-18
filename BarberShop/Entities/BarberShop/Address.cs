using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace BarberShop.Entities.BarberShop;

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
	public int PostalCode { get; set; }
	public double Longitute { get; set; }
	public double Latitude { get; set; }

}