using FluentValidation;

namespace Bruno.Application.Handlers.Booking.Create;

public class CreateBookingValidator : AbstractValidator<CreateBookingCommand>
{
	public CreateBookingValidator()
	{
		RuleFor(x => x.StartDate)
			.LessThan(x => x.EndDate);

		RuleFor(x => x.EndDate)
			.GreaterThan(x => x.StartDate);

		RuleFor(x => x.TotalPrice)
			.NotEmpty()
			.GreaterThan(0);

		RuleFor(x => x.Status)
			.IsInEnum();

		RuleFor(x => x.CustomerId)
			.NotEmpty();

		RuleFor(x => x.VehicleId)
			.NotEmpty();
	}
}
