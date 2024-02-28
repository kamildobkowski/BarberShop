using AutoMapper;
using BarberShop.Application.Dto.Appointments;
using BarberShop.Application.Interfaces;
using BarberShop.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Application.Services.Appointments.Queries;

public record GetLoggedUserAppointmentsQuery() : IRequest<List<AppointmentDto>>;

internal class GetLoggedUserAppointmentsQueryHandler 
	: IRequestHandler<GetLoggedUserAppointmentsQuery, List<AppointmentDto>>
{
	private readonly IAppointmentRepository _repository;
	private readonly IUserContextService _userContextService;
	private readonly IMapper _mapper;

	public GetLoggedUserAppointmentsQueryHandler(IAppointmentRepository repository, IUserContextService userContextService
		, IMapper mapper)
	{
		_repository = repository;
		_userContextService = userContextService;
		_mapper = mapper;
	}
	public async Task<List<AppointmentDto>> Handle(GetLoggedUserAppointmentsQuery request, CancellationToken cancellationToken)
	{
		var userId = _userContextService.UserId; 
		var entities = await _repository.GetQueryable().Where(r => r.CustomerUserId == userId).ToListAsync(cancellationToken);
		var dtos = _mapper.Map<List<AppointmentDto>>(entities);
		return dtos;
	}
}