using System.Data;
using BarberShop.Application.Interfaces.Repositories;
using BarberShop.Application.Services.Appointments.Commands;
using FluentValidation;

namespace BarberShop.Application.Dto.Appointments.Validation;

public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentCommand>
{
	public CreateAppointmentValidator(IBarberServiceRepository barberServiceRepository, ITimeTableRepository repository)
	{
		RuleFor(r => r.ServiceId)
			.NotEmpty().WithMessage("service id cannot be null");
		RuleFor(r => r.StartDate)
			.GreaterThanOrEqualTo(DateTime.Now).WithMessage("date cannot be in the past")
			.NotEmpty().WithMessage("date cannot be null");
		/*RuleFor(r => r)
			.Custom((obj, context) =>
			{
				var service = barberServiceRepository
					.GetAsync(r => r.Id == obj.ServiceId).Result;
				if (service is null)
					context.AddFailure("service with given id does not exist");
				if (service!.ShopId != obj.ShopId)
					context.AddFailure("shop id does not match with service id");
				for (var date = obj.StartDate;
				     date < obj.StartDate.Add(service.Duration);
				     date = date.Add(TimeSpan.FromMinutes(15)))
				{
					var slot = repository.GetAsync(r => r.TimeSlot.Equals(date)).Result;
					if (slot is null || slot.AppointmentId is not null)
						context.AddFailure("Time slot is already taken!");
				}
			});*/
	}
}