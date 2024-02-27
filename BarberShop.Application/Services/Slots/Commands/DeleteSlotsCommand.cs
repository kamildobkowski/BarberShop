using BarberShop.Application.Dto.Slots;
using BarberShop.Application.Interfaces;
using BarberShop.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Application.Services.Slots.Commands;

public record DeleteSlotsCommand(List<RemoveSlotDto> Dtos, int ShopId) : IRequest;

internal class DeleteSlotsCommandHandler : IRequestHandler<DeleteSlotsCommand>
{
	private readonly ITimeTableRepository _repository;
	private readonly IAuthorizationService _authorizationService;

	public DeleteSlotsCommandHandler(ITimeTableRepository repository, IAuthorizationService authorizationService)
	{
		_repository = repository;
		_authorizationService = authorizationService;
	}
	public async Task Handle(DeleteSlotsCommand request, CancellationToken cancellationToken)
	{
		_authorizationService.AuthorizeShopAdmin(request.ShopId);
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