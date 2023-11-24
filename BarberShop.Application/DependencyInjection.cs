using BarberShop.Application.Dto.Account.Validation;
using BarberShop.Application.Profiles;
using BarberShop.Application.Services.Shops.Commands;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Application;

public static class DependencyInjection
{
	public static void AddApplication(this IServiceCollection services)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateShopCommand).Assembly));
		services.AddAutoMapper(typeof(ShopMappingProfile));
		services.AddValidatorsFromAssemblyContaining<CreateCustomerDtoValidator>()
			.AddFluentValidationAutoValidation()
			.AddFluentValidationClientsideAdapters(); //in case of use MVC
	}
}