using BarberShop.Shops.Entities;

namespace BarberShop.Shops.Services.Interfaces;

public interface ILocationService
{
	Task GetCoordinates(Address address);
	double GetDistance(Address address1, Address address2);
}