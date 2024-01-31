namespace BarberShop.Domain.ValueObjects;

public enum AppointmentStatus
{
	Created, 
	Approved, 
	CancelledByUser, 
	CancelledByShop,
	Successful
}