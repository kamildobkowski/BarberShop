using System.Data.Common;
using System.Security.Authentication;
using System.Security.Claims;
using AutoMapper;
using BarberShop.Application.Dto.Appointments;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites.Appointments;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Application.Services.Appointments.Commands;

public record CreateAppointmentCommand(int ShopId, DateTime StartDate, int ServiceId) : IRequest<int>;

internal class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, int>
{
	private readonly IAppointmentRepository _repository;
	private readonly ITimeTableRepository _slotRepository;
	private readonly IBarberServiceRepository _barberServiceRepository;
	private readonly IHttpContextAccessor _accessor;

	public CreateAppointmentCommandHandler(IAppointmentRepository repository, 
		ITimeTableRepository slotRepository, IBarberServiceRepository barberServiceRepository, 
		IHttpContextAccessor accessor)
	{
		_repository = repository;
		_slotRepository = slotRepository;
		_barberServiceRepository = barberServiceRepository;
		_accessor = accessor;
	}
	public async Task<int> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
	{
		var userId = int.Parse(
			_accessor.HttpContext?
				.User.FindFirst(r => r.Type == ClaimTypes.NameIdentifier)!.Value 
				?? throw new AuthenticationException());
		var entity = new Appointment
		{
			ServiceId = request.ServiceId,
			ShopId = request.ShopId,
			CustomerUserId = userId,
			StartTime = request.StartDate
		};
		var service = _barberServiceRepository
			.GetAsync(r => r.Id == entity.ServiceId).Result;
		//adding entity to db
		_repository.Add(entity);
		await _repository.SaveChangesAsync();
		Console.WriteLine(entity.Id);
		//setting slots to taken and setting appointmentId
		for (var date = entity.StartTime; date < entity.StartTime.Add(service!.Duration); date = date.Add(TimeSpan.FromMinutes(15)))
		{
			var slot = await _slotRepository.GetAsync(r => r.TimeSlot.Equals(date));
			slot!.AppointmentId = entity.Id;
		}
		
		//saving timetable changes and appointments changes (the rest does not change)
		await _slotRepository.SaveChangesAsync();
		return entity.Id;
	}
} 