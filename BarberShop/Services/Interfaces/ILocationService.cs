using BarberShop.Entities.BarberShop;

namespace BarberShop.Services.Interfaces;

public interface ILocationService
{
	void GetCoordinates(Address address);
	double GetDistance(Address address1, Address address2);
}