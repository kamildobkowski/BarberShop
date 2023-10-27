using System.ComponentModel.DataAnnotations;


namespace BarberShop.Entities.BarberShop;

public class Shop
{
	public int Id { get; set; }
	[Required]
	public string Name { get; set; }
	
	public virtual List<Service>? Services { get; set; }
	public virtual List<Review>? Reviews { get; set; }
	
	
	public int AddressId { get; set; }
	public virtual Address Address { get; set; }

}