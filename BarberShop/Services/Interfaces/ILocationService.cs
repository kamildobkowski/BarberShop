using BarberShop.Entities.BarberShop;

namespace BarberShop.Services.Interfaces;

public interface ILocationService
{
	Task GetCoordinates(Address address);
}