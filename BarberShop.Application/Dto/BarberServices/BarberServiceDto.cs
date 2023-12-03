namespace BarberShop.Application.Dto.BarberServices;

public record BarberServiceDto
{
	public int Id { get; init; }
	public string Name { get; init; } = default!;
	public string? Description { get; init; }
	public decimal? Price { get; init; }
	public TimeSpan? Duration { get; init; }
}