using BarberShop.Application.Dto.Appointments;
using MediatR;

namespace BarberShop.Application.Services.Appointments.Commands;

public record CreateAppointmentCommand(int ShopId, int UserId, CreateAppointmentDto Dto) : IRequest;