using BarberShop.Application.Dto.Appointments;
using BarberShop.Application.Dto.Slots;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites.Appointments;
using MediatR;

namespace BarberShop.Application.Services.Slots;

public record AddSlotsCommand(List<CreateSlotDto> Dtos, int ShopId) : IRequest;

internal class AddSlotsCommandHandler : IRequestHandler<AddSlotsCommand>
{
	private readonly ITimeTableRepository _repository;

	public AddSlotsCommandHandler(ITimeTableRepository repository)
	{
		_repository = repository;
	}
	public async Task Handle(AddSlotsCommand request, CancellationToken cancellationToken)
	{
		var slots = new List<Slot>();
		foreach (var dto in request.Dtos)
		{
			slots.AddRange(Slot.CreateEmptySlots(dto.Date, dto.StartTime, dto.EndTime, request.ShopId));
		}

		_repository.AddRange(slots);
		await _repository.SaveChangesAsync();
	}
}