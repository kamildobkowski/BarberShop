using AutoMapper;
using BarberShop.Application.Dto.Appointments;
using BarberShop.Application.Interfaces;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Domain.Entites.Appointments;
using BarberShop.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Application.Services.Appointments.Queries;

public record GetAppointmentQuery(int AppointmentId) : IRequest<AppointmentDto>;

internal class GetAppointmentQueryHandler : IRequestHandler<GetAppointmentQuery, AppointmentDto>
{
	private readonly IAppointmentRepository _repository;
	private readonly IMapper _mapper;
	private readonly IAuthorizationService _authorizationService;

	public GetAppointmentQueryHandler(IAppointmentRepository repository, IMapper mapper, 
		IAuthorizationService authorizationService)
	{
		_repository = repository;
		_mapper = mapper;
		_authorizationService = authorizationService;
	}
	public async Task<AppointmentDto> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
	{
		
		var entity = await _repository.GetAsync(r => r.Id == request.AppointmentId);
		if (entity is null)
			throw new NotFoundException("appointment not found");
		_authorizationService.AuthorizeAppointment(entity.CustomerUserId, entity.ShopId);
		var dto = _mapper.Map<AppointmentDto>(entity);
		return dto;
	}
}