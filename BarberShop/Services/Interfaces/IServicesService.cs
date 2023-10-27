using BarberShop.Models.Dto;

namespace BarberShop.Services.Interfaces;

public interface IServicesService
{
	GetServiceDto GetById(int shopId, int serviceId);
	IEnumerable<GetServiceDto> GetAll(int shopId);
	int Add(int shopId, CreateServiceDto dto);
	public void Delete(int shopId, int id);
	public void Update(int shopId, int id, CreateServiceDto dto);
}