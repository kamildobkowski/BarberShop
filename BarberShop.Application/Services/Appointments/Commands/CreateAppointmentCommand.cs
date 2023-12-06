using System.Data.Common;
using AutoMapper;
using BarberShop.Application.Dto.Appointments;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites.Appointments;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BarberShop.Application.Services.Appointments.Commands;

public record CreateAppointmentCommand(int ShopId, int UserId, DateTime StartDate, int ServiceId) : IRequest;
internal class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand>
{
	private readonly IAppointmentRepository _repository;
	private readonly ITimeTableRepository _slotRepository;
	private readonly IBarberServiceRepository _barberServiceRepository;

	public CreateAppointmentCommandHandler(IAppointmentRepository repository, 
		ITimeTableRepository slotRepository, IBarberServiceRepository barberServiceRepository)
	{
		_repository = repository;
		_slotRepository = slotRepository;
		_barberServiceRepository = barberServiceRepository;
	}
	public async Task Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
	{
		
		var entity = new Appointment
		{
			ServiceId = request.ServiceId,
			ShopId = request.ShopId,
			UserId = request.UserId,
			StartTime = request.StartDate
		};
		//validation
		var service = await _barberServiceRepository.GetAsync(r => r.Id == entity.Id);
		if (service is null)
			throw new BadHttpRequestException("Service does not exist");
		if (service.ShopId != request.ShopId)
			throw new BadHttpRequestException("Invalid shop id or service id");
		//validating if slots are to be taken
		for (var date = entity.StartTime; date < entity.StartTime.Add(service.Duration); date = date.Add(TimeSpan.FromMinutes(15)))
		{
			var slot = await _slotRepository.GetAsync(r => r.TimeSlot.Equals(date));
			if (slot is null || slot.AppointmentId is not null)
				throw new BadHttpRequestException("Time slot already taken!");
		}
		//adding entity to db
		_repository.Add(entity);
		//setting slots to taken and setting appointmentId
		for (var date = entity.StartTime; date < entity.StartTime.Add(service.Duration); date = date.Add(TimeSpan.FromMinutes(15)))
		{
			var slot = await _slotRepository.GetAsync(r => r.TimeSlot.Equals(date));
			slot!.AppointmentId = entity.Id;
		}
		
		//saving timetable changes and appointments changes (the rest does not change)
		await _repository.SaveChangesAsync();
		await _slotRepository.SaveChangesAsync();
	}
} 