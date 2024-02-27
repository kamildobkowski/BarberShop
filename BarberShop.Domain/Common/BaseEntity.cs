namespace BarberShop.Domain.Common;

public abstract class BaseEntity
{
	public virtual int Id { get; set; }
	public DateTime Created { get; set; } = DateTime.Now;
	public DateTime? Updated { get; set; } = null;
	public int CreatedById { get; set; }
}