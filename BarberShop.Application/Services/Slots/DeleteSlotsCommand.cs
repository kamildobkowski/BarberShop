using BarberShop.Application.Dto.Appointments;
using BarberShop.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Application.Services.Slots;

public record DeleteSlotsCommand(List<RemoveSlotDto> Dtos, int ShopId) : IRequest;

internal class DeleteSlotsCommandHandler : IRequestHandler<DeleteSlotsCommand>
{
	private readonly ITimeTableRepository _repository;

	public DeleteSlotsCommandHandler(ITimeTableRepository repository)
	{
		_repository = repository;
	}
	public async Task Handle(DeleteSlotsCommand request, CancellationToken cancellationToken)
	{
		foreach (var i in request.Dtos)
		{
			var startDate = i.Date.ToDateTime(i.StartTime);
			var endDate = i.Date.ToDateTime(i.EndTime).Subtract(TimeSpan.FromMinutes(15));
			var list = await _repository
				.GetQueryable()
				.Where(r => r.ShopId == request.ShopId 
				            && r.TimeSlot >= startDate 
				            && r.TimeSlot <= endDate)
				.ToListAsync(cancellationToken: cancellationToken);
			_repository.DeleteRange(list);
		}
		await _repository.SaveChangesAsync();
	}
} 