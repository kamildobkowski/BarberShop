using System.ComponentModel.DataAnnotations;

namespace BarberShop.Entities;

public class Shop
{
	public int Id { get; set; }
	[Required]
	public string Name { get; set; }
	
	public virtual List<Service> Services { get; set; }
	
	
}