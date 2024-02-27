using BarberShop.Application.Dto.Slots;
using BarberShop.Application.Interfaces;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites.Appointments;
using MediatR;

namespace BarberShop.Application.Services.Slots.Commands;

public record AddSlotsCommand(List<CreateSlotDto> Dtos, int ShopId) : IRequest;

internal class AddSlotsCommandHandler : IRequestHandler<AddSlotsCommand>
{
	private readonly ITimeTableRepository _repository;
	private readonly IAuthorizationService _authorizationService;
	private readonly IUserContextService _userContextService;

	public AddSlotsCommandHandler(ITimeTableRepository repository, IAuthorizationService authorizationService, 
		IUserContextService userContextService)
	{
		_repository = repository;
		_authorizationService = authorizationService;
		_userContextService = userContextService;
	}
	public async Task Handle(AddSlotsCommand request, CancellationToken cancellationToken)
	{
		_authorizationService.AuthorizeShopAdmin(request.ShopId);
		var slots = new List<Slot>();
		foreach (var dto in request.Dtos)
		{
			slots.AddRange(
				Slot.CreateEmptySlots(dto.Date, dto.StartTime, dto.EndTime, request.ShopId, _userContextService.UserId));
		}

		_repository.AddRange(slots);
		await _repository.SaveChangesAsync();
	}
}