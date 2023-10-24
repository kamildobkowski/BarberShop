using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models.Dto;

public class CreateServiceDto
{
	[Required]
	public string? Name { get; set; }
	[Required] 
	public decimal Price { get; set; }
	[Required]
	public TimeSpan? Duration { get; set; }
}