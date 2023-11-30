using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites.Users;
using BarberShop.Domain.Exceptions;
using MediatR;

namespace BarberShop.Application.Services.Accounts.Commands;

public record AddShopIdToShopAdminCommand(string UserEmail, int ShopId) : IRequest;

internal class AddShopIdToShopAdminCommandHandler : IRequestHandler<AddShopIdToShopAdminCommand>
{
	private readonly IUserRepository _repository;

	public AddShopIdToShopAdminCommandHandler(IUserRepository repository)
	{
		_repository = repository;
	}
	public async Task Handle(AddShopIdToShopAdminCommand request, CancellationToken cancellationToken)
	{
		var user = await _repository.GetAsync(r => r.Email == request.UserEmail);
		if (user is null)
			throw new NotFoundException();
		user.ShopAdmin = new ShopAdmin
		{
			ShopId = request.ShopId
		};
		await _repository.SaveChangesAsync();
	}
}