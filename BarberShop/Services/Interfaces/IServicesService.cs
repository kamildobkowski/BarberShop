using BarberShop.Entities.BarberShop;
using BarberShop.Models.Dto;

namespace BarberShop.Services.Interfaces;

public interface IServicesService
{
	GetServiceDto GetById(int shopId, int serviceId);
	IEnumerable<Service> GetAll(int shopId);
	void Add(int shopId, CreateServiceDto dto);
}