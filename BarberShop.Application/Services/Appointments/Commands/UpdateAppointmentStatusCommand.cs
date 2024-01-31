using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Exceptions;
using BarberShop.Domain.ValueObjects;
using MediatR;

namespace BarberShop.Application.Services.Appointments.Commands;

public sealed record UpdateAppointmentStatusCommand(int Id, string NewStatus) : IRequest;

internal sealed class UpdateAppointmentStatusCommandHandler : IRequestHandler<UpdateAppointmentStatusCommand>
{
	private readonly IAppointmentRepository _repository;

	public UpdateAppointmentStatusCommandHandler(IAppointmentRepository repository)
	{
		_repository = repository;
	}
	public async Task Handle(UpdateAppointmentStatusCommand request, CancellationToken cancellationToken)
	{
		Enum.TryParse(request.NewStatus, out AppointmentStatus status);
		var entity = await _repository.GetAsync(r => r.Id == request.Id);
		if (entity is null)
			throw new NotFoundException();
		entity.Status = status;
		await _repository.SaveChangesAsync();
	}
}
