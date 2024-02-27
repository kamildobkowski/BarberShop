using System.Data.Common;
using System.Security.Authentication;
using System.Security.Claims;
using AutoMapper;
using BarberShop.Application.Dto.Appointments;
using BarberShop.Application.Interfaces;
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
	private readonly IUserContextService _userContextService;
	private readonly IAuthorizationService _authorizationService;

	public CreateAppointmentCommandHandler(IAppointmentRepository repository, 
		ITimeTableRepository slotRepository, IBarberServiceRepository barberServiceRepository, 
		IUserContextService userContextService, IAuthorizationService authorizationService)
	{
		_repository = repository;
		_slotRepository = slotRepository;
		_barberServiceRepository = barberServiceRepository;
		_userContextService = userContextService;
		_authorizationService = authorizationService;
	}
	public async Task<int> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
	{
		_authorizationService.AuthorizeAppointment(null, request.ShopId);

		var userId = _userContextService.UserId;
		var entity = new Appointment
		{
			ServiceId = request.ServiceId,
			ShopId = request.ShopId,
			CustomerUserId = userId,
			StartTime = request.StartDate,
			CreatedById = userId
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