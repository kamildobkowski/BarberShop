using System.ComponentModel.DataAnnotations;
using BarberShop.Domain.Entites.Appointments;

namespace BarberShop.Domain.Entites.Users;

public class Customer
{
	public int UserId { get; set; }
	public User User { get; set; } = default!;
	public string PhoneNumber { get; set; } = default!;
	public List<Appointment> Appointments { get; set; } = default!;
}