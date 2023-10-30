namespace BarberShop.Shops.Models.Dto;

public class GetServiceDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public TimeSpan Duration { get; set; }
	public decimal Price { get; set; }
}