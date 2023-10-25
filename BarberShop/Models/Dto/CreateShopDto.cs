using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models.Dto;

public class CreateShopDto
{
	
	public string? Name { get; set; }
	
	public string? Street { get; set; }
	
	public string? City { get; set; }
	
	public int Number { get; set; }
	
	public int ApartamentNumber { get; set; }
	
	public string? PostalCode { get; set; }
	
}