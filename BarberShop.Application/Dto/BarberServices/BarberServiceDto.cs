namespace BarberShop.Application.Dto.BarberServices;

public record BarberServiceDto
{
	public int Id { get; set; }
	public string Name { get; set; } = default!;
	public string? Description { get; set; }
	public decimal? Price { get; set; }
	public TimeSpan? Duration { get; set; }
}