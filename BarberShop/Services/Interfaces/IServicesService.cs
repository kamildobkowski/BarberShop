using BarberShop.Entities.BarberShop;
using BarberShop.Models.Dto;

namespace BarberShop.Services.Interfaces;

public interface IServicesService
{
	GetServiceDto GetById(int shopId, int serviceId);
	IEnumerable<GetServiceDto> GetAll(int shopId);
	int Add(int shopId, CreateServiceDto dto);
}