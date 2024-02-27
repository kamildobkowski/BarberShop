using BarberShop.Application.Interfaces;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Exceptions;
using BarberShop.Domain.ValueObjects;
using MediatR;

namespace BarberShop.Application.Services.Appointments.Commands;

public sealed record UpdateAppointmentStatusCommand(int Id, string NewStatus) : IRequest;

internal sealed class UpdateAppointmentStatusCommandHandler : IRequestHandler<UpdateAppointmentStatusCommand>
{
	private readonly IAppointmentRepository _repository;
	private readonly IAuthorizationService _authorizationService;

	public UpdateAppointmentStatusCommandHandler(IAppointmentRepository repository, IAuthorizationService authorizationService)
	{
		_repository = repository;
		_authorizationService = authorizationService;
	}
	public async Task Handle(UpdateAppointmentStatusCommand request, CancellationToken cancellationToken)
	{
		if (!Enum.TryParse(request.NewStatus, out AppointmentStatus status))
		{
			throw new NotFoundException("Incorrect status");
		}
		var entity = await _repository.GetAsync(r => r.Id == request.Id);
		if (entity is null)
			throw new NotFoundException("Appointment not found");
		_authorizationService.AuthorizeAppointment(entity.CustomerUserId, entity.ShopId);
		entity.Status = status;
		await _repository.SaveChangesAsync();
	}
}
